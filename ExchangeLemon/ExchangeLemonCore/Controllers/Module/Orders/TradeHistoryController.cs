// ;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    [Authorize]
    public class TradeHistoryController : Controller
    {
        //private DBContext _context = new DBContext();

        private ApplicationContext _context;
        private readonly RepoGeneric repoGeneric;

        public TradeHistoryController(ApplicationContext context,
          RepoGeneric repoGeneric)
        {
            this._context = context;
            this.repoGeneric = repoGeneric;
        }


        // GET: SpotMarkets
        public async Task<ActionResult> Index()
        {
            var userName = User.Identity.Name;

            var output = await _context.SpotMarkets.ToListAsync();
            output = output.Where(x => x.CurrencyPair == CurrencyParam.BtcPair).ToList();
            var result = output.Select(x => new MvInquiryBalancePair()
            {
                CurrencyPair = x.CurrencyPair
            }).ToList();

            return View(result);
        }


        // GET: SpotMarkets
        public async Task<ActionResult> Details(string id)
        {

            ViewBag.CurrentPair = id;
            var currencyPair = id;
            var userName = User.Identity.Name;

            var output = await repoGeneric.GetHistoryOrders(userName,
                currencyPair, true);

            var output2 = output.ToList();
            var output3 = output2.Select(x => new MvDetailReportOrder(x)).ToList();
            var w4 = MvDetailReportOrder.GetAverageBuy(output3);
            ViewBag.AverageBuy = w4;

            var uri = WebHelper.GetUrlToUri(Request);
            // string newUrl2 = DisplayHelper.GetNewUrlReportExtWithPagingPrefix (uri);
            string newUrl2 = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri);
            ViewBag.UrlSourceTable = newUrl2;

            //http://localhost:52494/Report/TradeHistoryDetail/xlm_btc
            //http://localhost:52494/api/TradeHistoryDetail/xlm_btc
            return View(output3);
        }

    }
}