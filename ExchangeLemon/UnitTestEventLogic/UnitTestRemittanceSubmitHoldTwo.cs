using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEndClassLibrary;

namespace BlueLight.Main.Tests
{
    public class UnitTestRemittanceSubmitHoldTwo
    {
        public ServiceProvider service { get; set; } = ServiceHelper.Generate();

        public async Task LogicSubmitHoldTwo()
        {
            var context = service.GetService<ApplicationContext>();
            var serviceProvider = this.service;
            var transferBtcService = serviceProvider.GetService<RemittanceOutgoingAdminService>();

            var user = await context.UserProfiles.FirstAsync(x => x.id == 1);
            var detail = user.GetUserProfileDetail("btc");

            await transferBtcService.SubmitHold(detail, new MvDepositInsert()
            {
                Address = "address2",
                Amount = 0.2m,
                Currency = "btc"
            });


            Assert.Equal(2m, detail.OutgoingRemittance);
            Assert.Equal(2m, detail.Balance);
            Assert.Equal(1.3m, detail.HoldBalance);
            Assert.Equal(0, detail.AccountTransactions.Count);
            Assert.Equal(2, detail.HoldTransactions.Count);
            Assert.Equal(0.7m, UserProfileLogic.GetAvailableBalance(detail));

            var totalPendingBulk = await context.PendingBulkTransfers.CountAsync();
            Assert.Equal(0, totalPendingBulk);

            var totalPendingTransferLists = await context.PendingTransferLists
            .Where(x => x.PendingBulkTransfer == null)
            .CountAsync();
            Assert.Equal(2, totalPendingTransferLists);
            
        }
    }
}