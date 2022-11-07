// ;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    [Authorize]
    public class ReportController : Controller
    {
        //private DBContext _context = new DBContext();

        private ApplicationContext _context;
        private readonly RepoGeneric repoGeneric;
        private readonly RepoUser repoUser;

        //private DBContext _context = new DBContext();

        public ReportController(ApplicationContext context,
        RepoGeneric repoGeneric, RepoUser repoUser)
        {
            this._context = context;
            this.repoGeneric = repoGeneric;
            this.repoUser = repoUser;
        }

        [Authorize]
        // GET: SpotMarkets
        public async Task<ActionResult> OrderHistory()
        {
            var userName = User.Identity.Name;

            var output = await _context.SpotMarkets.ToListAsync();
            var result = output.Select(x => new MvInquiryBalancePair()
            {
                CurrencyPair = x.CurrencyPair
            }).ToList();

            return View(result);
        }

        [Authorize]
        // GET: SpotMarkets
        public async Task<ActionResult> OrderHistoryDetail(string id)
        {
            var currencyPair = id;
            var userName = User.Identity.Name;

            var output = await repoGeneric.GetHistoryOrders(userName,
                currencyPair, false);

            var output2 = await output.ToListAsync();
            var output3 = output2.Select(x => new MvDetailReportOrder(x)).ToList();

            return View(output3);
        }

        [Authorize]
        // GET: SpotMarkets
        public async Task<ActionResult> Deposit()
        {
            var userName = User.Identity.Name;
            //var userName = "";
            //var userProfile = await _context.UserProfiles
            //    .Include (x => x.UserProfileDetails)
            //    .FirstAsync (x => x.username == userName);
            var userProfile = await repoUser.GetUser(userName);
            var output = userProfile.UserProfileDetails.
            Select(x => new MvInquiryBalance()
            {
                Amount = x.Balance,
                CurrencyCode = x.CurrencyCode,
                AvailableBalance = UserProfileLogic.GetAvailableBalance(x),
                Balance = x.Balance,
                HoldBalance = x.HoldBalance
            }).ToList();
            output = output.Where(x => x.CurrencyCode == CurrencyParam.BtcCode).ToList();
            return View(output);
            //return View(db.SpotMarkets.ToList());
        }

        // GET: SpotMarkets/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

        // POST: SpotMarkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            SpotMarket spotMarket = _context.SpotMarkets.Find(id);
//            _context.SpotMarkets.Remove(spotMarket);
//            _context.SaveChanges();
//            return RedirectToAction("Index");
//        }

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

//// GET: SpotMarkets/Details/5
//public ActionResult Details(int? id)
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

//// GET: SpotMarkets/Edit/5
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

// POST: SpotMarkets/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

// GET: SpotMarkets/Delete/5
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