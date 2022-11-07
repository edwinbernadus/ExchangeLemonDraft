using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlueLight.Main;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
// using Serilog;
using Microsoft.Extensions.DependencyInjection;
using DebugWorkplace;

namespace ExchangeLemonCore.Controllers
{
    public class DevReplayController : Controller
    {
        Random random = new Random();

        private readonly IReplayPlayerService replayPlayerService;
        private readonly ReplayFileService replayFileService;
        private readonly EnsureContextService ensureContextService;

        //public readonly OrderItemMainService orderItemMainService;

        private readonly IReplayValidationService _replayValidationService;

        private readonly LoggingExtContext loggingExtContext;
        private readonly ApplicationContext applicationContext;
        private readonly IValidationCancelOrderAllService validationCancelOrderAllService;

        public OrderItemMainValidationDebug OrderItemMainValidationDebug { get; }

        //private readonly IServiceProvider serviceProvider;
        //private readonly FakeAccountService fakeAccountService;
        //private readonly FakeSpotMarketService fakeSpotMarketService;

        public DevReplayController(

                    IReplayPlayerService replayPlayerService,
                    ReplayFileService replayFileService,
                    IReplayValidationService replayValidationService,
                    EnsureContextService ensureContextService,
                    LoggingExtContext loggingExtContext,
                    ApplicationContext applicationContext,
                    IValidationCancelOrderAllService validationCancelOrderAllService,
                    //OrderItemMainService orderItemMainService,
                    OrderItemMainValidationDebug orderItemMainValidationDebug

            //FakeAccountService FakeAccountService,
            //FakeSpotMarketService FakeSpotMarketService
            )
        {
            _replayValidationService = replayValidationService;

            this.loggingExtContext = loggingExtContext;
            this.applicationContext = applicationContext;
            this.validationCancelOrderAllService = validationCancelOrderAllService;

            //fakeAccountService = FakeAccountService;
            //fakeSpotMarketService = FakeSpotMarketService;
            this.replayPlayerService = replayPlayerService;
            this.replayFileService = replayFileService;
            this.ensureContextService = ensureContextService;

            //this.orderItemMainService = orderItemMainService;
            OrderItemMainValidationDebug = orderItemMainValidationDebug;
        }

        // localhost:5000/DevReplay
        public async Task<long> Index()

        {
            await this.ensureContextService.Execute();
            await replayPlayerService.ExecuteFromTable();
            return -1;
        }


        // localhost:5000/DevReplay/ValidationCancelAllPredictionTooBig
        public async Task<decimal> ValidationCancelAllPredictionTooBig()

        {
            decimal sampleDiff = 0.0005m;
            await this.OrderItemMainValidationDebug.ValidationPredicationCancellAllTooBig(sampleDiff);
            return sampleDiff;

        }


        // localhost:5000/DevReplay/GetHoldBalance
        public async Task<decimal> GetHoldBalance()

        {
            var output = await validationCancelOrderAllService.Execute();
            return output.Item2;

        }

        //public async Task<bool> EnsureContext()
        //{
        //    await this.applicationContext.Database.EnsureCreatedAsync();
        //    //await LoggingContext.Database.EnsureCreatedAsync();
        //    await this.loggingExtContext.Database.EnsureCreatedAsync();

        //    var context = this.applicationContext;

        //    await this.fakeSpotMarketService.EnsureDataPopulated();
        //    var totalError = await fakeAccountService.InitAccount();

        //    return true;
        //}

        // localhost:5000/DevReplay/TestOne
        public async Task<bool> TestOne()
        {
            var r2 = await replayPlayerService.ValidationHoldBalance();
            bool isAHoldBalanceNegative = r2.Item1;
            if (isAHoldBalanceNegative)
            {
                throw new ArgumentException("hold negative");
            }
            return true;
        }

        // localhost:5000/DevReplay/CheckBalance
        public async Task<long> CheckBalance()
        {
            // await replayPlayerService.ExecuteFromTable();
            var output = await replayPlayerService.ValidationAvailableBalance();
            var isAvailableBalanceNegative = output.Item1;
            if (isAvailableBalanceNegative)
            {
                throw new ArgumentException("balance negative");
            }
            return -1;
        }

        public async Task<string> TestInsert()
        {
            var randomNumber = random.Next(0, 10);

            {

                var balance = await applicationContext.AccountBalances.FirstOrDefaultAsync();

                if (balance == null)
                {
                    var newBalance = new AccountBalance()
                    {
                        Balance = 100,
                        GuidCode = Guid.NewGuid(),
                    };
                    applicationContext.AccountBalances.Add(newBalance);
                    balance = newBalance;
                }
                var accountTransaction = balance.AddTransaction(randomNumber);
                balance.AccountHistories = balance.AccountHistories ?? new List<AccountHistory>();
                balance.AccountHistories.Add(accountTransaction);
                try
                {
                    await this.applicationContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                    return m;
                }

            }


            return randomNumber.ToString(); ;
        }

        // localhost:5000/DevReplay/TestInsert
        public async Task<string> TestInsertOne()
        {
            var randomNumber = random.Next(0, 10);

            // using (var transaction = new CommittableTransaction(
            //     new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))

            // // using (var scope = new TransactionScope(
            // //     TransactionScopeOption.Required,
            // //     new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            // {
            //     //     scope.Complete();

            //     // }
            using (var transaction = applicationContext.Database.BeginTransaction())
            {

                // applicationContext.Database.OpenConnection();
                // applicationContext.Database.EnlistTransaction(transaction);
                var balance = await applicationContext.AccountBalances.FirstOrDefaultAsync();

                if (balance == null)
                {
                    var newBalance = new AccountBalance()
                    {
                        Balance = 100,
                        GuidCode = Guid.NewGuid(),
                    };
                    applicationContext.AccountBalances.Add(newBalance);
                    // await this.applicationContext.SaveChangesAsync();
                    balance = newBalance;
                }
                var accountTransaction = balance.AddTransaction(randomNumber);
                try
                {
                    // await this.applicationContext.SaveChangesAsync();
                    balance.AccountHistories = balance.AccountHistories ?? new List<AccountHistory>();
                    balance.AccountHistories.Add(accountTransaction);
                    await this.applicationContext.SaveChangesAsync();


                    // throw new ArgumentException("woot error");
                    // await this.applicationContext.SaveChangesAsync();
                    // scope.Complete();
                    transaction.Commit();
                    // applicationContext.Database.CloseConnection();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var m = ex.Message;
                    return m;
                }

                // scope.Dispose();
            }


            return randomNumber.ToString(); ;
        }
        //localhost:5000/DevReplay/Validation
        public async Task<long> Validation()
        {
            await _replayValidationService.Execute();
            return -1;
        }


        //localhost:5000/DevReplay/DownloadFile
        public async Task<FileResult> DownloadFile()
        {
            var items = await replayPlayerService.GetReplayItemsAsync();
            var content = JsonConvert.SerializeObject(items);
            var contentType = "application/json";

            var bytes = Encoding.UTF8.GetBytes(content);
            var result = new FileContentResult(bytes, contentType);
            result.FileDownloadName = "sample.json";
            return result;
        }



        public async Task<bool> SaveItems()
        {
            var items = await replayPlayerService.GetReplayItemsAsync();
            await replayFileService.SaveToFileAsync(items);
            return true;
        }

        public async Task<long> LoadItems()
        {
            var z1 = await replayFileService.LoadFromFileAsync();
            var output = z1.Count();
            return output;
        }

        //localhost:5000/DevReplay/UploadToTable
        public async Task<long> UploadToTable()
        {
            var collection = await replayFileService.LoadItems();
            foreach (var item in collection)
            {
                item.Id = 0;
                this.loggingExtContext.LogItemEventSources.Add(item);
                await this.loggingExtContext.SaveChangesAsync();
            }

            var output = collection.Count();
            return output;
        }


    }
}

