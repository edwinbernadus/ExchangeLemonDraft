using System;
using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using BlueLight.Main;
using ExchangeLemonCore.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestEventBuy
    {

        [Fact]
        public async Task TestBuyTwoAndRunningOrder()
        {

            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "10000",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_idr"
            };

            //var userProfile2 = new UserProfile();
            //userProfile2.PopulateCurrency();
            //FakeInsertMoneyHelper.Execute(userProfile2);


            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();

            var context = serviceProvider.GetService<ApplicationContext>();
            var FakeSpotMarketService = serviceProvider.GetService<FakeSpotMarketService>();
            await FakeSpotMarketService.EnsureDataPopulated();
            var user1 = new UserProfile();
            var user2 = new UserProfile();
            context.UserProfiles.Add(user1);
            context.UserProfiles.Add(user2);
            await context.SaveChangesAsync();
            var user3 = new UserProfile();
            context.UserProfiles.Add(user3);
            await context.SaveChangesAsync();
            var users = context.UserProfiles.ToList();
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = true;
            var market = factory.Generate();

            foreach (var item in market.Orders)
            {
                //item.Id = 0;
                context.Orders.Add(item);
            }
            await context.SaveChangesAsync();

            LogHelperBusiness.Log(market.Orders);
            LogHelperBusiness.Log(market.Orders.First());
            LogHelperBusiness.Log(input);

            var order = context.Orders.First();
            var user_loaded = order.UserProfile;
            var userProfile = user_loaded;
            FakeInsertMoneyHelper.Execute(userProfile);

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();

            await event1.Test(input, userProfile);
            var total = event1.OrderResult.TargetNotificationNames.Count;
            Assert.Equal(2, total);

            var orderResult = event1.OrderResult;
            var orderHistories = orderResult.OrderHistories;
            var orderHistory = orderHistories.First();
            Assert.Equal(2.5m, orderHistory.RunningAmount);
            Assert.Equal(2.5m, orderHistory.RunningLeftAmount);

        }

        [Fact]
        public async Task TestBuyOne()
        {
            ParamSpecial.IsForceStop = false;
            InputTransactionRaw input = new InputTransactionRaw()
            {
                rate = "1000",
                amount = "0.5",
                mode = "buy",
                current_pair = "btc_usd"
            };

            
            var serviceProvider = DependencyHelper.GenerateServiceProviderForTesting();


            var context = serviceProvider.GetService<ApplicationContext>();

            var user1 = new UserProfile();
            user1.username = "user1";
            var user2 = new UserProfile();
            user2.username = "user2";

            context.UserProfiles.Add(user1);
            context.UserProfiles.Add(user2);
            await context.SaveChangesAsync();

            var user3 = new UserProfile();
            user3.username = "user3";

            context.UserProfiles.Add(user3);
            await context.SaveChangesAsync();
            var users = context.UserProfiles.ToList();
            var factory = new FactoryTest();
            factory.IsResetIdDevMode = true;
            var market = factory.Generate();

            foreach (var item in market.Orders)
            {
                //item.Id = 0;
                context.Orders.Add(item);
            }
            await context.SaveChangesAsync();

            LogHelperBusiness.Log(market.Orders);
            LogHelperBusiness.Log(market.Orders.First());
            LogHelperBusiness.Log(input);

            var order = context.Orders.First();
            var user_loaded = order.UserProfile;
            var userProfile = user_loaded;

            FakeInsertMoneyHelper.Execute(userProfile);

            var event1 = serviceProvider.GetService<OrderItemMainTestService>();

            await event1.Test(input, userProfile);
            Assert.Null(event1.OrderResult.TargetNotificationNames);
        }

      
    }
}