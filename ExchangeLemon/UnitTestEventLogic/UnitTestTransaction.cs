using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using BlueLight.Main;
using ExchangeLemonCore.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestTransaction
    {


        [Fact]
        public async Task Buy()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };




            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var userProfile = new UserProfile()
            {
                username = "hello"
            };
            userProfile.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(userProfile);
            context.UserProfiles.Add(userProfile);
            await context.SaveChangesAsync();

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();
            await event1.Test(input, userProfile);

            var totalOrders = await context.Orders.CountAsync();
            Assert.Equal(1, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(1, totalUsers);

            var totalHoldTransactions = await context.HoldTransactions.CountAsync();
            Assert.Equal(1, totalHoldTransactions);

            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(6, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }


        [Fact]
        public async Task Sell()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "sell",
                current_pair = "btc_usd"
            };




            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var userProfile = new UserProfile();
            userProfile.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(userProfile);
            context.UserProfiles.Add(userProfile);
            await context.SaveChangesAsync();

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();
            await event1.Test(input, userProfile);

            var totalOrders = await context.Orders.CountAsync();
            Assert.Equal(1, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(1, totalUsers);

            var totalHoldTransactions = await context.HoldTransactions.CountAsync();
            Assert.Equal(1, totalHoldTransactions);

            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(6, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);


            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);
        }


        [Fact]
        public async Task BuyThenSellSameAmount()
        {



            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var buyer = new UserProfile()
            {
                username = "buyer"
            };
            buyer.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(buyer);

            var seller = new UserProfile()
            {
                username = "seller"
            };
            seller.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(seller);

            context.UserProfiles.Add(buyer);
            context.UserProfiles.Add(seller);
            await context.SaveChangesAsync();

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();
            var fakeSpotMarketService = serviceProvider.GetService<FakeSpotMarketService>();
            await fakeSpotMarketService.EnsureDataPopulated();

            InputTransactionRaw inputBuy = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };
            await event1.Test(inputBuy, buyer);

            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(9500, spotMarket.LastRate);
            }

            InputTransactionRaw inputSell = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "sell",
                current_pair = "btc_usd"
            };
            await event1.Test(inputSell, seller);

            var orders = await context.Orders.ToListAsync();
            var totalOrders = orders.Count();
            Assert.Equal(2, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(2, totalUsers);

            var totalHoldTransactions = await context.HoldTransactions.CountAsync();
            Assert.Equal(2, totalHoldTransactions);

            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal((6 * 2) + 4, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(1, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(1000, spotMarket.LastRate);
            }

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(3, totalOrderHistories);

        }


        [Fact]
        public async Task BuyThenSellDiffAmount()
        {



            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var buyer = new UserProfile()
            {
                username = "buyer"
            };
            buyer.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(buyer);


            var seller = new UserProfile()
            {
                username = "seller"
            };
            seller.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(seller);

            context.UserProfiles.Add(buyer);
            context.UserProfiles.Add(seller);
            await context.SaveChangesAsync();

            
            var fakeSpotMarketService = serviceProvider.GetService<FakeSpotMarketService>();
            await fakeSpotMarketService.EnsureDataPopulated();

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "1000",
                    amount = "0.5",
                    mode = "buy",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, buyer);
            }

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "2000",
                    amount = "0.5",
                    mode = "buy",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, buyer);
            }

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "3000",
                    amount = "0.5",
                    mode = "buy",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, buyer);
            }

            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(9500, spotMarket.LastRate);
            }

            {
                var openOrders = await context.Orders
                     .Where(x => x.IsOpenOrder)
                     .ToListAsync();
                Assert.Equal(3, openOrders.Count());
            }

            InputTransactionRaw inputSell = new InputTransactionRaw()
            {
                rate = "500",
                amount = "2",
                mode = "sell",
                current_pair = "btc_usd"
            };
            {
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputSell, seller);
            }

            var totalOrders = await context.Orders.CountAsync();
            Assert.Equal(4, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(2, totalUsers);

            var holdTransactions = await context.HoldTransactions.ToListAsync();
            var totalHoldTransactions = holdTransactions.Count();
            Assert.Equal(7, totalHoldTransactions);

            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(12 + 6 + 6, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(3, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(1000, spotMarket.LastRate);
            }


            var orders = await context.Orders.ToListAsync();
            var m = LogHelperBusiness.Log(orders);


            var lastOrder = await context.Orders.LastAsync();
            Assert.Equal(0.5m, lastOrder.LeftAmount);

            Assert.Equal(0.5m, seller.holdBalanceBtcTesting);


            {
                var openOrders = await context.Orders
                    .Where(x => x.IsOpenOrder)
                    .ToListAsync();

                Assert.Single(openOrders);
            }


            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(10, totalOrderHistories);
        }



        [Fact]
        public async Task SellhenBuyDiffAmount()
        {



            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var seller1 = new UserProfile()
            {
                username = "seller"
            };
            seller1.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(seller1);

            var buyer1 = new UserProfile()
            {
                username = "buyer"
            };
            buyer1.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(buyer1);

            context.UserProfiles.Add(seller1);
            context.UserProfiles.Add(buyer1);
            await context.SaveChangesAsync();

            
            var fakeSpotMarketService = serviceProvider.GetService<FakeSpotMarketService>();
            await fakeSpotMarketService.EnsureDataPopulated();

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "1000",
                    amount = "0.5",
                    mode = "sell",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, seller1);
            }

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "2000",
                    amount = "0.5",
                    mode = "sell",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, seller1);
            }

            {
                InputTransactionRaw inputBuy = new InputTransactionRaw()
                {
                    rate = "3000",
                    amount = "0.5",
                    mode = "sell",
                    current_pair = "btc_usd"
                };
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputBuy, seller1);
            }

            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(9500, spotMarket.LastRate);
            }

            {
                var openOrders = await context.Orders
                     .Where(x => x.IsOpenOrder)
                     .ToListAsync();
                Assert.Equal(3, openOrders.Count());
            }

            InputTransactionRaw inputSell = new InputTransactionRaw()
            {
                rate = "4000",
                amount = "2",
                mode = "buy",
                current_pair = "btc_usd"
            };
            {
                var event1 = serviceProvider.GetService<OrderItemMainTestService>();
                await event1.Test(inputSell, buyer1);
            }

            var totalOrders = await context.Orders.CountAsync();
            Assert.Equal(4, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(2, totalUsers);

            var holdTransactions = await context.HoldTransactions.ToListAsync();
            var totalHoldTransactions = holdTransactions.Count();
            Assert.Equal(7, totalHoldTransactions);

            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(12 + 6 + 6, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(3, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(3000, spotMarket.LastRate);
            }


            var orders = await context.Orders.ToListAsync();
            var m = LogHelperBusiness.Log(orders);


            var lastOrder = await context.Orders.LastAsync();
            Assert.Equal(0.5m, lastOrder.LeftAmount);

            var buyerDetail = buyer1.GetUserProfileDetail("usd");
            Assert.Equal(2000, buyerDetail.HoldBalance);


            {
                var openOrders = await context.Orders
                    .Where(x => x.IsOpenOrder)
                    .ToListAsync();

                Assert.Single(openOrders);
            }

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(10, totalOrderHistories);

        }

    }
}