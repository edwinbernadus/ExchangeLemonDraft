using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestEventCancel
    {
         [Fact]
        public async Task TestCancelBuy()
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

            var isCancel = order.IsCancelled;
            Assert.False(isCancel);

            //var items1 = userProfile.UserProfileDetails.ToList();

            var balance1 =  userProfile.GetAvailableBalanceFromCurrency("btc");
            var amount = order.Amount;
            var shouldBalance = balance1 + amount;
            var cancelService = serviceProvider.GetService<OrderItemCancelService>();
            await cancelService.DirectFromOrder(order.Id,"one");
            var isCancel2 = order.IsCancelled;

            Assert.True(isCancel2);
            //var items2 = userProfile.UserProfileDetails.ToList();
            var balance2=  userProfile.GetAvailableBalanceFromCurrency("btc");
            Assert.Equal(shouldBalance,balance2);

            var items = context.Orders.ToList();
        }


          [Fact]
        public async Task TestCancelAll()
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

            var isCancel = order.IsCancelled;
            Assert.False(isCancel);

            //var items1 = userProfile.UserProfileDetails.ToList();

            //var balance1 =  userProfile.GetAvailableBalanceFromCurrency("btc");
            //var amount = order.Amount;
            //var shouldBalance = balance1 + amount;
            var cancelService = serviceProvider.GetService<OrderItemCancelAllService>();
            var user = order.UserProfile;
            

                var userInput = new UserProfileLite()
            {
                UserId = user.id,
                UserName = user.username
            };

            await cancelService.DirectExecute(userInput);
            var isCancel2 = order.IsCancelled;

            Assert.True(isCancel2);
            //var items2 = userProfile.UserProfileDetails.ToList();
            var items3 = userProfile.UserProfileDetails.Where(x => x.HoldBalance > 0).ToList();
            Assert.Empty(items3);
            //var balance2=  userProfile.GetAvailableBalanceFromCurrency("btc");
            //Assert.Equal(shouldBalance,balance2);

            //var orderItems1 = context.Orders.ToList();
            var orderItems2 = context.Orders.Where(x => x.IsCancelled == false).ToList();
            Assert.Empty(orderItems2);
        }
    }
}