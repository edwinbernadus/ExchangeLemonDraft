using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEndClassLibrary;

namespace BlueLight.Main.Tests
{
    public class UnitTestRemittanceSendTransferTwoItems
    {

        public ServiceProvider service {get;set;} = ServiceHelper.Generate();

        [Fact]
        public async Task  TestSendTransferTwoItems()
        {

            var context = service.GetService<ApplicationContext>();

            var s = new UnitTestRemittanceSubmitHold();
            s.service = service;
            await s.TestSubmitHold();
            await Task.Delay(500);

            var s2 = new UnitTestRemittanceSubmitHoldTwo();
            s2.service = service;
            await s2.LogicSubmitHoldTwo();

            //var context = unitTestState.context;
            //var service = unitTestState.serviceProvider;

            {
                var totalBefore = await context.PendingBulkTransfers.CountAsync();
                Assert.Equal(0, totalBefore);

                var total = await context.PendingTransferLists
                 .Where(x => x.PendingBulkTransfer == null).CountAsync();
                Assert.Equal(2, total);
            }

            var transfer = service.GetService<RemittanceOutgoingAdminService>();


            {
                var pending = await context.PendingTransferLists.FirstAsync();
                var detail = pending.UserProfileDetail;
                Assert.Equal(2m, detail.OutgoingRemittance);
                Assert.Equal(2m, detail.Balance);
                Assert.Equal(1.3m, detail.HoldBalance);
                Assert.Equal(0, detail.AccountTransactions.Count);
                Assert.Equal(2, detail.HoldTransactions.Count);
                Assert.Equal(0.7m, UserProfileLogic.GetAvailableBalance(detail));
            }
            await transfer.SendAndReleaseHold();
            //var btcCloudServiceRegisterNotification = transfer.registerNotification;
            {
                var isPendingAvailable = await context.PendingTransferLists
                    .Where(x => x.PendingBulkTransfer == null).AnyAsync();
                Assert.False(isPendingAvailable);

                var totalAfter = await context.PendingBulkTransfers.CountAsync();
                Assert.Equal(1, totalAfter);

                var item = await context.PendingTransferLists
                    //.Include(x => x.PendingBulkTransfer)
                    .FirstAsync();
                var bulk = item.PendingBulkTransfer;
                Assert.Equal(1, bulk.Id);

                var pending = await context.PendingTransferLists.FirstAsync();
                var detail = pending.UserProfileDetail;

                Assert.Equal(3.3m, detail.OutgoingRemittance);
                Assert.Equal(0.7m, detail.Balance);
                Assert.Equal(0m, detail.HoldBalance);
                Assert.Equal(2, detail.AccountTransactions.Count);
                Assert.Equal(4, detail.HoldTransactions.Count);
                Assert.Equal(0.7m, UserProfileLogic.GetAvailableBalance(detail));

            }

            //unitTestState.BtcCloudServiceRegisterNotification = btcCloudServiceRegisterNotification;

            //return unitTestState;
        }
    }
}