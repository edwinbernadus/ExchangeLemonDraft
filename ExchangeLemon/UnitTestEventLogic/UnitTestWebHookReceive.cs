using BackEndClassLibrary;
using ExchangeLemonCore.Controllers;
using ExchangeLemonCore.Models.ReceiveModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestWebHookReceive
    {
        [Fact]
        public async Task TestReceive()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var service  = serviceProvider.GetService<WebhookReceiveTransferService>();

            //var service2 = serviceProvider.GetService<IBtcServiceSendMoney>();


            var context = serviceProvider.GetService<ApplicationContext>();
            await FactoryUnitTestRemittance.GenerateUser(context);


            string event1 = "event1";
            var inputItem = GenerateSample();
         

            var inputGuid = System.Guid.NewGuid();
            await service.ReceiveLogic(inputItem, inputGuid, event1);

            var detail = await context.UserProfileDetails.FirstAsync();
            var balance = detail.Balance;
            Assert.Equal(0.00000003m, balance);

            
        }

        

  
        private BtcWebHookReceiveResult GenerateSample()
        {
            BtcWebHookReceiveResult inputItem = new BtcWebHookReceiveResult()
            {

                outputs = new List<ExchangeLemonCore.Models.ReceiveModels.Output>()
                {
                    new ExchangeLemonCore.Models.ReceiveModels.Output()
                    {
                        addresses =  new List<string>{"abc","def" }
                    },

                    new ExchangeLemonCore.Models.ReceiveModels.Output()
                    {
                        addresses =  new List<string>{"hij","klm" }
                    }

                },


            };
            return inputItem;
        }

        private async Task<string> LoadFromFileReceive()
        {
            var fileName = "sample_receive.txt";
            var s = await File.ReadAllTextAsync(fileName);
            return s;
        }

        [Fact]
        public async Task TestReceiveFromFile()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();

            
            serviceCollection.AddSingleton<IBtcServiceSendMoney, DummySendMoneyWebHookSent>();
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            var context = service.GetService<ApplicationContext>();
            await FactoryUnitTestRemittance.CreateSampleTwo(context);

            var routingService = service.GetService<WebHookRoutingService>();

            {
                var detail = await context.UserProfileDetails.FirstAsync();
                var balance = detail.Balance;
                Assert.Equal(0, balance);
            }
          


            string event1 = "unconfirmed-tx";
            string content = await LoadFromFileReceive();

            {
                await routingService.Execute(content, event1);
            }

            {
                var detail = await context.UserProfileDetails.FirstAsync();
                var balance = detail.Balance;
                Assert.Equal(0.00000003m,balance);
            }


            {
                await routingService.Execute(content, event1);
            }

            {
                var detail = await context.UserProfileDetails.FirstAsync();
                var balance = detail.Balance;
                Assert.Equal(0.00000003m, balance);
            }
        }
    }
}