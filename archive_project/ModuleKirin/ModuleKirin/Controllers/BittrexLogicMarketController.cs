using System;
using System.Linq;
// using System.Net.Http;
using System.Threading.Tasks;
// using System.Web.Http;
using BittrexOpenOrderSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BlueLight.Main;
using MediatR;

namespace BlueLight.KirinLogic
{

    public partial class BittrexLogicMarketController : Controller
    {
        private ApplicationContext _context;
        private readonly OrderItemCancelService event1;
        private readonly OrderItemMainService orderItemMainEvent;
        private readonly RepoUser repoUser;

        public ILogHelperMvc LogHelperMvc { get; }
        public LoggingExtContext LoggingExtContext { get; }
        public IMediator Mediator { get; }

        private readonly LogHelperObject _logHelperObject;

        public BittrexLogicMarketController(ApplicationContext context,
                            OrderItemCancelService event1,
                            OrderItemMainService orderItemMainEvent,
                            LoggingContext loggingContext,
                            RepoUser repoUser,
                            LogHelperObject logHelperObject,
                            ILogHelperMvc logHelperMvc,
                            LoggingExtContext LoggingExtContext,
                            IMediator mediator)
        {
            this._logHelperObject = logHelperObject;
            this._context = context;
            this.event1 = event1;
            this.orderItemMainEvent = orderItemMainEvent;
            this.repoUser = repoUser;
            this.LogHelperMvc = logHelperMvc;
            this.LoggingExtContext = LoggingExtContext;
            Mediator = mediator;
        }

        [HttpGet]

        //http://localhost:5000/api/v1/market/getopenorders?apikey=1&nonce=2&market=USDT-BTC 
        [Route("api/v1/market/getopenorders")]
        public async Task<OpenOrder> GetOpenOrders(string apikey, long nonce, string market)
        {
            var output = new OpenOrder();

            try
            {
                if (BittrexHelper.UseOldSystem)
                {
                    output = await OldLogicGetOpenOrders(market);
                }
                else
                {

                    var userName = BittrexHelper.GetUserName(Request);
                    //var currencyPair = market;
                    var currencyPair = BittrexHelper.EnsurePairName(market);

                    var repo = new RepoOpenOrder(this._context);
                    var userProfile = await _context.UserProfiles.FirstAsync(x => x.username == userName);
                    var output1 = await repo.GetOpenOrdersPerPairList(userProfile, currencyPair);

                    var result2 = output1.Select(x => new BittrexOpenOrderSchema.Result()
                    {
                        Uuid = x.Id,
                        Quantity = x.Amount,
                        QuantityRemaining = x.LeftAmount,
                        OrderType = BittrexHelper.LimitDisplay(x.IsBuy),
                        Limit = x.RequestRate,
                        Opened = x.CreatedDate
                    }).ToList();

                    output = new OpenOrder()
                    {
                        Result = result2,
                        Success = true,
                        Message = ""
                    };

                    if (output.Result.Count == 0)
                    {
                        output.Result = null;
                    }
                    await LogHelperMvc.SaveLog(output, Request);
                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }
            return output;

        }

        //[HttpGet]
        //// http://localhost:5000/api/v1/market/clearTemp
        //[Route("api/v1/market/clearTemp")]
        //public bool ClearTemp()
        //{
        //    TempRepo.ClearAll();
        //    return true;
        //}

        //http://localhost:5000/api/v1/market/cancel?apikey=1&nonce=2&uuid=c4c9de60-4738-41c6-8123-f4bfe9ec74a6 
        [HttpGet]
        [Route("api/v1/market/cancel")]
        public async Task<MarketResult> Cancel(string apikey, long nonce, string uuid)
        {
            await CaptureCancelLogId(uuid);

            
            var output = new MarketResult()
            {
                Success = false,
                Message = "",
                Result = null
            };

            try
            {
                if (BittrexHelper.UseOldSystem)
                {
                    var userName = BittrexHelper.GetUserName(Request);
                    output = await OldLogicCancel(uuid, apikey, nonce);

                }
                else
                {

                    var userName = BittrexHelper.GetUserName(Request);

                    //var id = int.Parse(uuid);
                    //var orderId = id;

                    //await event1.ExecuteFromOrder(orderId, userName);

                    var guidId = Guid.Parse(uuid);
                    try
                    {
                        //await event1.ExecuteFromOrderGuid(guidId, userName);
                        var command = new CancelByGuidCommand()
                        {
                            guidId = guidId,
                            userNameLogCapture = User.Identity.Name,
                            //includeLog = true
                        };
                        await Mediator.Send(command);
                    }
                    catch(Exception ex)
                    {
                        var m = ex.Message;
                    }

                    output.Success = true;

                    await LogHelperMvc.SaveLog(output, Request);
                }

            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;

        }

        private async Task CaptureCancelLogId(string uuid)
        {
            var logItem = new LogItem()
            {
                SessionId = uuid,
                //Content = "BittrexCancel",
                ModuleName = "BittrexCancel",
            };
            LoggingExtContext.LogItems.Add(logItem);
            await LoggingExtContext.SaveChangesAsync();
        }

        //http://localhost:5000/api/v1/market/selllimit?apikey=409534a6876044b393bb675915a6f40a&nonce=1522950315&market=USDT-BTC&quantity=0.05892086&rate=6709.5545 
        [Route("api/v1/market/selllimit")]
        [HttpGet]

        public async Task<MarketResult> SellLimit(string apikey, long nonce, string market, double quantity, double rate)
        {
            var output = new MarketResult();

            try
            {
                if (BittrexHelper.UseOldSystem)
                {
                    var userName = BittrexHelper.GetUserName(Request);
                    //output = await OldLogicSellLimit(apikey, nonce, market, quantity, rate, userName);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);

                    var market2 = BittrexHelper.EnsurePairName(market);
                    await BittrexHelper.EnsureHasUser(userName, _context);

                    InputTransactionRaw input = new InputTransactionRaw()
                    {
                        current_pair = market2,
                        amount = quantity.ToString(),
                        mode = "sell",
                        rate = rate.ToString()

                    };




                    //var event1 = this.orderItemMainEvent;
                    //event1.Request = Request;
                    //await event1.DirectExecute(input, userName);

                    var command = new OrderItemQueueCommand()
                    {
                        inputTransactionRaw = input,
                        userName = userName,
                        //includeLog = true
                    };
                    OrderResult result = await Mediator.Send(command);

                    output = MarketResult.Generate(result.GuidId);

                    await LogHelperMvc.SaveLog(output, Request);
                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;

        }

        //http://localhost:5001/api/v1/market/buylimit?apikey=409534a6876044b393bb675915a6f40a&nonce=1522950315&market=USDT-BTC&quantity=0.05892086&rate=6709.5545 
        [Route("api/v1/market/buylimit")]
        [HttpGet]
        public async Task<MarketResult> BuyLimit(string apikey, long nonce, string market, double quantity, double rate)
        {

            var output = new MarketResult();

            try
            {
                if (BittrexHelper.UseOldSystem)
                {
                    var userName = BittrexHelper.GetUserName(Request);
                    //output = await OldLogicBuyLimit(apikey, nonce, market, quantity, rate, userName);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);

                    var market2 = BittrexHelper.EnsurePairName(market);
                    await BittrexHelper.EnsureHasUser(userName, _context);


                    InputTransactionRaw input = new InputTransactionRaw()
                    {
                        current_pair = market2,
                        amount = quantity.ToString(),
                        mode = "buy",
                        rate = rate.ToString()

                    };

                    

                    string currency_pair = market;

                    //var event1 = this.orderItemMainEvent;
                    //event1.Request = Request;
                    //await event1.DirectExecute(input, userName);

                    var command = new OrderItemCommand()
                    {
                        inputTransactionRaw = input,
                        userName = userName,
                        //includeLog = true
                    };
                    OrderResult result = await Mediator.Send(command);

                    output = MarketResult.Generate(result.GuidId);

                    await LogHelperMvc.SaveLog(output, Request);
                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;

        }

    }

    public partial class BittrexLogicMarketController
    {

        //async Task<MarketResult> OldLogicBuyLimit(string apikey, long nonce, string market,
        //    double quantity, double rate, string userName)
        //{
        //    await Task.Delay(0);
        //    var item = new LogTransaction()
        //    {
        //        apikey = apikey,
        //        nonce = nonce,
        //        market = market,
        //        quantity = quantity,
        //        rate = rate,
        //        typeTransaction = "buy"
        //    };


        //    await _logHelperObject.SaveObject(item);

        //    var t = MarketResult.GenerateModel();
        //    TempRepo.AddFakeTransaction(item, t.Item2);

        //    ////var name = userName + "-" + market;
        //    //var name = userName;
        //    //var amount = quantity * rate;
        //    //TempRepo.AdjustBalance(name, amount);

        //    var name = userName;
        //    var amount = quantity * rate;
        //    TempRepo.AdjustBalance(name, amount * +1, "btc");
        //    TempRepo.AdjustBalance(name, amount * -1, "usd");

        //    var output = t.Item1;
        //    return output;

        //}
        //async Task<MarketResult> OldLogicSellLimit(string apikey, long nonce, string market,
        //    double quantity, double rate, string userName)
        //{
        //    await Task.Delay(0);
        //    var item = new LogTransaction()
        //    {
        //        apikey = apikey,
        //        nonce = nonce,
        //        market = market,
        //        quantity = quantity,
        //        rate = rate,
        //        typeTransaction = "sell"
        //    };

        //    var t = MarketResult.GenerateModel();
        //    TempRepo.AddFakeTransaction(item, t.Item2);


        //    await _logHelperObject.SaveObject(item);

        //    var name = userName;
        //    var amount = quantity * rate;
        //    TempRepo.AdjustBalance(name, amount * -1, "btc");
        //    TempRepo.AdjustBalance(name, amount * +1, "usd");

        //    var output = t.Item1;
        //    return output;
        //}
        async Task<MarketResult> OldLogicCancel(string uuid, string apikey, long nonce)
        {
            await Task.Delay(0);
            var item = new LogTransaction()
            {
                apikey = apikey,
                nonce = nonce,
                market = uuid,
                quantity = -1,
                rate = -1,
                typeTransaction = "cancel"
            };

            //TempRepo.RemoveFakeTransaction(uuid);


            await _logHelperObject.SaveObject(item);

            var t = MarketResult.GenerateCancelModel();
            return t;
        }
        async Task<OpenOrder> OldLogicGetOpenOrders(string market)
        {
            await Task.Delay(0);

            var output = new OpenOrder();
            //var option = BittrexHelper.GenerateCredential();
            //using (var client = new BittrexClient(option))
            //{

            //    var result3 = await client.GetOpenOrdersAsync(market);
            //    var output2 = result3.RawContent;
            //    WelcomeBittrexOpenOrders items = TempRepo.GetAll();
            //    var output3 = JsonConvert.SerializeObject(items);
            //    output = OpenOrder.FromJson(output2);

            //    if (output.Result.Count == 0)
            //    {
            //        output.Result = null;
            //    }
            //    await LogHelperMvc.SaveLog(output, Request);

            //}

            return output;
        }

    }
}