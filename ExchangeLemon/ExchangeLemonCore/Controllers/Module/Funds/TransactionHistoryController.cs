// ;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main {
    [Authorize]
    public class TransactionHistoryController : Controller {

        private ApplicationContext _context;

        public TransactionHistoryController (ApplicationContext context) {
            this._context = context;
        }

        // GET: SpotMarkets
        public async Task<ActionResult> Index () {
            var userName = User.Identity.Name;

            var userProfile = await _context.UserProfiles
                .Include (x => x.UserProfileDetails)
                .FirstAsync (x => x.username == userName);

            var output = userProfile.UserProfileDetails.
            Select (x => new MvInquiryBalance () {
                Amount = x.Balance,
                    CurrencyCode = x.CurrencyCode,
                    AvailableBalance = UserProfileLogic.GetAvailableBalance(x),
                    Balance = x.Balance,
                    HoldBalance = x.HoldBalance
            }).ToList ();
            
            output = output.Where(x => x.CurrencyCode == CurrencyParam.BtcCode).ToList();
            
            return View (output);

        }

        public ActionResult Details (string id) {
            var currencyCode = id;

            var uri = WebHelper.GetUrlToUri (Request);
            // string newUrl2 = DisplayHelper.GetNewUrlReport (uri);
            // string newUrl2 = DisplayHelper.GetNewUrlReportExtWithPagingPrefix (uri);
            string newUrl2 = DisplayHelper.GetNewUrlReportExtWithSuffixPaging (uri);
            ViewBag.UrlSourceTable = newUrl2;

            ViewBag.CurrentDateTime = DateTime.Now.ToString();
            return View ();
        }
    }
}

//bool isDemo = false;
//if (isDemo)
//{
//    var output = Enumerable.Range(0, 20).Select(x => new ExternalTransaction()
//    {
//        CreatedDate = DateTime.Now,
//        Amount = x
//    }).ToList();
//    //var output = new List<ExternalTransaction>();
//    //output.Add(new ExternalTransaction()
//    //{
//    //    Amount = 10
//    //});
//    //output.Add(new ExternalTransaction()
//    //{
//    //    Amount = 11
//    //});
//    return View(output);
//}
//else
//{
//    var user = await FactoryOrders.GetUser(context, this);
//    var detail = user.GetUserProfileDetail(currencyCode);
//    var output = detail.ExternalTransactions.OrderByDescending(x => x.Id).ToList();
//    return View(output);
//}

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