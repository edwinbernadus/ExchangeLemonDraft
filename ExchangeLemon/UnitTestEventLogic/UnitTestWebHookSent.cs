using BackEndClassLibrary;
using ExchangeLemonCore.Controllers;
using ExchangeLemonCore.Models.ReceiveModels;
using ExchangeLemonCore.Models.SendModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestWebHookSent 
    {
        //public void AdditionalService(ServiceCollection service)
        //{
        //    service.AddTransient<IBtcServiceSendMoney, DummyBtcServiceSendMoneyValidationSendTransfer>();
            
        //}


        [Fact]
        public async Task TestSentFromFile()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();

            
            serviceCollection.AddSingleton<IBtcServiceSendMoney, DummySendMoneyWebHookSent>();
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            var context = service.GetService<ApplicationContext>();

            var unitTestRemittanceSendTransferTwoItems = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = service
            };
            

            await unitTestRemittanceSendTransferTwoItems.TestSendTransferTwoItems();
            
            

            var routingService = service.GetService<WebHookRoutingService>();
            

            //var inputGuid = System.Guid.NewGuid();

            {
                var totalBefore = await context.PendingTransferLists.Where(x => x.TransactionId == null).CountAsync();
                Assert.Equal(0, totalBefore);
            }

            {
                var totalBefore = await context.PendingTransferLists.Where(x => x.TransactionId != null).CountAsync();
                Assert.Equal(2, totalBefore);
            }


            string event1 = "tx-confirmation";
            string content = await LoadFromFileSend();
            //BtcWebHookSendResult inputItem = JsonConvert.DeserializeObject<BtcWebHookSendResult>(content);
            await routingService.Execute(content, event1);


            {
                var totalAfter = await context.PendingTransferLists.Where(x => x.TransactionId == null).CountAsync();
                Assert.Equal(0, totalAfter);
            }

            {
                var totalAfter = await context.PendingTransferLists.Where(x => x.TransactionId != null).CountAsync();
                Assert.Equal(2, totalAfter);
            }

            var pendingItem = await context.PendingTransferLists.FirstAsync();
            Assert.Equal(6, pendingItem.ConfirmTransfer);



        }

        private async Task<string> LoadFromFileSend()
        {
            var fileName = "sample_send.txt";
            var s = await File.ReadAllTextAsync(fileName);
            return s;
        }

        [Fact]
        public async Task TestSent()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            serviceCollection.AddSingleton<IBtcServiceSendMoney, DummySendMoneyWebHookSent>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetService<ApplicationContext>();


            var unitTestRemittanceSendTransferTwoItems = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = serviceProvider
            };
            await unitTestRemittanceSendTransferTwoItems.TestSendTransferTwoItems();
            
            var service = serviceProvider.GetService<WebhookInquirySentTransferService>();
            string event1 = "event1";
            var inputItem = GenerateSampleSentItem();


            var inputGuid = System.Guid.NewGuid();

            {
                var totalBefore = await context.PendingTransferLists.Where(x => x.TransactionId == null).CountAsync();
                Assert.Equal(0, totalBefore);
            }

            {
                var totalBefore = await context.PendingTransferLists.Where(x => x.TransactionId != null).CountAsync();
                Assert.Equal(2, totalBefore);
            }
            await service.InquiryLogic(inputItem, inputGuid, event1);

           
            {
                var totalAfter = await context.PendingTransferLists.Where(x => x.TransactionId == null).CountAsync();
                Assert.Equal(0, totalAfter);
            }

            {
                var totalAfter = await context.PendingTransferLists.Where(x => x.TransactionId != null).CountAsync();
                Assert.Equal(2, totalAfter);
            }

            var items = await context.PendingTransferLists.ToListAsync();

            var transactionId = DummySendMoneyWebHookSent.transactionId;
            var pendingItem = items.First(x => x.TransactionId == transactionId);
            Assert.Equal(3, pendingItem.ConfirmTransfer);

            

        }

        private BtcWebHookSendResult GenerateSampleSentItem()
        {
            //var p = "e79669f7-57d5-4854-a5b3-a982ac83624d";
            //var p = transactionId;
            var p = DummySendMoneyWebHookSent.transactionId;
            var inputItem = new BtcWebHookSendResult()
            {
                confirmations = 3,
                hash = p,
                outputs = new List<ExchangeLemonCore.Models.SendModels.Output>()
                {

                }

            };
            return inputItem;
        }
    }
}