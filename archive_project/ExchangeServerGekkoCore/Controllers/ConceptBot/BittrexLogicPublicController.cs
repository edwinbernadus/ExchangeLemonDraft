using System;
using System.Linq;
// using System.Net.Http;
using System.Threading.Tasks;
// using System.Web.Http;
using BittrexMarketHistorySchema;
using BittrexTickerSchema;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.Main
{

    public partial class BittrexLogicPublicController : Controller
    {

        private ApplicationContext _context;

        public LogHelperMvc LogHelperMvc { get; }
        public SignalDashboard dashboardService { get; }

        public BittrexLogicPublicController(ApplicationContext context,
            SignalDashboard dashboardService)
        {
            this._context = context;
            this.dashboardService = dashboardService;
            this.LogHelperMvc = new LogHelperMvc(_context);
        }

        // https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-DOGE
        //x http://localhost:5000/api/v1/public/getmarkethistory?market=BTC-USD
        [Route("api/v1/public/getmarkethistory")]
        [HttpGet]
        public async Task<MarketHistory> GetMarketHistory(string market)
        //public async Task<string> GetMarketHistory(string market)
        {

            // var dashboardService = new DashboardService (_context);
            await dashboardService.SubmitDashboardDefault("gekko");
            var output = new MarketHistory();

            var useOldSystem = BittrexHelper.UseOldSystem;

            //TODO 001: force old system
            //useOldSystem = true;
            try
            {
                if (useOldSystem)
                {
                    output = await OldLogicGetMarketHistory(market);
                }
                else
                {
                    var currentPair = BittrexHelper.EnsurePairName(market);

                    // var orders1 = _context.Orders
                    // .Take(50)
                    // .OrderByDescending(x => x.Id).ToList();
                    var orders = _context.Orders
                        .Where(x => x.IsOpenOrder == false &&
                        x.CurrencyPair == "btc_usd")
                        .Take(50)
                        .OrderByDescending(x => x.Id)
                        .ToList();

                    var rawItems = orders;

                    {
                        //var orders2 = _context.Orders.Take(50).OrderByDescending(x => x.Id);
                        //var orders3 = OrderHelper.IsOpenOrder(orders2, false);
                        //var rawItems2 = orders3.ToList();
                        //OrderHelper.IsValid(rawItems2, rawItems);
                    }

                    var items = rawItems
                        .Select(x => new BittrexMarketHistorySchema.Result()
                        {
                            Id = x.Id,
                            FillType = x.IsFillComplete ?
                               FillType.Fill :
                               FillType.PartialFill,
                            OrderType = x.IsBuy ?
                               OrderType.Buy :
                               OrderType.Sell,
                            Price = x.RequestRate,
                            Quantity = x.Amount,
                            TimeStamp = x.CreatedDate,
                            Total = x.Amount * x.RequestRate

                        }).ToList();
                    output = new MarketHistory()
                    {
                        Message = "",
                        Success = true,
                        Result = items
                    };

                    await LogHelperMvc.SaveLog(output, Request);

                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;

        }

        //x http://localhost:5000/api/v1/public/getticker?market=BTC-USD
        [HttpGet]
        [Route("api/v1/public/getticker")]
        public async Task<Ticker> GetTicker(string market)
        {
            var output = new Ticker();

            try
            {
                var useOldSystem = BittrexHelper.UseOldSystem;
                //TODO 001: force old system
                //useOldSystem = true;
                if (useOldSystem)
                {
                    output = await OldLogicGetTicker(market);
                }
                else
                {
                    var currentPair = BittrexHelper.EnsurePairName(market);
                    var item = this._context.SpotMarkets.First(x => x.CurrencyPair == currentPair);

                    output = new Ticker()
                    {
                        Success = true,
                        Message = "",
                        Result = new BittrexTickerSchema.Result()
                        {
                            Ask = item.LastRate,
                            Bid = item.LastRate,
                            Last = item.LastRate,
                        }
                    };

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

    public partial class BittrexLogicPublicController
    {

        async Task<Ticker> OldLogicGetTicker(string market)
        {
            var output = new Ticker();
            using (var client = new BittrexClient())
            {
                var result3 = await client.GetTickerAsync(market);
                var output2 = result3.RawContent;
                output = Ticker.FromJson(output2);
                await LogHelperMvc.SaveLog(output, Request);
            }
            return output;
        }
        public async Task<MarketHistory> OldLogicGetMarketHistory(string market)
        {
            var output = new MarketHistory();
            //LOGIC
            using (var client = new BittrexClient())
            {
                var result3 = await client.GetMarketHistoryAsync(market);
                var output2 = result3.RawContent;
                output = MarketHistory.FromJson(output2);
                var outputText = output2;
                await LogHelperMvc.SaveLog(output, Request);

                //return output2;
                //return output;

            }

            return output;
        }
    }

}