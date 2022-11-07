using ExchangeLemonCore.Controllers;
using Serilog;
using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderItemMainValidationDebug
    {
        DashboardContext dashboardContext;
        IValidationCancelOrderAllService validationCancelOrderAllService;
        SignalDashboard _signalDashboard;
        LogHelperObject _logHelperObject;
        private readonly OrderItemNotificationService _OrderItemNotificationService;

        public OrderItemMainValidationDebug(DashboardContext dashboardContext,
        IValidationCancelOrderAllService validationCancelOrderAllService,
        SignalDashboard _signalDashboard,
        LogHelperObject _logHelperObject,
        OrderItemNotificationService _orderItemNotificationService)
        {
            this.dashboardContext = dashboardContext;
            this.validationCancelOrderAllService = validationCancelOrderAllService;
            this._signalDashboard = _signalDashboard;
            this._logHelperObject = _logHelperObject;
            _OrderItemNotificationService = _orderItemNotificationService;
        }
        
        public async Task<decimal> CheckCancelAllOrderBalance(UserProfile user,string ModuleName)
        {
            var itemService = this;
            //var user = itemService.UserProfile;

            var validationCancelAllResult = await this.validationCancelOrderAllService.Execute();
            var output = validationCancelAllResult.Item2;
            var methodName = "CancelAllForecast";
            if (validationCancelAllResult.Item1 == false)
            {
                var number1 = validationCancelAllResult.Item2;
                var isValid = OrderItemMainHelper.IsValidGreyZoneNumber(number1);
                if (isValid)
                {
                    var numberFiltered = OrderItemMainHelper.FilterGreyNumber(number1);
                    string event1 = $"PositiveCheck-[{numberFiltered}]-{methodName}-{ModuleName}-{user.username}";
                    await itemService._signalDashboard.Submit(event1);
                    var input = event1 + "-" + user.username;
                    await itemService._logHelperObject.SaveObject(input);
                }
                else
                {
                    string event1 = $"NegativeCheck-{methodName}-{ModuleName}-{user.username}-{number1}";
                    await itemService._signalDashboard.Submit(event1);
                    var input = event1 + "-" + user.username;
                    await itemService._logHelperObject.SaveObject(input);

                    await SaveLogPersistance(event1);
                }

            }
            else
            {
                string event1 = $"PositiveCheck-{methodName}-{ModuleName}-{user.username}";
                await itemService._signalDashboard.Submit(event1);
                var input = event1 + "-" + user.username;
                await itemService._logHelperObject.SaveObject(input);
            }


            return output;
        }


        public async Task CheckNegativeBalance(UserProfile user,string ModuleName)
        {
            var itemService = this;

            //var user = itemService.UserProfile;
            {
                var isNegative = user.IsAvailableBalanceNegative();
                if (isNegative)
                {
                    //ParamSpecial.IsForceStop = true;
                    string event1 = $"NegativeCheck-AvailableBalance-{ModuleName}-{user.username}";
                    await itemService._signalDashboard.Submit(event1);
                    var input = event1 + "-" + user.username;
                    await itemService._logHelperObject.SaveObject(input);
                }
            }
            {
                var isNegative = user.IsHoldBalanceNegative();
                if (isNegative)
                {
                    //ParamSpecial.IsForceStop = true;
                    var username = user.username;
                    string event1 = $"NegativeCheck-HoldBalance-{ModuleName}-{username}";
                    await itemService._signalDashboard.Submit(event1);
                    var input = event1 + "-" + user.username;
                    await itemService._logHelperObject.SaveObject(input);

                }
            }
        }

        private async Task SaveLogPersistance(string event1)
        {

            var logDetail = new LogDetail()
            {
                Content = event1,
                ModuleName = this.GetType().ToString(),
            };
            this.dashboardContext.LogDetails.Add(logDetail);
            await dashboardContext.SaveChangesAsync();
        }

        public async Task ValidationPredicationCancellAllTooBig(decimal input)
        {
            decimal maxDiff = 0.0001m;

            // var lastNumber = ParamSpecial.lastNumber;
            var currentDiff = input - ParamSpecial.lastNumber;
            if ((currentDiff) > maxDiff)
            {

                ParamSpecial.IsForceStop = true;
                var msg = $"Diff too big, from {input} to {ParamSpecial.lastNumber}";
                msg += $" - current diff: {currentDiff},";
                msg += $" handle procedure force stop";
                await this._signalDashboard.Submit(msg);
            }
            else
            {
                ParamSpecial.lastNumber = input;
            }


            // throw new NotImplementedException();
        }

        internal async Task CheckAll(WorkingFolder workingFolder, OrderResult OrderResult)
        {

            var w = workingFolder;
            UserProfile user = w.UserProfile;
            string ModuleName = w.ModuleName;
            Order CreatedOrder = w.CreatedOrder;

            var orderItemMainValidationDebug = this;
            await orderItemMainValidationDebug.CheckNegativeBalance(user, ModuleName);
            var totalBalanceAfterPredictionCancelAll = 
                await orderItemMainValidationDebug.CheckCancelAllOrderBalance(user, ModuleName);
            try
            {
                
                _OrderItemNotificationService.Populate(workingFolder, OrderResult);
                await _OrderItemNotificationService.HandleNotification();
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }

            //await orderItemMainValidationDebug.ValidationPredicationCancellAllTooBig(totalBalanceAfterPredictionCancelAll);
            ParamSpecial.LastCancelAllBalance = totalBalanceAfterPredictionCancelAll;
        }
    }
}