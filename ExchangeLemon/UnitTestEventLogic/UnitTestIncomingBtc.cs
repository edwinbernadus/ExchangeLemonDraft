using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using BackEndClassLibrary;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main.Tests
{
    public class UnitTestIncomingBtc
    {
        ServiceProvider serviceProvider;

        void EnsureServiceProvider()
        {
            if (this.serviceProvider == null)
            {
                var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
                serviceCollection.AddTransient<IBitcoinGetBalance, DummyTwoTransactionBitcoinGetBalance>();
                serviceProvider = serviceCollection.BuildServiceProvider();
                
            }
        }
        

        [Fact]
        public async Task TestIncomingTwoTransfer()
        {
            //var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            //serviceCollection.AddTransient<IBitcoinGetBalance, DummyTwoTransactionBitcoinGetBalance>();
            //ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            EnsureServiceProvider();
            
            var event1  = serviceProvider.GetService<BitcoinSyncJob>();
            var context = serviceProvider.GetService<ApplicationContext>();
            var userName = "one";
            
            await FactoryUnitTestRemittance.GenerateUser(context);

            await event1.ExecuteAsync(userName);
            
            Assert.Equal(0.00000003m, event1.ResultDiffAmount);

            var user = await context.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync( x => x.username == userName);
            var detail1 = user.UserProfileDetails.First(x => x.CurrencyCode == "btc");


            var detail = await context.UserProfileDetails
                .Include(x => x.RemittanceIncomingTransactions)
                .FirstAsync(x => x.Id == detail1.Id);
            var total = detail.RemittanceIncomingTransactions.Count();
            Assert.Equal(2, total);

            Assert.Equal(0.00000003m, detail.IncomingRemittance);

  

        }

        [Fact]
        public async Task TestIncomingTwoTransferDoubleCheck()
        {
            
            await TestIncomingTwoTransfer();
            EnsureServiceProvider();
            
            var bitcoinSyncJob = serviceProvider.GetService<BitcoinSyncJob>();
            var context = serviceProvider.GetService<ApplicationContext>();
            var userName = "one";
            await bitcoinSyncJob.ExecuteAsync(userName);
            Assert.Equal(0.00000000m, bitcoinSyncJob.ResultDiffAmount);

            var user = await context.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync(x => x.username == "one");
            var detail1 = user.UserProfileDetails.First(x => x.CurrencyCode == "btc");


            var detail = await context.UserProfileDetails
                .Include(x => x.RemittanceIncomingTransactions)
                .FirstAsync(x => x.Id == detail1.Id);
            var total = detail.RemittanceIncomingTransactions.Count();
            Assert.Equal(2, total);

            Assert.Equal(0.00000003m, detail.IncomingRemittance);



        }

        [Fact]
        public async Task TestIncomingThreeTransfer()
        {
            var serviceCollection = DependencyHelper.GenerateServiceCollectionForTesting();
            serviceCollection.AddTransient<DummyTwoTransactionBitcoinGetBalance>();
            serviceCollection.AddTransient<IBitcoinGetBalance, DummyThreeTransactionBitcoinGetBalance>();
            this.serviceProvider = serviceCollection.BuildServiceProvider();

            var bitcoinSyncJob = serviceProvider.GetService<BitcoinSyncJob>();
            var context = serviceProvider.GetService<ApplicationContext>();

            await TestIncomingTwoTransfer();

            DummyThreeTransactionBitcoinGetBalance.Step = 2;

            var userName = "one";
            await bitcoinSyncJob.ExecuteAsync(userName);
            Assert.Equal(0.00000003m, bitcoinSyncJob.ResultDiffAmount);
            
            var user = await context.UserProfiles
                .Include(x => x.UserProfileDetails)
                .FirstAsync(x => x.username == "one");
            var detail1 = user.UserProfileDetails.First(x => x.CurrencyCode == "btc");


            var detail = await context.UserProfileDetails
                .Include(x => x.RemittanceIncomingTransactions)
                .FirstAsync(x => x.Id == detail1.Id);
            var total = detail.RemittanceIncomingTransactions.Count();
            Assert.Equal(3, total);
            

            Assert.Equal(0.00000006m, detail.IncomingRemittance);
            



        }


 

       
    }
}