using BlueLight.Main;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
//using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonSyncBotCore.Controllers
{

    [Route("api/adjustRateComplex")]
    public class AdjustRateComplexController : Controller
    {

        private ApplicationContext _context;
        private readonly OrderItemCancelAllService eventCancelAll;
        private readonly OrderItemMainService orderItemMainEvent;
        private readonly LogHelperStopWatch logHelperStopWatch;
        private readonly RepoUser repoUser;

        public SignalDashboard signalDashboard { get; }
        public IMediator Mediator { get; }

        //public DBContext Context { get; }

        private readonly LogHelperObject logHelperObject;

        public AdjustRateComplexController(ApplicationContext context,
                    OrderItemCancelAllService eventCancelAll,
                    OrderItemMainService orderItemMainEvent,
                    SignalDashboard signalDashboard,
                    LogHelperStopWatch logHelperStopWatch,
                    RepoUser repoUser, LogHelperObject logHelperObject,
                    IMediator mediator)
        {


            this._context = context;
            this.eventCancelAll = eventCancelAll;
            this.orderItemMainEvent = orderItemMainEvent;
            this.signalDashboard = signalDashboard;
            this.logHelperObject = logHelperObject;
            this.logHelperStopWatch = logHelperStopWatch;
            this.repoUser = repoUser;
            Mediator = mediator;
        }

        public static string GetUserName(HttpRequestMessage request)
        {
            var default1 = "NoHeader";
            default1 = "guest2@server.com";
            var output = request.Headers.From ?? default1;
            return output;
        }

        public static string GetUserName1(HttpRequestMessage request)
        {
            var default1 = "NoHeader";
            default1 = "guest2@server.com";
            var output = request.Headers.From ?? default1;
            return output;
        }

        private string GetUserName(HttpRequest request)
        {
            // var default1 = "NoHeader";
            // default1 = "guest2@server.com";
            var result = request.Headers.FirstOrDefault(x => x.Key == "From");
            var output = result.Value;
            return output;
        }

        //http://localhost:5002/api/AdjustRateComplex?currencyPair=btc_usd&rate=5000
        //[HttpGet]
        //[HttpGet("{currencyPair,rate}")]
        // public async Task<List<OrderItem>> Get(int isBuy)
        public async Task<IActionResult> Get(string currencyPair, decimal rate)
        {
            try
            {
                // throw new ArgumentException("[debug-sync] one");


                var userName = GetUserName(this.Request);
                if (string.IsNullOrEmpty(userName))
                {
                    // return Unauthorized();
                    userName = "bot_sync@server.com";
                }



                logHelperStopWatch.Start("AdjustRateLogic");
                await EnsureCancelAll(userName);
                await logHelperStopWatch.Save("cancelAll");

                await EnsureHasBalance(userName);
                await logHelperStopWatch.Save("ensureHasBalance");
                await _context.SaveChangesAsync();

                var logic = new AdjustRateLogic(_context,
                    this.orderItemMainEvent,
                    this.Mediator)
                {
                    currencyPair = currencyPair,
                    bfxLastRate = rate,
                    //userName = userName,
                    logHelper = logHelperStopWatch,
                    request = Request,
                    UserName = userName

                };

                await logic.Execute();
                await logHelperStopWatch.Save("afterExecute");

                var total = logic.total;

                await SendDashboardNotificationCompleteSync(total);
                logHelperStopWatch.End();
                return Ok(total);
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                var m2 = BadRequest(m);
                return m2;
            }

        }

        private async Task SendDashboardNotificationCompleteSync(decimal total)
        {
            string event1 = $"SyncFinish";
            await signalDashboard.Submit(event1);
        }

        private async Task EnsureHasBalance(string userName)
        {
            var userProfile = await this.repoUser.GetUser(userName);
            //var userProfile = await _context.UserProfiles.FirstAsync(x => x.username == userName);
            {
                var targetAmount = 100;
                var currency = "btc";
                await BalanceReadyLogic(userProfile, targetAmount, currency);
            }

            {
                var targetAmount = 10000 * 100;
                var currency = "usd";
                await BalanceReadyLogic(userProfile, targetAmount, currency);
            }
        }

        private async Task BalanceReadyLogic(UserProfile userProfile, int targetAmount, string currency)
        {
            var amount = userProfile.GetAvailableBalanceFromCurrency(currency);

            if (amount < targetAmount)
            {

                var command = new AddExternalManuallyCommand
                {
                    amount2 = targetAmount,
                    currencyCode = currency,
                    userName = userProfile.username
                };
                var result = await Mediator.Send(command);

                // userProfile.AddExternalManually(targetAmount, currency);
                string event1 = $"BalanceReady-{currency}";

                await signalDashboard.Submit(event1);
                var input = event1 + "-" + userProfile.username + "-" + targetAmount;


                await logHelperObject.SaveObject(input);
            }
        }

        private async Task EnsureCancelAll(string userName)
        {
            // var eventCancelAll = new OrderItemCancelAllEvent () {
            //     _context = this._context
            // };
            var userProfile = await repoUser.GetUser(userName);
            //var isCancelAll = await eventCancelAll.Execute(userProfile.LiteMode());

            var userProfileLiteMode = userProfile.LiteMode();
            var command = new CancelAllQueueCommand()
            {
                UserId = userProfileLiteMode.UserId,
                UserName = userProfileLiteMode.UserName
            };
            var isCancelAll = await Mediator.Send(command);



            if (isCancelAll)
            {
                string event1 = $"CancelAllBotSync";
                // var dashboardService = new DashboardService (_context);
                await signalDashboard.Submit(event1);
                var input = event1 + "-" + userName;


                await logHelperObject.SaveObject(input);
            }

        }
    }
}

//public IEnumerable<string> Get()
//{
//    return new string[] { "value1", "value2" };
//}

//// GET api/values/5
//public async Task<string> Get(int id)
//{
//    return "value";
//}

////http://localhost:5002/api/AdjustRateComplex?test=btc_usd
//public async Task<double> Get(string test)
//{
//    return 10;
//}

////http://localhost:5002/api/AdjustRateComplex?a=btc_usd&b=5000&c=a
//public async Task<double> Get(string a, double b, string c)
//{
//    return 11;
//}

////http://localhost:5002/api/AdjustRateComplex?a=btc_usd&b=5000
//public async Task<double> Get(string a, double b)
//{
//    return 12;
//}

////http://localhost:5002/api/AdjustRateComplex?currencyPair=btc_usd&rate=5000&data=a
//public async Task<double> GetOne(string currencyPair, double rate,string date)
//{
//    var userName = User.Identity.Name;

//    var logHelper = new LogHelperStopWatch("AdjustRateLogic");
//    await logHelper.Log("cancelAll");

//    return 5;
//}