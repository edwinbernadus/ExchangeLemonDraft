using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlueLight.Main.Tests
{
    public class UnitTestRemittancePlainObject
    {
        [Fact]
        public void TestIncoming()
        {
            UserProfile user = CreateSample();
            var s = user.IncomingTransfer(1.1m, "btc");

            var detail = user.GetUserProfileDetail("btc");
            var totalRemittance = detail.RemittanceTransactions
                .Where(x => x.IsIncoming)
                .Count();
            var totalTransaction = detail.AccountTransactions.Count();

            
            Assert.Equal(1, totalRemittance);
            Assert.Equal(1, totalTransaction);

            Assert.Equal(1.1m, detail.IncomingRemittance);
            Assert.Equal(0, detail.OutgoingRemittance);
            Assert.Equal(1.1m, UserProfileLogic.GetAvailableBalance(detail));
        }

       

        [Fact]
        public void TestOutgoing()
        {
            UserProfile user = CreateSample();
            var s = user.OutgoingTransfer(1.1m, "btc");

            var detail = user.GetUserProfileDetail("btc");
            var totalRemittance = detail.RemittanceTransactions
                .Where(x => x.IsIncoming == false)
                .Count();
            var totalTransaction = detail.AccountTransactions.Count();
            Assert.Equal(1, totalRemittance);
            Assert.Equal(1, totalTransaction);

            Assert.Equal(0, detail.IncomingRemittance);
            Assert.Equal(1.1m, detail.OutgoingRemittance);
            Assert.Equal(-1.1m, UserProfileLogic.GetAvailableBalance(detail));

        }


        private UserProfile CreateSample()
        {
            var user = new UserProfile();
            user.UserProfileDetails = new List<UserProfileDetail>();

            var detail = new UserProfileDetail()
            {
                CurrencyCode = "btc",
                AccountTransactions = new List<AccountTransaction>(),
                HoldTransactions = new List<HoldTransaction>(),
                RemittanceTransactions = new List<RemittanceTransaction>()
            };
            user.UserProfileDetails.Add(detail);
            return user;
        }
    }
}