//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueLight.Main;
//using BlueLight.Main;
using Xunit;

namespace BlueLight.Main.Tests {
    //[TestClass()]
    public class OrderListFactoryTests {
        //[TestMethod()]
        [Fact]
        public void GetItemsBuyLogicTest () {
            var factory = new OrderListInquiryQueryService ();
            var take = 5;
            var currentPair = "btc_usd";

            var itemsInput = Enumerable.Range (1, 100).Select (x => new OrderInput () {
                IsBuy = true,
                    CurrencyPair = currentPair,
                    Amount = 1,
                    RequestRate = x
            });

            var items0 = itemsInput.Select (x => OrderFactory.Generate (x)).ToList ();
            var items = items0.AsQueryable ();
            //var items = new List<Order>();

            var col1 = factory.GetItemsBuyLogic (items, take, currentPair);
            var col2 = col1.Select (x => x.RequestRate).ToList ();
            var total = col1.Count ();

            Assert.Equal (100, col2.Max ());
            Assert.Equal (96, col2.Min ());
            Assert.Equal (5, total);

        }

        [Fact]
        public void GetItemsSellLogicTest () {
            var factory = new OrderListInquiryQueryService ();
            var take = 5;
            var currentPair = "btc_usd";

            var itemsInput = Enumerable.Range (101, 100).Select (x => new OrderInput () {
                IsBuy = false,
                    CurrencyPair = currentPair,
                    Amount = 1,
                    RequestRate = x
            }).ToList ();

            var items0 = itemsInput.Select (x => OrderFactory.Generate (x)).ToList ();
            var items = items0.AsQueryable ();
            //var items = new List<Order>();

            var col0 = factory.GetItemsSellLogic (items, take, currentPair);
            var col1 = col0.ToList();

            var col2 = col1.Select (x => x.RequestRate).ToList ();
            var total = col1.Count ();

            Assert.Equal (105, col2.Max ());
            Assert.Equal (101, col2.Min ());
            Assert.Equal (5, total);

        }
    }
}