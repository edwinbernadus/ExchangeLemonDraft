using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main {

    public class MarketHistoryTransactionController : Controller {
        private ApplicationContext _context;

        //private DBContext _context = new DBContext();

        public MarketHistoryTransactionController (ApplicationContext context) {
            this._context = context;
        }

        public ActionResult Details (string id) {
            //var url = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(Request);
            var uri = WebHelper.GetUrlToUri (Request);

            string newUrl2 = DisplayHelper.GetNewUrlReportExtWithSuffixPaging (uri);
            ViewBag.UrlSourceTable = newUrl2;
            ViewBag.CurrencyPair = id;
            return View ();
        }

    }
}

//var result = new PagingClass<AccountTransaction>();
//result.Populate(output, counter, newUrl,itemPerPage:10);

//var result2 = new PagingClass<MvAccountTransaction>()
//{
//    current_page = result.current_page,
//    last_page = result.last_page,
//    next_page_url = result.next_page_url,
//    prev_page_url = result.prev_page_url,
//    data = result.data.Select(x => new MvAccountTransaction()
//    {
//        Amount = x.Amount,
//        //CreatedBy = x.CreatedBy,
//        CreatedBy = "NoData",
//        Id = x.Id,
//        //TransactionRate = x.TransactionRate
//        TransactionRate = -2
//    }).ToList()
//};