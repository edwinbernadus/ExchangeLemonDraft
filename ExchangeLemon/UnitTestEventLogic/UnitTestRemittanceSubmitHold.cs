using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BackEndClassLibrary;
using System;

namespace BlueLight.Main.Tests
{

    public class UnitTestRemittanceSubmitHold
    {
        //public IUnitTestServiceProvider caller { get; set; }

        public ServiceProvider service { get; set; } = ServiceHelper.Generate();

        [Fact]
        public async Task TestSubmitHold()
        {
            //var state = Generate();
            var context = service.GetService<ApplicationContext>();
            //var serviceProvider = state.serviceProvider;

            await FactoryUnitTestRemittance.CreateSample(context);

            var transferBtcService = service.GetService<RemittanceOutgoingAdminService>();

            var user = await context.UserProfiles.FirstAsync(x => x.id == 1);
            var detail = user.GetUserProfileDetail("btc");

            await transferBtcService.SubmitHold(detail, new MvDepositInsert()
            {
                Address = "address1",
                Amount = 1.1m,
                Currency = "btc"
            });
            //TODO: 105 - validation negative
            //TODO: 105 - hold balance external


            Assert.Equal(2m, detail.OutgoingRemittance);
            Assert.Equal(2m, detail.Balance);
            Assert.Equal(1.1m, detail.HoldBalance);
            Assert.Equal(0, detail.AccountTransactions.Count);
            Assert.Equal(1, detail.HoldTransactions.Count);
            Assert.Equal(0.9m, UserProfileLogic.GetAvailableBalance(detail));

            var totalPendingBulk = await context.PendingBulkTransfers.CountAsync();
            Assert.Equal(0, totalPendingBulk);

            var totalPendingTransferLists = await context.PendingTransferLists
            .Where(x => x.PendingBulkTransfer == null)
            .CountAsync();
            Assert.Equal(1, totalPendingTransferLists);

            //return state;
        }

      

       
    }
}




//UnitTestState Generate()
//{
//    var service = DependencyHelper.GenerateServiceCollectionForTesting();
//    caller?.AdditionalService(service);

//    var serviceCollection = service.BuildServiceProvider();

//    //var serviceCollection = DependencyHelper.GenerateServiceProviderForTesting();
//    var context = serviceCollection.GetService<ApplicationContext>();
//    var output = new UnitTestState()
//    {
//        context = context,
//        serviceProvider = serviceCollection
//    };
//    return output;
//}