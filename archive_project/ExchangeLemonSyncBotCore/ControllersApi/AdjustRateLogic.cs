//using System;
//using BlueLight.Main;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
////using System.Data.Entity;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace BlueLight.Main {
//    public class AdjustRateLogic {
//        public string currencyPair;
//        public double bfxLastRate;
//        public ApplicationContext _context;
//        private readonly OrderItemMainService orderItemMainEvent;
//        public double defaultBtc = 0.1;
//        public double lemonLastRate;

//        internal LogHelperStopWatch logHelper;
//        internal HttpRequest request;
//        internal UserProfile userProfile;

//        public string shouldBuyOrSell { get; private set; }
//        public double total { get; private set; }
//        //public string userName { get; internal set; }

//        public AdjustRateLogic (ApplicationContext _context,
//            OrderItemMainService orderItemMainEvent) {
//            this._context = _context;
//            this.orderItemMainEvent = orderItemMainEvent;
//        }

//        public AdjustRateLogic (int initForUnitTest) {

//        }

//        internal async Task Execute () {
//            this.lemonLastRate = await GetLastRate ();
//            await logHelper.Save ("getLastRate");

//            if (lemonLastRate == this.bfxLastRate) {
//                this.total = -1;
//                return;
//            }

//            this.shouldBuyOrSell = ShouldBuyOrSell ();
//            await logHelper.Save ("getMode");

//            this.total = GetTotalOrderAmount ();
//            await logHelper.Save ("getTotalOrderAmount");

//            await AdjustBasedOnAmount ();
//            await logHelper.Save ("adjustBasedOnAmount");

//            await SetSell ();
//            await logHelper.Save ("setSell");

//            await SetBuy ();
//            await logHelper.Save ("setBuy");

//            await ValidationIfHasHold ();
//            await logHelper.Save ("validationIfHasHold");
//        }

//        private async Task ValidationIfHasHold () {
//            await Task.Delay (0);

//            //throw new NotImplementedException();
//        }

//        private async Task<double> GetLastRate () {
//            var spotMarket = await _context.SpotMarkets
//                .FirstAsync (x => x.CurrencyPair == currencyPair);
//            var lastRate = spotMarket.LastRate;
//            return lastRate;
//        }

//        private double GetTotalOrderAmount () {
//            IQueryable<Order> items;

//            var query = _context.Orders
//                //.Where(x => x.CurrencyPair == currencyPair && x.IsCancelled == false);
//                .Where (x => x.CurrencyPair == currencyPair && x.IsOpenOrder);

//            if (shouldBuyOrSell == "buy") {
//                items = query.Where (x => x.RequestRate >= bfxLastRate);
//            } else {
//                items = query.Where (x => x.RequestRate <= bfxLastRate);
//            }

//            var total = query.Sum (x => x.LeftAmount);
//            return total;

//        }

//        ////var total = items
//        ////    .GroupBy (x => x.CurrencyPair)
//        ////    .Select (y => new {
//        ////        Result = y.Sum (z => z.LeftAmount),
//        ////            Currency = y.Key
//        ////    }).ToList ();

//        //var item = total.FirstOrDefault ();
//        //var output = 0.0d;
//        //if (item != null) {
//        //    output = item.Result;
//        //}

//        //return this.total;

//        public string ShouldBuyOrSell () {
//            var mode = "buy";
//            if (bfxLastRate < lemonLastRate) {
//                mode = "sell";
//            }
//            return mode;
//        }

//        private async Task SetBuy () {
//            //var controller = new OrderItemMainController();
//            InputTransactionRaw input = new InputTransactionRaw () {
//                current_pair = currencyPair,
//                amount = this.defaultBtc.ToString (),
//                mode = "buy",
//                rate = bfxLastRate.ToString ()

//            };

//            // var event1 = new OrderItemMainEvent () {
//            //     Request = this.request,
//            //     _context = this._context
//            // };
//            var event1 = this.orderItemMainEvent;
//            event1.Request = this.request;

//            //var userProfile = await RepoUser.GetUser(_context, userName);
//            await event1.Execute (input, userProfile);

//            //await controller.OrderItemLogic(input, userName);
//            //await controller.Post(input);
//        }

//        private async Task SetSell () {
//            InputTransactionRaw input = new InputTransactionRaw () {
//                current_pair = currencyPair,
//                amount = this.defaultBtc.ToString (),
//                mode = "sell",
//                rate = bfxLastRate.ToString ()

//            };

//            // var event1 = new OrderItemMainEvent () {
//            //     Request = this.request,
//            //     _context = this._context
//            // };
//            var event1 = this.orderItemMainEvent;
//            event1.Request = this.request;
//            await event1.Execute (input, userProfile);

//        }

//        private async Task AdjustBasedOnAmount () {
//            //var controller = new OrderItemMainController();
//            InputTransactionRaw input = new InputTransactionRaw () {
//                current_pair = currencyPair,
//                amount = this.total.ToString (),
//                //mode = getOpposite (mode),
//                mode = this.shouldBuyOrSell,
//                //mode = mode,
//                rate = this.bfxLastRate.ToString ()

//            };

//            // var event1 = new OrderItemMainEvent () {
//            //     Request = this.request,
//            //     _context = this._context
//            // };
//            var event1 = this.orderItemMainEvent;
//            event1.Request = this.request;
//            //var userProfile = await RepoUser.GetUser(_context, userName);
//            await event1.Execute (input, userProfile);
//            //await controller.OrderItemLogic(input,userName);
//        }

//        //private string getOpposite (string mode) {
//        //    if (mode == "buy") {
//        //        return "sell";
//        //    } else if (mode == "sell") {
//        //        return "buy";
//        //    } else {
//        //        return "nothing";
//        //    }
//        //}
//    }
//}

////var result = await items.ToListAsync();
////var total = result.Sum(x => x.LeftAmount);

////var items2 = await items.ToListAsync();
////var total2 = items2.Sum(x => x.LeftAmount);

////var total = items.Sum(x => x.LeftAmount);

////var sums = dc.Deliveries
//// .Where(d => d.TripDate == DateTime.Now)
//// .GroupBy(d => d.TripDate)
//// .Select(g =>
////     new
////     {
////         Rate = g.Sum(s => s.Rate),
////         AdditionalCharges = g.Sum(s => s.AdditionalCharges)
////     });
////var w2 = await query.ToListAsync();
//////var w3 = w2.Where(x => x.IsOpenOrder).ToList();
////var w3 = w2;
////var items3 = w3;

////var items2 = OrderHelper.IsOpenOrder(items);
////var items3 = await items2.ToListAsync();

////var itemsTest1 = items.ToList();
////var itemsTest2 = OrderHelper.IsOpenOrderListVersion(itemsTest1);
////var itemsTest3 = itemsTest2.ToList();

////OrderHelper.IsValid(w3, items3);
////OrderHelper.IsValid(w3, itemsTest3);