using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//
//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    // [Route("api/OrderItemMain")]
    public class OrderItemMainController : Controller
    {


        private readonly ApplicationContext _context;
        private readonly OrderListInquiryQueryService _orderListInquiryService;
        private readonly RepoUser repoUser;
        private readonly RepoOrderList repoOrderList;

        public IUnitOfWork unitOfWork { get; private set; }
        public IMediator Mediator { get; }

        public OrderItemMainController(ApplicationContext context,
            OrderListInquiryQueryService orderListInquiryService,
            //OrderItemMainService orderItemMainService,
            RepoUser repoUser,
            RepoOrderList repoOrderList,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            this._context = context;
            this._orderListInquiryService = orderListInquiryService;
            this.repoUser = repoUser;
            this.repoOrderList = repoOrderList;
            this.unitOfWork = unitOfWork;
            Mediator = mediator;

        }





        [Route("/api/orderItemMain")]
        //[HttpGet]
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] InputTransactionRaw inputTransactionRaw)
        {


            
            try
            {
                var userName = User.Identity.Name;

                userName = userName ?? "N/A";
                // return Content($"username: {userName}");
                var command = new OrderItemQueueCommand()
                {
                    inputTransactionRaw = inputTransactionRaw,
                    userName = userName,
                    includeLog = true
                };
                OrderResult result = await Mediator.Send(command);
                if (result == null)
                {
                    return BadRequest("transaction server is down");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return BadRequest(ex.Message);
            }

        }

        //http://localhost:53252/api/orderItemMain/btc_usd
        [Route("/api/orderItemMain/{id}")]
        [HttpGet]

        public async Task<MvDetailSpotMarketItemContent> Get(string id)
        {
            var currencyPair = id;
            //var output2 = await this.repoOrderList.GetOrderList(id);
            //return output2;

            //var userName = User.Identity.Name;
            var command = new OrderListInquiryQuery()
            {
                //userName = userName,
                currencyPair = currencyPair
            };
            var result = await Mediator.Send(command);
            return result;
        }

    }
}


//private List<MvDetailSpotMarketItem> DemoLogic()
//{
//    var output = MvDetailSpotMarketItem.GenerateSample();

//    List<MvDetailSpotMarketItem> output2 = output.Select(x => new MvDetailSpotMarketItem()
//    {
//        Amount = x.Amount + 3,
//        OrderId = x.OrderId + 2,
//        Rate = x.Rate + 3
//    }).ToList();
//    return output2;
//}