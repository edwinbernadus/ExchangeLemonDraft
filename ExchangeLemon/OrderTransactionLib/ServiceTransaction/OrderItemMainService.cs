using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Serilog;

namespace BlueLight.Main
{



    public class OrderItemMainService
    {

        public static Action<OrderItemNotificationService >BackgroundNotificationExecutor;
        [JsonIgnore]
        public HttpRequest Request { get; set; }

        [JsonIgnore]
        private readonly ApplicationContext _applicationContext;
        [JsonIgnore]
        private readonly DashboardContext _dashboardContext;
        [JsonIgnore]
        private readonly LoggingExtContext _loggingExtContext;


        [JsonIgnore]
        public OrderItemMainServiceExt main { get; }


        public OrderItemMainService(
                    ApplicationContext appCtx,
                    DashboardContext dashboardCtx,
                    LoggingExtContext loggingExtCxt,
                    OrderItemMainServiceExt extService)
        {


            this._dashboardContext = dashboardCtx;
            this._applicationContext = appCtx;
            this._loggingExtContext = loggingExtCxt;

            this.main = extService;
        }




        public StopWatchHelper stopWatchHelper { get; private set; }
        //WorkingFolder workingFolder = new WorkingFolder();
        WorkingFolder workingFolder;
        public OrderResult OrderResult { get; set; } = new OrderResult();






        void ValidationBusinessLogic(InputTransactionRaw input)
        {
            if (string.IsNullOrEmpty(input.rate))
            {
                throw new ArgumentException("input rate empty");
            }

            if (string.IsNullOrEmpty(input.amount))
            {
                throw new ArgumentException("input amount empty");
            }

            var parseAmount = Decimal.Parse(input.amount);
            if (parseAmount == 0)
            {
                throw new ArgumentException("amount is empty");
            }

            var parseRate = Decimal.Parse(input.rate);
            if (parseRate == 0)
            {
                throw new ArgumentException("rate is empty");
            }
        }


        // async Task ExecuteNew(WorkingFolder inputWorkingFolder)
        public async Task DirectExecuteFromHandler(WorkingFolderInput inputWorkingFolder)
        {
            //await NotificationForceStop(100);
            if (ParamSpecial.IsForceStop)
            {
                throw new ArgumentException("Force Stop Mode");
            }
            //#if DEBUG
            //throw new ArgumentException("test only");
            //#endif

            var inputRaw = inputWorkingFolder.inputTransactionRaw;
            this.workingFolder = new WorkingFolder()
            {
                includeLog = inputWorkingFolder.includeLog,
                inputTransaction = InputTransactionRawHelper.ConvertTo(inputRaw),

                //inputTransaction = inputWorkingFolder.inputTransactionRaw.ConvertTo()
                //input = inputWorkingFolder.input,
                //inputUser = inputWorkingFolder.inputUser,

            };
            //this.workingFolder = inputWorkingFolder;

            //validation
            ValidationBusinessLogic(inputWorkingFolder.inputTransactionRaw);



            //capture log / replay item
            var sessionId = await LogReplayStart();


            try
            {
                //transaction lock
                IDbContextTransaction transaction = EnsureUseTransactionStart();

                //populate  data
                await EnsureHasUserProfile(inputWorkingFolder.inputUser);

                // dto
                workingFolder.CreatedOrder = OrderFactory.Generate(
                    workingFolder.inputTransaction,
                    workingFolder.UserProfile,
                    workingFolder.inputTransaction.IsBuy);
                //validation
                ValidationCreatedOrderAmount();

                var inputOrder = workingFolder.CreatedOrder;



                this.OrderResult = await this.main.OrderTransactionService
                    .Execute(inputOrder, inputOrder.CurrencyPair,
                    skipBalanceNegativeValidation: false);

                // logic
                if (this.OrderResult.IsDealOrder)
                {
                    await PostDealOrder();
                }
                else
                {
                    await PostSubmitOrder();
                }

                // transaction lock - end
                EnsureUseTransactionEnd(transaction);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                await CatchExceptionDbUpdateConcurrency(ex);
                throw;
            }
            catch (DbUpdateException ex)
            {
                await CatchExceptionDbUpdate(ex);
                throw;
            }
            catch (Exception ex)
            {
                await CatchExceptionGeneric(ex);
                throw;
            }
            finally
            {
                await main.OrderItemMainFlag.SemaphoreEnd();
            }


            await SendNotification();
            await ReckonTransaction();
            await ValidationJumpHigh();
        }

        public static Action RunBackground;



        private async Task CatchExceptionGeneric(Exception ex)
        {
            this.main.CustomTelemetryService.Submit(ex);
            var type1 = ex.GetType().ToString();
            Console2.WriteLine(ex.Message);
            var message = $"[{type1}] : {ex.Message}";

            var userName = workingFolder.UserProfile.username;
            var message2 = $"GenericException-{userName}";
            await this.main.SignalDashboard.Submit(message2);

            await EndPrepareError(message, ex.StackTrace);
        }

        private async Task CatchExceptionDbUpdate(DbUpdateException ex)
        {
            this.main.CustomTelemetryService.Submit(ex);

            Console2.WriteLine(ex.Message);
            var message = $"[DbUpdateException] : {ex.Message}";

            var userName = workingFolder.UserProfile.username;
            var message2 = $"DbUpdateException-{userName}";
            await this.main.SignalDashboard.Submit(message2);

            await EndPrepareError(message, ex.StackTrace);
        }

        private async Task CatchExceptionDbUpdateConcurrency(DbUpdateConcurrencyException ex)
        {
            this.main.CustomTelemetryService.Submit(ex);

            var m2 = ex.Message;
            var userName = workingFolder.UserProfile.username;
            var message = $"DbUpdateConcurrencyException-{userName}";
            await this.main.SignalDashboard.Submit(message);
            await EndPrepareError(message, ex.StackTrace);
        }

        private void ValidationCreatedOrderAmount()
        {
            if (workingFolder.CreatedOrder.Amount <= 0)
            {
                throw new ArgumentException("[input zero] main service");
            }
        }

        private void EnsureUseTransactionEnd(IDbContextTransaction transaction)
        {
            if (FeatureRepo.UseTransaction)
            {
                transaction?.Commit();
            }
        }

        private IDbContextTransaction EnsureUseTransactionStart()
        {
            IDbContextTransaction transaction = null;
            if (FeatureRepo.UseTransaction)
            {
                transaction = this._applicationContext.Database.BeginTransaction();
            }
            return transaction;
        }

        static decimal? previous = null;
        private async Task ValidationJumpHigh()
        {

            var maxDiff = 0.1m;
            var result = await main.ValidationCancelOrderAllService.Execute();
            var newNumber = result.Item2;
            if (previous.HasValue)
            {
                var currentDiff = (newNumber - previous.GetValueOrDefault());


                if (currentDiff >= maxDiff)
                {
                    ParamSpecial.IsForceStop = true;
                    await NotificationForceStop(currentDiff);
                }
            }

            previous = newNumber;
        }

        private async Task NotificationForceStop(decimal currentDiff)
        {
            var message = $"ForceStop-{currentDiff}";
            await main.SignalDashboard.Submit(message);
            //TODO: [item9] NotificationForceStop
            //throw new NotImplementedException();
        }

        private async Task EndPrepare()
        {
            var w = workingFolder;

            var sessionLogEnd = LogSession.End(startSession: w.sessionLogStart);
            await SessionPersistanceSave(sessionLogEnd);
        }

        //private async Task EndPrepareError(string errMsg)
        private async Task EndPrepareError(string errMsg, string stackTrace)
        {
            var w = workingFolder;

            //var errMsg = ex.Message;
            //var stackTrace = ex.StackTrace;

            var sessionLogEnd = LogSession.End(startSession: w.sessionLogStart);
            sessionLogEnd.IsError = true;
            sessionLogEnd.ErrorMessage = errMsg;
            sessionLogEnd.StackTrace = stackTrace;

            await SessionPersistanceSave(sessionLogEnd);
        }

        private async Task LogReplayEnd(Guid sessionId)
        {

            var w = workingFolder;

            //await EnsureHasUserProfile(w.inputMode);
            var sessionLogStart = LogSession.Start(sessionId);
            await SessionPersistanceSave(sessionLogStart);

            this.workingFolder.sessionLogStart = sessionLogStart;

        }

        private async Task<Guid> LogReplayStart()
        {
            var sessionId = Guid.NewGuid();
            //var w = workingFolder;
            //if (w.includeLog)
            //{
            //    var userName = w.inputUser.GetUserName();
            //    sessionId = await main.ReplayCaptureService.SaveLogSubmitOrder(GetType(), w.input, userName);
            //}

            //var currency_pair = w.input.current_pair;
            //Log.Information($"order currency pair : {currency_pair}");
            var beforeItems = await main.ReplayValidationService.CaptureItems();

            this.workingFolder.beforeItems = beforeItems;
            return sessionId;
        }

        private async Task ReckonTransaction()
        {
            var w = workingFolder;
            ReplayValidationItem beforeItems = w.beforeItems;
            var afterItems = await main.ReplayValidationService.CaptureItems();
            var isValid = main.ReplayValidationService.IsValidLogic(beforeItems, afterItems);
            if (isValid == false)
            {
                var message = $"HoldTotalNotValid-{workingFolder.UserProfile.username}";
                await main.SignalDashboard.Submit(message);
            }
        }

        private async Task EnsureHasUserProfile(InputUser inputUser)
        {
            var input = await this.main.InquiryUserService.GetUser(inputUser);
            this.workingFolder.UserProfile = input;


        }

        private async Task SessionPersistanceSave(LogSession input)
        {
            this._loggingExtContext.Add(input);
            await this._loggingExtContext.SaveChangesAsync();
        }


        private async Task Init()
        {
            //var w = workingFolder;


            //var inputTransaction = w.inputTransaction;
            //var moduleName = w.ModuleName;
            stopWatchHelper = new StopWatchHelper(workingFolder.ModuleName);

            //var user = workingFolder.UserProfile;
            await main.LogHelperObject.SaveObjectIncludeUser(
                workingFolder.inputTransaction,
                workingFolder.UserProfile.username, "order");

            //var isBuy = inputTransaction.IsBuy;



            stopWatchHelper.Save("Before execute");


        }

        private async Task PostDealOrder()
        {

            var OrderHistories = OrderResult.OrderHistories;
            _applicationContext.OrderHistories.AddRange(OrderHistories);
            await UpdateLastRateSpotMarket(OrderResult);
            _applicationContext.EnsureAutoHistory();
            await _applicationContext.SaveChangesAsync();

            await ClearCacheListOrder();
            stopWatchHelper?.Save("After Save Db");

            await main.OrderItemMainValidationDebug.CheckAll(
                workingFolder,
                this.OrderResult);

            this.OrderResult.GuidId = workingFolder.CreatedOrder.GuidId;
        }


        private async Task PostSubmitOrder()
        {
            var OrderHistories = OrderResult.OrderHistories;
            _applicationContext.OrderHistories.AddRange(OrderHistories);

            await _applicationContext.SaveChangesAsync();
            stopWatchHelper?.Save("After Save Db");

            await ClearCacheListOrder();



            this.OrderResult.GuidId = workingFolder.CreatedOrder.GuidId;
        }

        async Task SendNotification()
        {
            try
            {
                var notificationService = main.OrderItemNotificationService;
                notificationService.Populate(workingFolder, OrderResult);
                
                if (BackgroundNotificationExecutor != null)
                {
                    BackgroundNotificationExecutor(notificationService);
                }
                else
                {
                    await notificationService.HandleNotification();
                }
                
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }

        }

        
        //private void BackgroundNotificationExecutor(OrderItemNotificationService notificationService)
        //{
        //    throw new NotImplementedException();
        //}

        private async Task ClearCacheListOrder()
        {
            var pair = workingFolder.CreatedOrder.CurrencyPair;
            await this.main.OrderListInquiryContextService.ClearCache(pair);
        }



        private async Task UpdateLastRateSpotMarket(OrderResult orderResult)
        {
            var isDealOrder = this.OrderResult.IsDealOrder;
            if (isDealOrder)
            {


                var currencyPair = workingFolder.CreatedOrder.CurrencyPair;
                var spotMarket = await _applicationContext.SpotMarkets
                    .FirstAsync(x => x.CurrencyPair == currencyPair);
                spotMarket.LastRate = OrderResult.TransactionLastRate;

            }
        }


        async Task AdObsoletedOrderToPersistance()
        {

            var user = workingFolder.UserProfile;

            // if not match , submit balance

            var createdOrder = workingFolder.CreatedOrder;
            if (createdOrder != null)
            {
                if (createdOrder.Id == 0)
                {
                    // check balance
                    var validationBusinessLogic = new ValidationBusinessLogic();
                    bool isValid = validationBusinessLogic.CheckBalanceSubmitOrder(user, createdOrder);
                    if (isValid == false)
                    {
                        //TODO: 6 balance_validation
                        ////const string Message = "Not sufficient funds";
                        //const string Message = "Saldo tidak cukup";
                        //throw new ArgumentException(Message);
                    }
                    _applicationContext.Orders.Add(createdOrder);

                    var event1 = "AddOrderToPersistance";
                    await main.SignalDashboard.Submit(event1);
                }
            }
        }
    }
}


//[Obsolete]
//async Task ExecuteFullLog(WorkingFolder inputWorkingFolder)

//// public async Task Execute(WorkingFolder inputWorkingFolder)
//{
//    if (ParamSpecial.IsForceStop)
//    {
//        throw new ArgumentException("Force Stop Mode");
//    }

//    this.workingFolder = inputWorkingFolder;

//    //validation
//    ValidationBusinessLogic(workingFolder.input);

//    //capture log / replay item
//    var sessionId = await LogReplayStart();

//    //semaphore
//    await this.main.OrderItemMainFlag.SemaphoreStart();

//    try
//    {
//        //transaction lock
//        IDbContextTransaction transaction = EnsureUseTransactionStart();

//        //populate  data
//        await EnsureHasUserProfile(workingFolder.inputUser);

//        // save log
//        await LogReplayEnd(sessionId);

//        // save log
//        await Init();

//        // dto
//        workingFolder.CreatedOrder = OrderFactory.Generate(
//            workingFolder.inputTransaction,
//            workingFolder.UserProfile,
//            workingFolder.inputTransaction.IsBuy);


//        //validation
//        ValidationCreatedOrderAmount();

//        // save log
//        stopWatchHelper.Start();

//        var inputOrder = inputWorkingFolder.CreatedOrder;
//        this.OrderResult = await this.main.OrderTransactionService
//            .Execute(inputOrder, inputOrder.CurrencyPair,
//            skipBalanceNegativeValidation: false);


//        // save log
//        stopWatchHelper.Save("After execute");

//        // logic
//        if (this.OrderResult.IsDealOrder)
//        {
//            await PostDealOrder();
//        }
//        else
//        {
//            await PostSubmitOrder();
//        }

//        // save log end
//        stopWatchHelper.End();

//        // save log end
//        await EndPrepare();

//        // transaction lock - end
//        EnsureUseTransactionEnd(transaction);
//    }

//    catch (DbUpdateConcurrencyException ex)
//    {
//        await CatchExceptionDbUpdateConcurrency(ex);
//        throw;
//    }
//    catch (DbUpdateException ex)
//    {
//        await CatchExceptionDbUpdate(ex);
//        throw;
//    }
//    catch (Exception ex)
//    {
//        await CatchExceptionGeneric(ex);
//        throw;
//    }
//    finally
//    {
//        await main.OrderItemMainFlag.SemaphoreEnd();
//    }

//    await SendNotification();
//    await ReckonTransaction();
//    await ValidationJumpHigh();
//}