using System;
using System.Collections.Generic;
// ;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using ExchangeLemonCore.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    [Authorize]
    public class SpotMarketController : Controller
    {
        //private DBContext _context = new DBContext();

        private ApplicationContext _context;
        private readonly RepoUser repoUser;

        public FakeSpotMarketService FakeSpotMarketService { get; }
        public IInquirySpotMarketService InquirySpotMarketService { get; }

        //private DBContext _context = new DBContext();

        public SpotMarketController(ApplicationContext context,
        RepoUser repoUser, FakeSpotMarketService FakeSpotMarketService,
        IInquirySpotMarketService InquirySpotMarketService)
        {
            this._context = context;
            this.repoUser = repoUser;
            this.FakeSpotMarketService = FakeSpotMarketService;
            this.InquirySpotMarketService = InquirySpotMarketService;
        }

        [AllowAnonymous]
        // GET: SpotMarkets
        public async Task<ActionResult> Index()
        {

            //System.Diagnostics.Trace.TraceInformation("Information");
            //System.Diagnostics.Trace.TraceWarning("Warning");
            //System.Diagnostics.Trace.TraceError("Error");

            System.Diagnostics.Trace.TraceInformation("Spot market - start");

            //await  EnsureDataPopulated();
            await FakeSpotMarketService.EnsureDataPopulated(); ;
            var output = await _context.SpotMarkets.ToListAsync();

            var itemsTop = output
                .Where(x => x.CurrencyPair.Contains("btc") && x.CurrencyPair != "btc_usd").ToList();
            var itemsBottom = output.Except(itemsTop).ToList();
            var result = new MvSpotMarket()
            {

                ItemsTop = new MvSpotMarketExt()
                {
                    CurrencyMode = "btc",
                    Items = itemsTop
                .Select(y => new MvSpotMarketDetail()
                {
                    SpotMarket = y,
                    MyBalance = -1
                }).ToList(),
                },
                ItemsBottom = new MvSpotMarketExt()
                {
                    CurrencyMode = "usd",
                    Items = itemsBottom
                .Select(y => new MvSpotMarketDetail()
                {
                    SpotMarket = y,
                    MyBalance = -1,
                }).ToList(),
                },

            };

            var spotMarketsTop = result.ItemsTop;
            
            result.ItemsTop.Items.Clear();
            var first = result.ItemsBottom.Items.First();
            result.ItemsBottom.Items.Clear();
            result.ItemsBottom.Items.Add(first);
            
            
            var spotMarketsBottom = result.ItemsBottom;

            //await PopulateAvailableBalance(spotMarketsTop.Items, User, _context);
            //await PopulateAvailableBalance(spotMarketsBottom.Items, User, _context);

            

            var allItems = spotMarketsTop.Items.Union(spotMarketsBottom.Items);

            UserProfile userProfile = null;
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                userProfile = await repoUser.GetUser(userName);
            }

                foreach (var item in allItems)
            {
                if (userProfile != null)
                {
                    this.InquirySpotMarketService.PopulateAvailableBalance(userProfile, item);
                }
              
                item.CalculateVolume = await this.InquirySpotMarketService
                    .GetVolumeAsync(item.SpotMarket.CurrencyPair);

                var item2 = await this.InquirySpotMarketService
                    .GetLastChangeAsync(item.SpotMarket.CurrencyPair);
                item.CalculateLastChange = item2.DiffPercentage;
                item.PreviousLastRate = item2.PreviousLastRate;
            }

            System.Diagnostics.Trace.TraceInformation("Spot market - end");


            return View(result);
            //return View(output);
        }

        //public async Task PopulateAvailableBalance(List<MvSpotMarketDetail> input,
        //IPrincipal User, ApplicationContext db)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userName = User.Identity.Name;
        //        //var userProfile = await db.UserProfiles
        //        //    .Include (x => x.UserProfileDetails)
        //        //    .FirstAsync (x => x.username == userName);

        //        var userProfile = await repoUser.GetUser(userName);

        //        foreach (var i in input)
        //        {
        //            var currencyPair = i.SpotMarket.CurrencyPair;
        //            var currency = currencyPair.Split('_')[0];
        //            var balance = userProfile.GetAvailableBalanceFromCurrency(currency);
        //            i.MyBalance = balance;
        //        }
        //    }
        //}


        [AllowAnonymous]
        // GET: SpotMarkets/Details/5
        //public async Task<ActionResult> Details(int? id)
        public async Task<ActionResult> Details(string id)
        {
            ViewBag.CurrentDateTime = DateTime.Now.ToString();

            //var spotMarket = await _context.SpotMarkets.FirstAsync(x => x.Id == id);
            var spotMarket = await _context.SpotMarkets.FirstAsync(x => x.CurrencyPair == id);
            var pair = spotMarket.CurrencyPair;
            ViewBag.CurrencyPair = pair;

            var items = await _context.SpotMarkets
                .Where(x => x.CurrencyPair == CurrencyParam.BtcPair)
                .ToListAsync();
            ViewBag.SpotMarkets = items;

            ViewBag.SignalConnectionUrl = ParamRepo.SignalConnectionUrl;

            var isLogin = User.Identity.IsAuthenticated;
            ViewBag.IsLogin = isLogin ? 1 : 0;
            
            ViewBag.IsDevMode = GlobalParam.IsDevMode.ToString().ToLower();
            // ViewBag.IsLogin = fasfa;
            return View();
        }

        // GET: SpotMarkets/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

        //// POST: SpotMarkets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,CurrencyCode,Volume,LastRate,PercentageMovement")] SpotMarket spotMarket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.SpotMarkets.Add(spotMarket);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(spotMarket);
        //}

        // GET: SpotMarkets/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SpotMarket spotMarket = _context.SpotMarkets.Find(id);
        //    if (spotMarket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(spotMarket);
        //}

        //// POST: SpotMarkets/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,CurrencyCode,Volume,LastRate,PercentageMovement")] SpotMarket spotMarket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(spotMarket).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(spotMarket);
        //}

        //// GET: SpotMarkets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SpotMarket spotMarket = _context.SpotMarkets.Find(id);
        //    if (spotMarket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(spotMarket);
        //}

        // POST: SpotMarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpotMarket spotMarket = _context.SpotMarkets.Find(id);
            _context.SpotMarkets.Remove(spotMarket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}