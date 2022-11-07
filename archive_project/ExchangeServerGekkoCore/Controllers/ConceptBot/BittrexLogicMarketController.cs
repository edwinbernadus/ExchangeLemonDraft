using System;
using System.Linq;
// using System.Net.Http;
using System.Threading.Tasks;
// using System.Web.Http;
using BittrexOpenOrderSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public partial class BittrexLogicMarketController : Controller
    {
        private ApplicationContext _context;
        private readonly OrderItemCancelService event1;
        private readonly OrderItemMainService orderItemMainEvent;
        private readonly RepoUser repoUser;

        public LogHelperMvc LogHelperMvc { get; }

        public BittrexLogicMarketController(ApplicationContext context,
            OrderItemCancelService event1,
            OrderItemMainService orderItemMainEvent,
            RepoUser repoUser)
        {
            this._context = context;
            this.event1 = event1;
            this.orderItemMainEvent = orderItemMainEvent;
            this.repoUser = repoUser;
            this.LogHelperMvc = new LogHelperMvc(_context);
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

                    var repo = new RepoOrderReadOnly(this._context);
                    var userProfile = _context.UserProfiles.First(x => x.username == userName);
                    var output1 = repo.GetOpenOrders(userProfile, currencyPair);

                    //{ "success":true,"message":"","result":[{"uuid":null,"orderUuid":"54a55ce7-562a-4af6-ac0c-b06559bc3dc8","exchange":"BTC-ADA","orderType":"LIMIT_SELL","quantity":2000.0,"quantityRemaining":2000.0,"limit":3.853E-05,"commissionPaid":0,"price":0,"pricePerUnit":null,"opened":"2018-04-16T07:05:18.023+00:00","closed":null,"cancelInitiated":false,"immediateOrCancel":false,"isConditional":false,"condition":"NONE","conditionTarget":null}]}
                    var result2 = output1.Select(x => new BittrexOpenOrderSchema.Result()
                    {
                        //Uuid = x.Id.ToString(),
                        Uuid = x.Id,
                        Quantity = x.Amount,
                        QuantityRemaining = x.LeftAmount,
                        //PricePerUnit = x.Rate,
                        //OrderType = x.IsBuy ? "LIMIT_BUY" : "LIMIT_SELL",
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

        [HttpGet]
        // http://localhost:5000/api/v1/market/clearTemp
        [Route("api/v1/market/clearTemp")]
        public bool ClearTemp()
        {
            TempRepo.ClearAll();
            return true;
        }

        //http://localhost:5000/api/v1/market/cancel?apikey=1&nonce=2&uuid=c4c9de60-4738-41c6-8123-f4bfe9ec74a6 
        [HttpGet]
        [Route("api/v1/market/cancel")]
        public async Task<MarketResult> Cancel(string apikey, long nonce, string uuid)
        {
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

                    var id = int.Parse(uuid);
                    var order = await _context.Orders
                        .Include(x => x.UserProfile)
                        .ThenInclude(x => x.UserProfileDetails)
                        .FirstAsync(x => x.Id == id);
                    // var event1 = new OrderItemCancelEvent () {
                    //     //id = uuid,
                    //     _context = this._context,
                    //     userNameLogCapture = userName
                    // };
                    // await event1.ExecuteFromOrder (order);
                    await event1.ExecuteFromOrder(order, userName);
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
                    output = await OldLogicSellLimit(apikey, nonce, market, quantity, rate, userName);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);

                    var market2 = BittrexHelper.EnsurePairName(market);
                    await BittrexHelper.EnsureHasUser(userName, _context);

                    //var input = new InputTransactionSell () {
                    //    Amount = quantity.ToString (),
                    //    //mode = "sell",
                    //    CurrencyPair = market2,
                    //    Rate = rate.ToString ()
                    //};

                    InputTransactionRaw input = new InputTransactionRaw()
                    {
                        current_pair = market2,
                        amount = quantity.ToString(),
                        mode = "sell",
                        rate = rate.ToString()

                    };

                    var userProfile = await repoUser.GetUser(userName);

                    //var event1 = new OrderItemSellEvent () {
                    //    input = input,
                    //    userProfile = userProfile,
                    //    _context = this._context,
                    //};

                    //await event1.Execute ();

                    // var event1 = new OrderItemMainEvent () {
                    //     Request = Request,
                    //     _context = this._context
                    // };
                    var event1 = this.orderItemMainEvent;
                    event1.Request = Request;
                    await event1.Execute(input, userProfile);

                    output = MarketResult.Generate(event1.ResultGuidId);

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
                    output = await OldLogicBuyLimit(apikey, nonce, market, quantity, rate, userName);
                }
                else
                {
                    var userName = BittrexHelper.GetUserName(Request);

                    var market2 = BittrexHelper.EnsurePairName(market);
                    await BittrexHelper.EnsureHasUser(userName, _context);

                    //var input = new InputTransactionBuy () {
                    //    Amount = quantity.ToString (),
                    //    CurrencyPair = market2,
                    //    Rate = rate.ToString ()
                    //};

                    InputTransactionRaw input = new InputTransactionRaw()
                    {
                        current_pair = market2,
                        amount = quantity.ToString(),
                        mode = "buy",
                        rate = rate.ToString()

                    };

                    var userProfile = await repoUser.GetUser(userName);

                    string currency_pair = market;
                    //var event1 = new OrderItemBuyEvent () {
                    //    input = input,
                    //    _context = this._context,
                    //    userProfile = userProfile
                    //};
                    //await event1.Execute ();

                    // var event1 = new OrderItemMainEvent () {
                    //     Request = Request,
                    //     _context = this._context
                    // };
                    var event1 = this.orderItemMainEvent;
                    event1.Request = Request;
                    await event1.Execute(input, userProfile);

                    output = MarketResult.Generate(event1.ResultGuidId);

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

        async Task<MarketResult> OldLogicBuyLimit(string apikey, long nonce, string market,
            double quantity, double rate, string userName)
        {
            await Task.Delay(0);
            var item = new LogTransaction()
            {
                apikey = apikey,
                nonce = nonce,
                market = market,
                quantity = quantity,
                rate = rate,
                typeTransaction = "buy"
            };

            var LogHelper = new LogHelperObject(_context);
            LogHelper.SaveObject(item);

            var t = MarketResult.GenerateModel();
            TempRepo.AddFakeTransaction(item, t.Item2);

            ////var name = userName + "-" + market;
            //var name = userName;
            //var amount = quantity * rate;
            //TempRepo.AdjustBalance(name, amount);

            var name = userName;
            var amount = quantity * rate;
            TempRepo.AdjustBalance(name, amount * +1, "btc");
            TempRepo.AdjustBalance(name, amount * -1, "usd");

            var output = t.Item1;
            return output;

        }
        async Task<MarketResult> OldLogicSellLimit(string apikey, long nonce, string market,
            double quantity, double rate, string userName)
        {
            await Task.Delay(0);
            var item = new LogTransaction()
            {
                apikey = apikey,
                nonce = nonce,
                market = market,
                quantity = quantity,
                rate = rate,
                typeTransaction = "sell"
            };

            var t = MarketResult.GenerateModel();
            TempRepo.AddFakeTransaction(item, t.Item2);

            var LogHelper = new LogHelperObject(_context);
            LogHelper.SaveObject(item);

            var name = userName;
            var amount = quantity * rate;
            TempRepo.AdjustBalance(name, amount * -1, "btc");
            TempRepo.AdjustBalance(name, amount * +1, "usd");

            var output = t.Item1;
            return output;
        }
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

            TempRepo.RemoveFakeTransaction(uuid);

            var LogHelper = new LogHelperObject(_context);
            LogHelper.SaveObject(item);

            var t = MarketResult.GenerateCancelModel();
            return t;
        }
        async Task<OpenOrder> OldLogicGetOpenOrders(string market)
        {

            var output = new OpenOrder();
            var option = BittrexHelper.GenerateCredential();
            using (var client = new BittrexClient(option))
            {

                var result3 = await client.GetOpenOrdersAsync(market);
                var output2 = result3.RawContent;
                WelcomeBittrexOpenOrders items = TempRepo.GetAll();
                var output3 = JsonConvert.SerializeObject(items);
                output = OpenOrder.FromJson(output2);

                if (output.Result.Count == 0)
                {
                    output.Result = null;
                }
                await LogHelperMvc.SaveLog(output, Request);

            }

            return output;
        }

    }
}