using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEndClassLibrary;

namespace BlueLight.Main.Tests
{
    public class UnitTestRemittance
    {
        
        

        [Fact]
        public async Task TestSendTransferReleaseHold()
        {
            var service = ServiceHelper.Generate();
            var context = service.GetService<ApplicationContext>();

            var s = new UnitTestRemittanceSubmitHold();
            s.service = service;
            await s.TestSubmitHold();
            
            

            {
                var totalBefore = await context.PendingBulkTransfers.CountAsync();
                Assert.Equal(0, totalBefore);

                var isPendingAvailable = await context.PendingTransferLists
                 .Where(x => x.PendingBulkTransfer == null).AnyAsync();
                Assert.True(isPendingAvailable);
            }

            var transfer = service.GetService<RemittanceOutgoingAdminService>();
            await transfer.SendAndReleaseHold();

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

                Assert.Equal(3.1m, detail.OutgoingRemittance);
                Assert.Equal(0.9m, detail.Balance);
                Assert.Equal(0m, detail.HoldBalance);
                Assert.Equal(1, detail.AccountTransactions.Count);
                Assert.Equal(2, detail.HoldTransactions.Count);
                Assert.Equal(0.9m, UserProfileLogic.GetAvailableBalance(detail));

            }


            
        }


     

        [Fact]
        public async Task TestSendTransferAndCancelOne()
        {
            var service = ServiceHelper.Generate();
            var context = service.GetService<ApplicationContext>();
            var s = new UnitTestRemittanceSubmitHold()
            {
                service = service
            };
            await s.TestSubmitHold();


            var s2 = new UnitTestRemittanceSubmitHoldTwo()
            {
                service = service
            };
            await s2.LogicSubmitHoldTwo();

            //await LogicSubmitHoldTwo(unitTestState);

            
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

            await transfer.Reject(2);

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

                Assert.Equal(3.1m, detail.OutgoingRemittance);
                Assert.Equal(0.9m, detail.Balance);
                Assert.Equal(0m, detail.HoldBalance);
                Assert.Equal(1, detail.AccountTransactions.Count);
                Assert.Equal(4, detail.HoldTransactions.Count);
                Assert.Equal(0.9m, UserProfileLogic.GetAvailableBalance(detail));

            }

        }
    }
}