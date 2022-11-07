using BackEndClassLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace BlueLight.Main.Tests
{
    public class UnitTestOutgoingBtcZeroError 
    {
        //public void AdditionalService(ServiceCollection service)
        //{
        //    service.AddTransient<IBtcConfirmTransactionInquiry, DummyNetworkBtcConfirmTransactionInquiry>();
        //}

        [Fact]
        public async Task TestConfirmZeroError()
        {
            
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            serviceCollection.AddTransient<IBtcConfirmTransactionInquiry, DummyNetworkBtcConfirmTransactionInquiry>();
            var service = serviceCollection.BuildServiceProvider();
            var context = service.GetService<ApplicationContext>();

            var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
            {
                service = service
            };
            
            await unitTestRemittance.TestSendTransferTwoItems();
            //var BtcCloudService = s2.BtcCloudServiceRegisterNotification;
            var output = context != null;
            Assert.True(output);

            var total = await context.SentItems.CountAsync();
            Assert.Equal(2, total);

            {
                var totalSent = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "sent").CountAsync();
                Assert.Equal(2, totalSent);
                var totalDelivered = await context.SentItems.Where(x => x.PendingTransferList.StatusTransfer == "delivered").CountAsync();
                Assert.Equal(0, totalDelivered);
            }


            IRemittanceOutgoingValidatorService remittanceOutgoingValidatorService = service.GetService<IRemittanceOutgoingValidatorService>();
            var item = await context.PendingTransferLists.FirstAsync();
            long pendingTransferId = item.Id;


            

            await remittanceOutgoingValidatorService.Execute(pendingTransferId, true);

            Assert.Equal(0, item.ConfirmTransfer);
            Assert.Single(item.PendingTransferListHistories);

            var pendingTransferListHistory = item.PendingTransferListHistories.First();


            Assert.Equal("error-message", pendingTransferListHistory.Content);
            Assert.True(pendingTransferListHistory.IsError);
            Assert.True(pendingTransferListHistory.IsManual);

        }
    }
}