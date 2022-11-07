using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeLemonCore;
using ExchangeLemonCore.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BlueLight.Main
{
    public class HomeController : Controller
    {
        private ApplicationContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RepoGeneric repoGeneric;

        public FakeAccountService FakeAccountService { get; }
        public FakeSpotMarketService FakeSpotMarketService { get; }
        // public ServiceBusService ServiceBusService { get; }

        //private DBContext _context = new DBContext();

        public HomeController(ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RepoGeneric repoGeneric,
            FakeAccountService FakeAccountService,
            FakeSpotMarketService FakeSpotMarketService
            // , ServiceBusService serviceBusService
            )
        {
            this._context = context;
            this.userManager = userManager;
            this.repoGeneric = repoGeneric;
            this.FakeAccountService = FakeAccountService;
            this.FakeSpotMarketService = FakeSpotMarketService;
            // ServiceBusService = serviceBusService;
        }

        //[Authorize]

        // http://localhost:5000/home/sayhello
        public async Task<string> SayHello()
        {

            await Task.Delay(0);
            return "woot-2";
        }

        public async Task<ActionResult> Welcome()
        {

            // var RepoGeneric = new RepoGeneric (_context);

            List<MvInquiryBalance> output;
            decimal averageBtc = 0;
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                output = await repoGeneric.GetCurrentWallet(userName);
                var first = output.First();
                output.Clear();
                output.Add(first);
                averageBtc = await GetAverageBtc();
            }
            else
            {
                output = repoGeneric.WelcomePageDataPlain();
            }

            var displayAverageBtc = DisplayHelper.RateDisplay(averageBtc);
            ViewBag.DisplayAverageBtc = displayAverageBtc;

            //var lorem = new Bogus.DataSets.Lorem();
            //var content = lorem.Paragraphs();
            var content = "hello world - this is content";
            ViewBag.NewsContent = content;

            return View(output);
        }

        private async Task<decimal> GetAverageBtc()
        {
            var userName = User.Identity.Name;
            var userProfile = await this._context.UserProfiles.FirstAsync(x => x.username == userName);
            var details = userProfile.UserProfileDetails;

            var markets = await _context.SpotMarkets.Where(x => x.CurrencyPair.Contains("_btc")).ToListAsync();

            //var name = "xlm";

            decimal output = 0m;

            var btc = userProfile.GetAvailableBalanceFromCurrency("btc");
            output += btc;

            var excludeCurrency = new string[] { "btc", "usd", "idr" };
            var details1 = details.Where(x => excludeCurrency.Contains(x.CurrencyCode) == false);
            foreach (var i in details1)
            {
                var name = i.CurrencyCode;
                var lastRate = GetLastRate(markets, name);
                var balance = UserProfileLogic.GetAvailableBalance(i);
                var result = balance * lastRate;
                output += result;
            }

            return output;
            //return 0.0025;
        }

        private decimal GetLastRate(List<SpotMarket> markets, string name)
        {
            var market = markets.FirstOrDefault(x => x.CurrencyPair.Contains(name));
            if (market == null)
            {
                return 0;
            }

            var lastRate = market.LastRate;
            return lastRate;
        }

        
        public async Task<ActionResult> Index()
        {
            await Task.Delay(0);
            //var context = new ApplicationContext ();


#if DEBUG
            ForceSetSignalConnString();
#endif

            //await EnsureInitData();
           

            if (this.User.Identity.IsAuthenticated)
            {



                //return RedirectToAction ("welcome", "home", null);
                ////http://localhost:52494/spotmarkets
                ////return RedirectToAction("index", "spotmarket",null);
                ////ViewBag.WelcomeMessage = "Welcome, " + this.User.Identity.Name;
            }
            else
            {

                ViewBag.WelcomeMessage = "Please Sign In";
            }

            // await this.ServiceBusService.Run();
            return View();

        }

        public async Task<bool> InitData()
        {
            await FakeSpotMarketService.EnsureDataPopulated();
            var totalError = await FakeAccountService.InitAccount();
            return true;
        }

        private void ForceSetSignalConnString()
        {
            var url = WebHelper.GetUrl(Request);
            var url2 = url.TrimEnd('/');
            ParamRepo.SignalConnectionUrl = url2;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}