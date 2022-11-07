using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class OrderItemMainFunctionController : Controller
    {
        public OrderItemMainFunctionController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        [Route("/api/orderItemMainFunction")]
        //[HttpGet]
        [HttpPost]

        public async Task<IActionResult> Post()
        {
            var content = this.Request.Body;
            var sr = new StreamReader(content);
            var input = await sr.ReadToEndAsync();

            try
            {
                //RpcItem item = RpcHelper.ConvertBack(content2);
                IActionResult output = await Logic(input);
                return output;

            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return BadRequest(ex.Message);
            }

        }

        private async Task<IActionResult> Logic(string inputRaw)
        {
            var input = JsonConvert.DeserializeObject<RpcItem>(inputRaw);
            var contentRaw = input.content;


            OkObjectResult output;
            try
            {



                if (input.TypeContent == typeof(OrderItemQueueCommand).ToString())
                {
                    var command =
                        JsonConvert.DeserializeObject<OrderItemCommand>(contentRaw);
                    var result = await Mediator.Send(command);
                    output = Ok(result);
                }
                else if (input.TypeContent == typeof(CancelByIdQueueCommand).ToString())
                {
                    var command =
                        JsonConvert.DeserializeObject<CancelByIdCommand>(contentRaw);
                    var result = await Mediator.Send(command);
                    output = Ok(result);
                }
                else if (input.TypeContent == typeof(CancelByGuidQueueCommand).ToString())
                {
                    var command =
                        JsonConvert.DeserializeObject<CancelByGuidCommand>(contentRaw);
                    var result = await Mediator.Send(command);
                    output = Ok(result);
                }
                else if (input.TypeContent == typeof(CancelAllQueueCommand).ToString())
                {
                    var command =
                        JsonConvert.DeserializeObject<CancelAllCommand>(contentRaw);
                    var result = await Mediator.Send(command);
                    output = Ok(result);
                }
                else
                {
                    return BadRequest("No Mapping Queue Service");
                }
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return BadRequest(m);
            }

            return output;
        }


    }
}

//   private async Task<OrderResult> LogicTransaction(string raw)
//         {
//             var command =
//                JsonConvert.DeserializeObject<OrderItemCommand>(raw);
//             var result = await Mediator.Send(command);
//             return result;
//         }

//         private async Task<bool> LogicCancelById(string raw)
//         {
//             var command =
//                JsonConvert.DeserializeObject<CancelByIdCommand>(raw);
//             var result = await Mediator.Send(command);
//             return result;
//         }


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