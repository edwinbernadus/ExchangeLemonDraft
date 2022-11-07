using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using WebLibrary;
using System.IO;
using System;

namespace ExchangeLemonCore.Controllers
{
    public class DevController : Controller
    {

        //private readonly RepoGraph repoGraph;
        private readonly IHubContext<StreamHub> streamHub;
        private readonly IHubContext<TransactionHub> transactionHub;

        public LoggingContext LoggingContext { get; set; }
        public ILogHelperMvc LogHelperMvc { get; }
        public PulseInsertService PulseInsertService { get; }
        // public ServiceBusService ServiceBusService { get; }

        ILogger<DevController> logger;
        private readonly ApplicationContext applicationContext;



        public DevController(
        //RepoGraph repoGraph,
        IHubContext<StreamHub> streamHub,
        IHubContext<TransactionHub> transactionHub,
        LoggingContext loggingContext,
        ILogHelperMvc logHelperMvc,
        ILogger<DevController> logger,
        ApplicationContext applicationContext,
        PulseInsertService pulseInsertService
        // ServiceBusService serviceBusService
        )

        {
            //this.repoGraph = repoGraph;
            this.streamHub = streamHub;
            this.transactionHub = transactionHub;
            this.LoggingContext = loggingContext;
            LogHelperMvc = logHelperMvc;

            this.logger = logger;
            this.applicationContext = applicationContext;
            PulseInsertService = pulseInsertService;
            // ServiceBusService = serviceBusService;
        }

        // http://localhost:5000/dev/TestAccess
        [LocalHostAttribute]
        public long TestAccess()
        {
            return 1;
        }

        // /dev/ver
        public string Ver()
        {
            var output = GlobalParamVersion.Version;
            return output.ToString();
        }

        public string Content()
        {
            var p = AppDomain.CurrentDomain.BaseDirectory;
            var p2 = Path.Combine(p, "content1.csv");

            var owners = System.IO.File.ReadAllText(p2);
            return owners;
        }

        // http://localhost:5000/dev/InquiryAccess
        public (string ipClient, string ipServer) InquiryAccess()
        {
            var result = LocalHostAttribute.InquiryAddress(HttpContext);
            return result;
        }

        // http://localhost:5000/dev/GetTotalWebDummy
        public async Task<long> GetTotalWebDummy()
        {
            var httpClient = new HttpClient();
            var url = "http://www.apple.com";
            var result = await httpClient.GetStringAsync(url);
            var output = result.Count();
            return output;
        }

        // http://localhost:5000/dev/insertPulse
        public async Task<bool> InsertPulse()
        {
            await PulseInsertService.InsertOrUpdate("test");
            return true;
        }

        // // http://localhost:5000/dev/RestartServiceBus
        // public async Task<bool> RestartServiceBus()
        // {
        //     await this.ServiceBusService.Run();
        //     return true;
        // }

        // http://localhost:5000/dev/resetPulse
        public async Task<bool> ResetPulse()
        {
            await PulseInsertService.Reset();
            return true;
        }


        // http://localhost:5000/dev/inquiryPulse
        public async Task<List<Pulse>> InquiryPulse()
        {
            List<Pulse> output = await PulseInsertService.InquiryAllItems();
            return output;
        }
        public ActionResult Index()
        {
            // Log.Information("test info");
            logger.LogInformation("test info - 2");
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }


        // http://localhost:53252/dev/testlib
        public string TestLib()
        {
            var output = FrozenYoghurt.Class1.TestNumber().ToString();
            return output;
        }


        // http://localhost:53252/dev/TestRemittanceIncome
        public async Task<string> TestRemittanceIncome()
        {

            var user = await this.applicationContext.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync();
            user.IncomingTransfer(0.1m, "btc");
            await this.applicationContext.SaveChangesAsync();
            return user.username;
        }

        // http://localhost:53252/dev/TestRemittanceOutgoing
        public async Task<string> TestRemittanceOutgoing()
        {

            var user = await this.applicationContext.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync();
            user.OutgoingTransfer(0.2m, "btc");
            await this.applicationContext.SaveChangesAsync();
            return user.username;
        }

        //// http://localhost:5000/dev/GetThrottle
        ////[HttpGet("throttle")]
        //// Only allow access every 5 seconds
        //[Throttle(Name = "ThrottleTest", Seconds = 5)]
        //public object GetThrottle()
        //{
        //    return "OK";
        //}


        public async Task<ActionResult<List<MvInquiryBalanceWithUserName>>> Balance()
        {
            var profiles = await this.applicationContext.UserProfileDetails
                .Include(x => x.UserProfile).ToListAsync();
            var output = profiles.
            Select(x => new MvInquiryBalanceWithUserName()
            {
                UserName = x.UserProfile.username,
                Amount = x.Balance,
                CurrencyCode = x.CurrencyCode,
                AvailableBalance = UserProfileLogic.GetAvailableBalance(x),
                Balance = x.Balance,
                HoldBalance = x.HoldBalance
            }).ToList();
            return View(output);
        }

        public async Task<ActionResult<List<MvInquiryBalanceWithUserName>>> HoldFilter()
        {
            var profiles = await this.applicationContext.UserProfileDetails
                .Include(x => x.UserProfile)
                .Where(x => x.HoldBalance != 0)
                .ToListAsync();
            var output = profiles.
            Select(x => new MvInquiryBalanceWithUserName()
            {
                UserName = x.UserProfile.username,
                Amount = x.Balance,
                CurrencyCode = x.CurrencyCode,
                AvailableBalance = UserProfileLogic.GetAvailableBalance(x),
                Balance = x.Balance,
                HoldBalance = x.HoldBalance
            }).ToList();
            return View("Balance", output);
        }



        public ActionResult VuePlayground()
        {
            return View();
        }

        //// public async Task<ActionResult> GraphPlayground()
        //public async Task<List<GraphPlainData>> GraphPlayground()
        //{

        //    //var items = GraphDummyFactory.GenerateDummySetOne();
        //    //items = GraphDummyFactory.GenerateDummySetTwo();
        //    var items = await repoGraph.GetItems();
        //    // return View(items);
        //    return items;
        //}

        public ActionResult SignalPlayground()
        {
            return View();
        }

        public bool TestSendMessage()
        {
            this.LogHelperMvc.SaveLog("zzzz wooot", null);
            return true;
        }


        // static Logger log;
        // public void SendMessage(string id)
        // {
        //     var input = id;
        //     var z1 = "LPKty2CK9O/V6bg+4x4ZOhqpFEETsBiq9dqc/ypctku1+XK2BLCZttMEmTcAbCpHVWM4HbbsOhHaBr5dFs6W/A==";

        //     string webHookUri = "https://outlook.office.com/webhook/41e25f2f-0ed1-47e3-b70b-4f81785ab02a@e9689a0c-43c7-48cc-8bc3-fb3d79f6e2b8/IncomingWebhook/8413cbf0f0c347d2b2f46ea2b1b4e39f/ce0890d7-cf9c-42bb-b244-75a0ce68e3f8";
        //     string title = "title 1";

        //     if (log == null)
        //     {
        //         log = new LoggerConfiguration()
        //         .WriteTo.Sentry("https://75068368569c424783ed423cb3b1e99d@sentry.io/210698")
        //         .WriteTo.Console()
        //         .WriteTo.MicrosoftTeams(webHookUri, title: title)
        //         .WriteTo.AzureAnalytics("ecffecac-a7d3-446a-8afe-74e96ba4d7ae", z1)
        //         .Enrich.FromLogContext()
        //         .CreateLogger();
        //     }
        //     log.Error(input);
        // }



    }
}


// public bool UpdateRate(int id)
// {
//     var rate = id;
//     this.streamHub.Clients.All.SendAsync("Counter", 20, 1000);

//     var period1 = DateTimeHelper.Convert(DateTime.Now);
//     var period = DateTimeHelper.GetSequence(period1);

//     var output = repoGraph.GetItemDetail(period1, rate);
//     var result = output.Value;
//     this.transactionHub.Clients.All.SendAsync("ListenGraph", "btc_usd", period, rate);
//     return true;
// }