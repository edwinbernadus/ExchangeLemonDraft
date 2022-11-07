using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// ;

namespace BlueLight.Main
{

    [Authorize]

    public class OpenOrderController : Controller
    {

        private ApplicationContext _context;
        private readonly OrderItemCancelService eventCancelOneOrder;
        private readonly RepoUser repoUser;

        public OrderItemCancelAllService eventCancelAllOrder { get; }
        public IMediator Mediator { get; }

        private readonly LogHelperBot _logHelperBot;

        public OpenOrderController(ApplicationContext context,
                    OrderItemCancelAllService event1,
                    OrderItemCancelService event2,
                    RepoUser repoUser, LogHelperBot logHelperBot,
                    IMediator mediator)
        {
            _logHelperBot = logHelperBot;
            this._context = context;
            this.eventCancelAllOrder = event1;
            this.eventCancelOneOrder = event2;
            this.repoUser = repoUser;
            Mediator = mediator;
        }

        [AllowAnonymous]

        public ActionResult Index()
        {

            var uri = WebHelper.GetUrlToUri(Request);

            string newUrl2 = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri);
            // string newUrl2 = DisplayHelper.GetNewUrlReportExtWithPagingPrefix (uri);
            ViewBag.UrlSourceTable = newUrl2;
            return View();
        }

        public async Task<ActionResult> Cancel(int id)
        {


            var userName = User.Identity.Name;

            var LogHelperBot = this._logHelperBot;
            await LogHelperBot.Save(id, userName);

            var orderId = id;
            //await eventCancelOneOrder.ExecuteFromOrder(orderId, userName);

            var command = new CancelByIdQueueCommand()
            {
                orderId = orderId,
                userNameLogCapture = User.Identity.Name,
                //includeLog = true
            };
            await Mediator.Send(command);

            return RedirectToAction("index");
        }

        public async Task<ActionResult> CancelAll()
        {

            var userName = User.Identity.Name;
            var userProfile = await repoUser.GetUser(userName);
            var userProfileLiteMode = userProfile.LiteMode();
            //await eventCancelAllOrder.Execute(userProfile.LiteMode());

            var command = new CancelAllQueueCommand()
            {
                UserId = userProfileLiteMode.UserId,
                UserName = userProfileLiteMode.UserName
            };
            await Mediator.Send(command);

            return RedirectToAction("index");
        }
    }
}
//public async Task<ActionResult> List(int counter = 1)
//{
//    var _context = new DBContext();
//    var uri = this.Request.Url;
//    var newUrl = DisplayHelper.GetNewUrlReportExt(uri,counter);
//    var output = await Generate();

//    var result = new PagingClass<MvPendingOrderExt>();
//    result.Populate(output, counter, newUrl);

//    var result2 = Json(result,JsonRequestBehavior.AllowGet);
//    return result2;
//}

//async Task<IEnumerable<MvPendingOrderExt>> Generate()
//{
//    var _context = new DBContext();
//    var userName = User.Identity.Name;

//    var output = await ReportGenerator.GetOpenOrders(userName,
//        _context);

//    var output2 = output.Select(x => new MvPendingOrderExt()
//    {
//        Amount = x.Amount,
//        CurrencyPair = x.CurrencyPair,
//        Buy = x.IsBuy,
//        //@BlueLight.Main.DisplayHelper.RateDisplay(item.Rate)
//        DisplayRate = DisplayHelper.RateDisplay(x.Rate),
//        Left = x.LeftAmount,
//        Id = x.Id
//    });

//    return output2;

//}

//public async Task<bool> CancelAll_Logic(string userName)
//{
//    var output = await ReportGenerator.GetOpenOrders(userName,
//            _context);

//    var result = false;
//    var list = output.Select(x => x.Order).ToList();
//    foreach (var i in list)
//    {
//        //var userName = User.Identity.Name;
//        var event1 = new OrderItemCancelEvent()
//        {
//            id = i.Id.ToString(),
//            _context = _context,
//            userName = userName
//        };
//        await event1.ExecuteFromIdNumber();
//        result = true;
//    }
//    return result;

//}