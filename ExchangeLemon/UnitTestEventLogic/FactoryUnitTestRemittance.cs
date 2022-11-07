using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlueLight.Main.Tests
{
    public class FactoryUnitTestRemittance
    {
        public static async Task GenerateUser(ApplicationContext context)
        {
            var user = new UserProfile()
            {
                username = "one",
                UserProfileDetails = new List<UserProfileDetail>()
                {
                    new UserProfileDetail()
                    {
                        CurrencyCode = "btc",
                        PublicAddress = "abc",
                        PrivateKey = "key1"
                    }
                }

            };
            await context.UserProfiles.AddAsync(user);
            await context.SaveChangesAsync();

        }

        internal static async  Task CreateSample(ApplicationContext context)
        {
            var detail = new UserProfileDetail()
            {
                Balance = 2,
                OutgoingRemittance = 2,
                PublicAddress = "address1",
                PrivateKey = "key1",
                CurrencyCode = "btc",
                AccountTransactions = new List<AccountTransaction>(),
                HoldTransactions = new List<HoldTransaction>()
            };
            var user = new UserProfile()
            {
                UserProfileDetails = new List<UserProfileDetail>()
            };
            user.UserProfileDetails.Add(detail);
            await context.UserProfiles.AddAsync(user);
            await context.SaveChangesAsync();
        }


        internal static async Task CreateSampleTwo(ApplicationContext context)
        {
            var detail = new UserProfileDetail()
            {
                Balance = 0,
                OutgoingRemittance = 0,
                PublicAddress = "msv6zoMW59BbEWHeb6k9kMrd852WU1XpqZ",
                PrivateKey = "key1",
                CurrencyCode = "btc",
                AccountTransactions = new List<AccountTransaction>(),
                HoldTransactions = new List<HoldTransaction>()
            };
            var user = new UserProfile()
            {
                UserProfileDetails = new List<UserProfileDetail>()
            };
            user.UserProfileDetails.Add(detail);
            await context.UserProfiles.AddAsync(user);
            await context.SaveChangesAsync();
        }


        //internal async static Task PopulateDataOutgoingBtc(ServiceProvider serviceProvider)
        //{
        //    var unitTestRemittance = new UnitTestRemittanceSendTransferTwoItems()
        //    {
        //        service = serviceProvider
        //    };
        //    await unitTestRemittance.TestSendTransferTwoItems();
        //    var oldContext = serviceProvider.GetService<ApplicationContext>();
        //    var context = oldContext;
        //    var user = await oldContext.UserProfiles
        //        .Include(x => x.UserProfileDetails)
        //        .FirstAsync();
        //    await FactoryUnitTestRemittance.GenerateUserFromExisting(context, user);

        //}

        internal static async Task GenerateUserFromExisting(ApplicationContext context, UserProfile user)
        {
            user.id = 0;
            foreach (var detail in user.UserProfileDetails)
            {
                detail.Id = 0;
                foreach (var item2 in detail.RemittanceIncomingTransactions)
                {
                    item2.Id = 0;
                };
                foreach (var item2 in detail.AccountTransactions)
                {
                    item2.Id = 0;
                };

                foreach (var item2 in detail.RemittanceTransactions)
                {
                    item2.Id = 0;
                };
            }
            context.UserProfiles.Add(user);
            await context.SaveChangesAsync();
        }
      
    }
}