using System;
using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestValidationNegativeBalance
    {
        int defaultTransaction = 6;

        [Fact]
        public async Task Sell()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200000",
                amount = "100000",
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

            var accountTransactions = await context.AccountTransactions.ToListAsync();
            var totalAccountTransactions = accountTransactions.Count();
            Assert.Equal(defaultTransaction, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }

        [Fact]
        public async Task Buy()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200000",
                amount = "0.5",
                mode = "buy",
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

            var accountTransactions = await context.AccountTransactions.ToListAsync();
            var totalAccountTransactions = accountTransactions.Count();
            Assert.Equal(defaultTransaction, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }


        [Fact]
        public async Task SellTwo()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "190500",
                amount = "99999",
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

            var accountTransactions = await context.AccountTransactions.ToListAsync();
            var totalAccountTransactions = accountTransactions.Count();
            Assert.Equal(defaultTransaction, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }

        [Fact]
        public async Task BuyTwo()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "190500",
                amount = "0.5",
                mode = "buy",
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

            var accountTransactions = await context.AccountTransactions.ToListAsync();
            var totalAccountTransactions = accountTransactions.Count();
            Assert.Equal(defaultTransaction, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }


        [Fact]
        public async Task BuyNegative()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200500",
                amount = "0.5",
                mode = "buy",
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
            var m = "";
            try
            {
                await event1.Test(input, userProfile);
            }
            catch (Exception ex)
            {
                m = ex.Message;

            }

            Assert.Equal("Balance not enough", m);



        }


        [Fact]
        public async Task SellNegative()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200500",
                amount = "100001",
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
            var m = "";
            try
            {
                await event1.Test(input, userProfile);
            }
            catch (Exception ex)
            {
                m = ex.Message;

            }

            Assert.Equal("Balance not enough", m);



        }


        [Fact]
         public async Task BuyNegativeUserName()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200500",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };




            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var userProfile = new UserProfile();
            //userProfile.username = "woot";
            userProfile.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(userProfile);
            context.UserProfiles.Add(userProfile);
            await context.SaveChangesAsync();

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();

            var m = "";
            try
            {
                await event1.Test(input, userProfile);
            }
            catch (Exception ex)
            {
                m = ex.Message;
                
            }

            Assert.Equal("Balance not enough", m);

        }




        [Fact]
        public async Task BuyNegativeUserNameBypass()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "200500",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };




            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();
            var context = serviceProvider.GetService<ApplicationContext>();


            var userProfile = new UserProfile();
            userProfile.username = "bot_sync@server.com";
            userProfile.PopulateCurrency();
            FakeInsertMoneyHelper.Execute(userProfile);
            context.UserProfiles.Add(userProfile);
            await context.SaveChangesAsync();

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();

            var m = "";
            try
            {
                await event1.Test(input, userProfile);
            }
            catch (Exception ex)
            {
                m = ex.Message;

            }

            Assert.Equal("", m);

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(1, totalOrderHistories);

        }

        [Fact]
        public async Task BuyThenSellDiff()
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
                    rate = "10000",
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
                    rate = "20000",
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
                    rate = "30000",
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
                rate = "5000",
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

            Assert.Equal(12 + (defaultTransaction *2), totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(3, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(10000, spotMarket.LastRate);
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
                rate = "200000",
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
                rate = "200000",
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

            var targetTotalTransctions = 4 + (defaultTransaction * 2);
            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(targetTotalTransctions, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(1, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(200000, spotMarket.LastRate);
            }

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(3, totalOrderHistories);

        }


        [Fact]
        public async Task BuyThenSellSameAmountNegative()
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
                rate = "1",
                amount = "100001",
                mode = "buy",
                current_pair = "btc_usd"
            };
            

            var m1 = "";

            try
            {
                await event1.Test(inputBuy, buyer);
            }
            catch (Exception ex)
            {
                m1 = ex.Message;

            }

            Assert.Equal("Balance not enough", m1);


            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(9500, spotMarket.LastRate);
            }

            InputTransactionRaw inputSell = new InputTransactionRaw()
            {
                rate = "1",
                amount = "100001",
                mode = "sell",
                current_pair = "btc_usd"
            };

            //await event1.Test(inputSell, seller);

            var m2 = "";
            try
            {
                await event1.Test(inputSell, seller);
            }
            catch (Exception ex)
            {
                m2 = ex.Message;

            }

            Assert.Equal("Balance not enough", m2);

            var orders = await context.Orders.ToListAsync();
            var totalOrders = orders.Count();
            Assert.Equal(0, totalOrders);

            var totalUsers = await context.UserProfiles.CountAsync();
            Assert.Equal(2, totalUsers);

            var totalHoldTransactions = await context.HoldTransactions.CountAsync();
            Assert.Equal(0, totalHoldTransactions);

            var targetTotalTransctions = 0 + (defaultTransaction * 2);
            var totalAccountTransactions = await context.AccountTransactions.CountAsync();
            Assert.Equal(targetTotalTransctions, totalAccountTransactions);

            var totalTransactions = await context.Transactions.CountAsync();
            Assert.Equal(0, totalTransactions);
            {
                var spotMarket = await context.SpotMarkets.FirstAsync(x => x.CurrencyPair == "btc_usd");
                Assert.Equal(9500, spotMarket.LastRate);
            }

            var orderHistories = await context.OrderHistories.ToListAsync();
            var totalOrderHistories = orderHistories.Count();
            Assert.Equal(0, totalOrderHistories);

        }
    }
}