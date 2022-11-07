using System;
using System.Collections.Generic;
using System.Linq;
// using System.Net.Http;
using System.Threading.Tasks;
// using System.Web.Http;
using BittrexMarketHistorySchema;
using BittrexTickerSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLibrary;
using BlueLight.Main;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace BlueLight.KirinLogic
{

    public partial class BittrexLogicPublicController : Controller
    {

        private ApplicationContext _context;
        private readonly IOrderListInquiryContextService orderListInquiryContextService;

        public ILogHelperMvc LogHelperMvc { get; }
        public SignalDashboard _signalDashboard { get; }
        public PulseInsertService PulseInsertService { get; }
        //public PulseHub PulseHub { get; }

        private readonly IHubContext<PulseHub> pulseHubContext;
        public BittrexLogicPublicController(ApplicationContext context,
            LoggingContext loggingContext,
            SignalDashboard dashboardService,
            ILogHelperMvc logHelperMvc,
            IOrderListInquiryContextService orderListInquiryContextService,
             IHubContext<PulseHub> pulseHubContext,
             PulseInsertService pulseInsertService
            //PulseHub pulseHub
            )
        {
            this._context = context;
            this._signalDashboard = dashboardService;
            this.LogHelperMvc = logHelperMvc;
            this.orderListInquiryContextService = orderListInquiryContextService;
            this.pulseHubContext = pulseHubContext;
            PulseInsertService = pulseInsertService;
            //PulseHub = pulseHub;
        }

        async Task SendSignal()
        {
            var moduleName = "kirin-pulse-1";
            Pulse output = await PulseInsertService.InsertOrUpdate(moduleName);

            IHubClients Clients = pulseHubContext.Clients;
            await Clients.All.SendAsync("listenPulse", moduleName);

            var output2 = JsonConvert.SerializeObject(output);
            await Clients.All.SendAsync("listenPulseDetail", output2);
        }

        // https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-DOGE
        //x http://localhost:5000/api/v1/public/getmarkethistory?market=BTC-USD
        [Route("api/v1/public/getmarkethistory")]
        [Throttle(Name = "ThrottleKirin", Seconds = 2)]
        [HttpGet]
        public async Task<MarketHistory> GetMarketHistory(string market)
        //public async Task<string> GetMarketHistory(string market)
        {


            // await _signalDashboard.Submit("gekko");
            await _signalDashboard.Submit("gekko-inquiry-market-history");

            var output = new MarketHistory();

            var useOldSystem = BittrexHelper.UseOldSystem;


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


                    //List<Order> rawItems = await GetOrders();
                    currentPair = "btc_usd";
                    var rawItems = await orderListInquiryContextService.GetItemsKirin(currentPair);

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
                    await SendSignal();

                }
            }
            catch (Exception ex)
            {
                await LogHelperMvc.SaveError(output, Request, ex);
            }

            return output;

        }

        //private async Task<List<Order>> GetOrders()
        //{
        //    IOrderedQueryable<Order> queryOrders = _context.Orders
        //        .Where(x => x.IsOpenOrder == false && x.CurrencyPair == "btc_usd")
        //        .Take(50)
        //        .OrderByDescending(x => x.Id);
        //    List<Order> orders = await queryOrders
        //        .ToListAsync();

        //    return orders;
        //}

        //x http://localhost:5000/api/v1/public/getticker?market=BTC-USD
        [HttpGet]
        [Route("api/v1/public/getticker")]
        public async Task<Ticker> GetTicker(string market)
        {
            var output = new Ticker();
            await _signalDashboard.Submit("gekko-inquiry-ticker");
            try
            {
                var useOldSystem = BittrexHelper.UseOldSystem;

                //useOldSystem = true;
                if (useOldSystem)
                {
                    output = await OldLogicGetTicker(market);
                }
                else
                {
                    var currentPair = BittrexHelper.EnsurePairName(market);
                    var item = await this._context.SpotMarkets.FirstAsync(x => x.CurrencyPair == currentPair);

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