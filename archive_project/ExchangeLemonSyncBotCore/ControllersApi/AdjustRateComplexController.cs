//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
////using System.Data.Entity;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
////using System.Web.Http;
////using System.Web.Mvc;
////using Microsoft.AspNetCore.Cors;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;

//namespace BlueLight.Main
//{

//    [Route("api/adjustRateComplex")]
//    public class AdjustRateComplexController : Controller
//    {

//        private ApplicationContext _context;
//        private readonly OrderItemCancelAllService eventCancelAll;
//        private readonly OrderItemMainService orderItemMainEvent;
//        private readonly LogHelperStopWatch logHelper;
//        private readonly RepoUser repoUser;

//        public SignalDashboard dashboardService { get; }

//        //public DBContext Context { get; }

//        public AdjustRateComplexController(ApplicationContext context,
//            OrderItemCancelAllService eventCancelAll,
//            OrderItemMainService orderItemMainEvent,
//            SignalDashboard dashboardService,
//            LogHelperStopWatch logHelper,
//            RepoUser repoUser)
//        {
//            //this._context = new DBContext();
//            //Context = context;
//            //_context.Configuration.ProxyCreationEnabled = false;

//            this._context = context;
//            this.eventCancelAll = eventCancelAll;
//            this.orderItemMainEvent = orderItemMainEvent;
//            this.dashboardService = dashboardService;
//            this.logHelper = logHelper;
//            this.repoUser = repoUser;
//        }

//        public static string GetUserName(HttpRequestMessage request)
//        {
//            var default1 = "NoHeader";
//            default1 = "guest2@server.com";
//            var output = request.Headers.From ?? default1;
//            return output;
//        }

//        private string GetUserName(HttpRequest request)
//        {
//            // var default1 = "NoHeader";
//            // default1 = "guest2@server.com";
//            var result = request.Headers.First(x => x.Key == "From");
//            var output = result.Value;
//            return output;
//        }

//        //http://localhost:5002/api/AdjustRateComplex?currencyPair=btc_usd&rate=5000
//        //[HttpGet]
//        //[HttpGet("{currencyPair,rate}")]
//        // public async Task<List<OrderItem>> Get(int isBuy)
//        public async Task<double> Get(string currencyPair, double rate)
//        {

//            var userName = GetUserName(this.Request);
//            //var userProfile = await _context.UserProfiles
//            //    .Include (x => x.UserProfileDetails)
//            //    .FirstAsync (x => x.username == userName);

//            var userProfile = await repoUser.GetUser(userName);

//            //var logHelper = new LogHelperStopWatch ( this._context);
//            logHelper.Start("AdjustRateLogic");
//            await EnsureCancelAll(userProfile);
//            await logHelper.Save("cancelAll");

//            await EnsureHasBalance(userProfile);
//            await logHelper.Save("ensureHasBalance");
//            await _context.SaveChangesAsync();

//            var logic = new AdjustRateLogic(_context, this.orderItemMainEvent)
//            {
//                currencyPair = currencyPair,
//                bfxLastRate = rate,
//                //userName = userName,
//                logHelper = logHelper,
//                request = Request,
//                userProfile = userProfile
//            };

//            await logic.Execute();
//            await logHelper.Save("afterExecute");

//            var total = logic.total;
//            return total;
//        }

//        private async Task EnsureHasBalance(UserProfile userProfile)
//        {
//            //var userProfile = await _context.UserProfiles.FirstAsync(x => x.username == userName);
//            {
//                var targetAmount = 100;
//                var currency = "btc";
//                await BalanceReadyLogic(userProfile, targetAmount, currency);
//            }

//            {
//                var targetAmount = 10000 * 100;
//                var currency = "usd";
//                await BalanceReadyLogic(userProfile, targetAmount, currency);
//            }
//        }

//        private async Task BalanceReadyLogic(UserProfile userProfile, int targetAmount, string currency)
//        {
//            var amount = userProfile.GetAvailableBalanceFromCurrency(currency);

//            if (amount < targetAmount)
//            {
//                userProfile.AddExternalManually(targetAmount, currency);
//                string event1 = $"BalanceReady-{currency}";
//                // var dashboardService = new DashboardService (_context);
//                await dashboardService.SendObjectStat(event1);
//                var input = event1 + "-" + userProfile.username + "-" + targetAmount;

//                var LogHelper = new LogHelperObject(_context);
//                LogHelper.SaveObject(input);
//            }
//        }

//        private async Task EnsureCancelAll(UserProfile userProfile)
//        {
//            // var eventCancelAll = new OrderItemCancelAllEvent () {
//            //     _context = this._context
//            // };
//            var isCancelAll = await eventCancelAll.Execute(userProfile);

//            var userName = userProfile.username;

//            if (isCancelAll)
//            {
//                string event1 = $"CancelAllBotSync";
//                // var dashboardService = new DashboardService (_context);
//                await dashboardService.SendObjectStat(event1);
//                var input = event1 + "-" + userName;

//                var LogHelper = new LogHelperObject(_context);
//                LogHelper.SaveObject(input);
//            }

//        }
//    }
//}

////public IEnumerable<string> Get()
////{
////    return new string[] { "value1", "value2" };
////}

////// GET api/values/5
////public async Task<string> Get(int id)
////{
////    return "value";
////}

//////http://localhost:5002/api/AdjustRateComplex?test=btc_usd
////public async Task<double> Get(string test)
////{
////    return 10;
////}

//////http://localhost:5002/api/AdjustRateComplex?a=btc_usd&b=5000&c=a
////public async Task<double> Get(string a, double b, string c)
////{
////    return 11;
////}

//////http://localhost:5002/api/AdjustRateComplex?a=btc_usd&b=5000
////public async Task<double> Get(string a, double b)
////{
////    return 12;
////}

//////http://localhost:5002/api/AdjustRateComplex?currencyPair=btc_usd&rate=5000&data=a
////public async Task<double> GetOne(string currencyPair, double rate,string date)
////{
////    var userName = User.Identity.Name;

////    var logHelper = new LogHelperStopWatch("AdjustRateLogic");
////    await logHelper.Log("cancelAll");

////    return 5;
////}