using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BlueLight.Main;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ExchangeLemonCore.Controllers;
using ExchangeLemonCore;
using MediatR;
//using PCLStorage;

namespace DebugWorkplace
{

    public class ReplayPlayerService : IReplayPlayerService
    //: IReplayPlayerService
    {
        private readonly LoggingExtContext loggingExtContext;
        private readonly RepoUser repoUser;
        //private readonly ReplayFileService replayFileService;

        // private readonly IFileProvider _fileProvider;

        //private readonly IServiceCollection serviceCollection;
        private readonly IServiceProvider serviceProvider;
        private readonly IReplayFileService replayFileService;
        IReplayValidationService replayValidationService;
        private readonly ApplicationContext applicationContext;
        private readonly IContextSaveService contextSaveService;
        private readonly IValidationCancelOrderAllService validationCancelOrderAllService;

        public IMediator Mediator { get; }

        public ReplayPlayerService(
            LoggingExtContext LoggingExtContext,
            RepoUser repoUser,
            // IFileProvider fileProvider,
            // IServiceCollection serviceCollection, 
            //ReplayFileService ReplayFileService,
            IServiceProvider serviceProvider,
            ReplayFileService replayFileService,
            IReplayValidationService replayValidationService,
            ApplicationContext applicationContext,
            IContextSaveService contextSaveService,
            IValidationCancelOrderAllService validationCancelOrderAllService,
            IMediator mediator)
        {
            this.loggingExtContext = LoggingExtContext;
            this.repoUser = repoUser;
            //replayFileService = ReplayFileService;
            // this._fileProvider = fileProvider;
            //this.serviceCollection = serviceCollection;
            this.serviceProvider = serviceProvider;
            this.replayFileService = replayFileService;
            this.replayValidationService = replayValidationService;
            this.applicationContext = applicationContext;
            this.contextSaveService = contextSaveService;
            this.validationCancelOrderAllService = validationCancelOrderAllService;
            Mediator = mediator;
        }

        public Task ClearDb()
        {
            // throw new NotImplementedException();
            return Task.Delay(0);
        }



        //public async Task<long> ExecuteFromFile()
        //{
        //    var replayPlayerService = this;
        //    await replayPlayerService.ClearDb();

        //    //List<LogItem> items = await replayPlayerService.GetReplayItemsAsync();
        //    var items = await replayFileService.LoadItems();

        //    var counter = 0;
        //    foreach (var i in items)
        //    {
        //        counter++;

        //        await replayValidationService.Execute(i, counter);
        //        Console2.WriteLine($"Process: {counter}");
        //        await replayPlayerService.Invoker(i);
        //        await replayValidationService.Execute(i, counter);
        //    }

        //    var output = items.Count();
        //    return output;


        //}

        public async Task<long> ExecuteFromTable()
        {
            var replayPlayerService = this;
            await replayPlayerService.ClearDb();

            List<LogItemEventSource> items = await replayPlayerService.GetReplayItemsAsync();
            // var items = await replayFileService.LoadFromFileAsync();

            var counter = 0;
            foreach (var i in items)
            {
                counter++;

                var beforeItems = await replayValidationService.CaptureItems();


                Console2.WriteLine($"Process: {counter}");
                try
                {
                    await replayPlayerService.Invoker(i);
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                }

                var afterItems = await replayValidationService.CaptureItems();

                var r = await ValidationAvailableBalance();
                bool isAvailableBalanceNegative = r.Item1;
                if (isAvailableBalanceNegative)
                {
                    // throw new ArgumentException("balance negative");
                }
                //await replayValidationService.Execute(i, counter);


                var r2 = await ValidationHoldBalance();
                bool isAHoldBalanceNegative = r2.Item1;
                if (isAHoldBalanceNegative)
                {
                    throw new ArgumentException("hold negative");
                }


                var isValid = replayValidationService.IsValidLogic(beforeItems, afterItems);
                if (isValid == false)
                {
                    replayValidationService.FindDiff(beforeItems, afterItems);
                    var m = 2;
                    var m2 = m + 3;
                }

                var validationCancelAllResult = await this.validationCancelOrderAllService.Execute(i);
                if (validationCancelAllResult.Item1 == false)
                {

                    throw new ArgumentException("total hold not zero");
                }
                //await contextSaveService.ExecuteAsync();

                //await contextSaveService.DebugAsync();
            }

            var output = items.Count();
            return output;


        }





        public async Task<Tuple<bool, List<UserProfileDetail>>> ValidationHoldBalance()
        {
            var details = await this.applicationContext.UserProfileDetails.ToListAsync();
            var items = details.Where(x => x.HoldBalance < 0).ToList();
            var isExists = items.Any();

            if (isExists)
            {
                var itemOne = items.First();
            }

            var output = new Tuple<bool, List<UserProfileDetail>>(isExists, items);
            return output;
        }




        public async Task<Tuple<bool, List<UserProfileDetail>>> ValidationAvailableBalance()
        {
            var details = await this.applicationContext.UserProfileDetails.ToListAsync();
            var items = details.Where(x => UserProfileLogic.GetAvailableBalance(x) < 0).ToList();
            var isExists = items.Any();

            if (isExists)
            {
                var itemOne = items.First();
            }

            var output = new Tuple<bool, List<UserProfileDetail>>(isExists, items);
            return output;
        }

        public async Task<List<LogItemEventSource>> GetReplayItemsAsync()
        {
            var methodNames = GetMethodNames();

            var items = await this.loggingExtContext.LogItemEventSources
                .Where(x => methodNames.Contains(x.MethodName) && x.IsRequest)
                .OrderBy(x => x.Id).ToListAsync();
            return items;
        }

        private List<string> GetMethodNames()
        {
            var methodNames = new List<string>();
            methodNames.Add(typeof(OrderItemCommand).Name);
            methodNames.Add(typeof(CancelByGuidCommand).Name);
            methodNames.Add(typeof(CancelByIdCommand).Name);
            methodNames.Add(typeof(CancelAllCommand).Name);

            return methodNames;
        }

        public async Task Invoker(LogItemEventSource logItem)
        {
            var className = logItem.MethodName;
            {
                var strFullyQualifiedName1 = "" + className;
                try
                {
                    Type t2 = Type.GetType(strFullyQualifiedName1);
                }
                catch (System.Exception ex)
                {
                    var m = ex.Message;


                }

            }

            var assemblyName = "OrderTransactionLib";
            // Type.GetType("MyProject.Domain.Model." + myClassName + ", AssemblyName");
            var strFullyQualifiedName = "BlueLight.Main." + className + $", {assemblyName}";
            Type t = Type.GetType(strFullyQualifiedName);
            var command = JsonConvert.DeserializeObject(logItem.Content, t);
            dynamic command2 = command;
            await Mediator.Send(command2);
        }
    }
}