using BackEndClassLibrary;
using ExchangeLemonCore.Controllers;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using ExchangeLemonCore;
using DebugWorkplace;
using System.Threading.Tasks;
using BlueLight.Main;
//using DebugWorkplace;

namespace ConsoleDev
{
    class Program
    {
        static IServiceProvider serviceProvider = null;


        static void Main(string[] args)
        {

            NewMethod().GetAwaiter().GetResult();

            Console.WriteLine("Hello World! - dev");
            Console.ReadLine();
        }
        private static async Task Start(IServiceProvider serviceProvider)
        {
            {
                var l = new LogicGenerateGraphData(serviceProvider);
                await l.Execute();
            }
            {
                var l = new LogicScenarioOne(serviceProvider);
                //await l.GenerateAddress();
                //await l.Register();

            }
            {
                //var l = new LogicArchive(serviceProvider);
                //await l.Logic6Transfer();
            }


            //var j = new LogicError();
            //await j.TestOne();

            {
                var l = new LogicArchive(serviceProvider);
                //await l.Logic6Transfer();
            }

            {
                var l = new LogicArchive(serviceProvider);

                //await l.Logic12Transfer();
                //await l.Logic4DiffBalance();
                //await l.Logic5InqBalance();


                //await l.Logic8TransferAndMonitoringScenarioOne();
                //await l.Logic10TransferAndMonitoringScenarioTwo();


                //var c = BtcCloudService.Generate();
                //var lists = await c.GetListHook();

                //await l.Logic9DeleteAllHooks();
                //await l.Logic7RemittanceOutputBtcSend();
                //await l.Logic8CheckStatusTransfer();

                //await l.Logic11TransferAndMonitoringScenarioThree();
                await l.Logic6Transfer();
                //await l.Logic12CheckManualDeposit();
            }
            {
                //var l = new LogicSubmitRegister(serviceProvider);

                //var m1 = await l.GetList();
                //await l.ClearAllHook();
                ////await l.RegisterReceive();
                ////var m = await l.GetList();

                //await l.ExecuteRegisterNotificationSendConfirm();
                ////await l.RegisterNotifyTransactionHookTest();
                ////await l.RegisterNotifyTransactionHookTestTwo();
                //var m = await l.GetList();
            }


        }

        private static async System.Threading.Tasks.Task NewMethod()
        {


            var builder = new ConfigurationBuilder()

           .SetBasePath(Directory.GetCurrentDirectory())
           //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();

            IConfigurationRoot Configuration = builder.Build();
            IServiceProvider serviceProvider2 = new ServiceCollection().BuildServiceProvider();

            var logConnString = Configuration.GetConnectionString("LoggingConnection");
            var connString = Configuration.GetConnectionString("DefaultConnection");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IReplayPlayerService, ReplayPlayerService>();
            serviceCollection.AddTransient<IReplayValidationService, ReplayValidationService>();
            serviceCollection.AddTransient<IValidationCancelOrderAllService, DevValidationCancelOrderAllService>();

            //serviceCollection.AddTransient<ReplayValidationService>();
            serviceCollection.AddTransient<ReplayFileService>();

            serviceCollection.AddTransient<IBitcoinGetBalance, BitcoinGetBalance>();

            
            serviceProvider =
              DependencyHelper.GenerateServiceProviderConsole(
                  connString, logConnString, serviceCollection);

            await Start(serviceProvider);

        }


    }
}
