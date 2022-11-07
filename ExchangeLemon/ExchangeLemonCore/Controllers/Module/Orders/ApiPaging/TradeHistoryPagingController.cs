using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// using Serilog;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main.Api
{
    public class TradeHistoryPagingController : Controller
    {
        private ApplicationContext _context;
        private readonly RepoGeneric repoGeneric;
        private readonly ILogger<TradeHistoryPagingController> logger;

        public TradeHistoryPagingController(ApplicationContext context,
        RepoGeneric repoGeneric, ILogger<TradeHistoryPagingController> logger)
        {
            this.logger = logger;
            this._context = context;
            this.repoGeneric = repoGeneric;
        }

        // [Route ("/api/tradeHistoryDetailPaging/{id}")]
        // [Route ("/api/tradeHistoryDetailPaging/{id}&counter={counter}")]
        [Authorize]
        [HttpGet]
        public async Task<PagingClass<MvDetailReportOrder>> List(string id, int counter = 1)
        {
            var currencyPair = id;
            var db = this._context;

            var uri = WebHelper.GetUrlToUri(Request);
            var newUrl = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri, counter);

            logger.LogInformation("newUrl : " + newUrl);
            var userName = User.Identity.Name;


            var output = await repoGeneric.GetHistoryOrders(userName,
                currencyPair, true);

         
            var result = new PagingClass<Order>();
            await result.PopulateQuery(output, counter, newUrl);

            var result2 = new PagingClass<MvDetailReportOrder>();
            PagingClass.Copy<Order, MvDetailReportOrder>(result, result2);
            result2.data = result.data
                .Select(x => new MvDetailReportOrder(x))
                .ToList();

            return result2;
        }
    }
}

// string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
// string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

// var scheme = Request.HttpContext.Request.Scheme;
// var host = Request.HttpContext.Request.Host;

// var new_counter = counter +1;
// var route = $"/api/tradeHistoryDetail/{id}&counter={new_counter}";

// var fullPath = $"{scheme}://{host}{route}";
// var newUrl = fullPath;

// http :// localhost:5000  /api/tradeHistoryDetail/{id}&counter={counter}"            

// Request.HttpContext.Request.Host
// http://localhost:5000
// var newUrl = UrlHelper.GetNewUrlReportApi(Request);

// var route = $"/api/tradeHistoryDetail/{id}&counter={counter+1}";

////[Produces("application/json")]
////[Consumes("application/json")]
////[Authorize]
//[HttpGet]
////public async Task<PagingClass<MvDetailReportOrder>> Get(int page, int perPage, int offset, string sorts = null)
//public async Task<PagingClassTwo<MvDetailSpotMarketItemString>> Get(int page, int perPage, int offset, string sorts = null)
//{
//    var id = "xlm_btc";
//    var currencyPair = id;
//    var db = this._context;

//    var req = HttpContext.Current.Request;
//    var a = req.Url.Authority;
//    var b = req.Url.LocalPath;

//    var newUrl = req.Url.Scheme + "://" + a + b;

//    Log.Information("newUrl : " + newUrl);
//    var userName = User.Identity.Name;
//    var output = await ReportGenerator.GetHistoryOrders(userName,
//        currencyPair, true, db);

//    //var result = new PagingClass<MvDetailReportOrder>();
//    var counter = page;
//    //result.Populate(output, counter, newUrl);

//    var output2 = output.Select(x => new MvDetailSpotMarketItemString()
//    {
//        Id = x.Id.ToString(),
//        //Rate = x.Rate.ToString()
//        Rate = "Abc"
//    }).ToList();
//    var result = new PagingClassTwo<MvDetailSpotMarketItemString>();
//    result.Populate(output2, counter, offset, itemPerPage: perPage);

//    return result;
//}