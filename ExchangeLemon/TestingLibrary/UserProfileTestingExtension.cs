using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public static class UserProfileTestingExtension
    {
        
        public static void AddBalanceTesting(this UserProfile userProfile, string code, decimal amount)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == code);
            var output = detail.Balance;

            detail.Balance += amount;


            var externalTransaction = new RemittanceTransaction()
            {
                Amount = amount,
                CreatedDate = DateTime.Now
            };
            detail.RemittanceTransactions.Add(externalTransaction);

            var accountTransaction = new AccountTransaction()
            {
                Amount = amount,
                CreatedDate = DateTime.Now,
            };
            detail.AccountTransactions.Add(accountTransaction);



        }

        public static void SetBalanceTesting(this UserProfile userProfile, string currencyCode, decimal balance)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);
            var prevBalance = detail.Balance;
            var prevHoldBalance = detail.HoldBalance;

            var adjustmentAmount = balance - prevBalance;


            detail.Balance = balance;
            detail.HoldBalance = 0;


            var history = new AdjustmentTransaction()
            {
                AdjustmentAmount = adjustmentAmount,

                CurrencyCode = currencyCode,
                RunningBalance = detail.Balance,
                PrevHoldBalance = prevHoldBalance
            };
            detail.AdjustmentTransactions.Add(history);
        }

        public static void PopulateCurrency(this UserProfile userProfile)
        {

            userProfile.UserProfileDetails = new List<UserProfileDetail>();
            var userProfileDetails = userProfile.UserProfileDetails;

            var currencies = UserProfile.GetDefaultCurrencies();
            foreach (var currency in currencies)
            {
                var detail = new UserProfileDetail()
                {
                    CurrencyCode = currency,
                    HoldTransactions = new List<HoldTransaction>(),
                    AccountTransactions = new List<AccountTransaction>(),
                    RemittanceTransactions = new List<RemittanceTransaction>(),
                    AdjustmentTransactions = new List<AdjustmentTransaction>()
                };

                userProfileDetails.Add(detail);


            }
        }


        public static decimal GetLedgerBalanceFromCurrency(this UserProfile userProfile, string code)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == code);
            var output = detail.Balance;
            return output;
        }

    }

}