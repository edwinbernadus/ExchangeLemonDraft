using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public static class UserProfileRemittanceExtension
    {
        public static AccountTransaction OutgoingTransfer(this UserProfile userProfile, decimal amount2, string currencyCode)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);
            detail.HoldVersion++;
            var newExtBalance = detail.OutgoingRemittance + amount2;
            var external = new RemittanceTransaction()
            {
                Amount = amount2,
                RunningBalance = newExtBalance,
                CurrencyCode = currencyCode,
                Version = detail.HoldVersion,
                IsIncoming = false
            };

            detail.RemittanceTransactions = detail.RemittanceTransactions ??
            new List<RemittanceTransaction>();
            detail.RemittanceTransactions.Add(external);
            detail.OutgoingRemittance = newExtBalance;



            var newBalance = detail.Balance - amount2;
            var accountTransaction = new AccountTransaction()
            {
                Amount = amount2,
                RunningBalance = newBalance,
                CurrencyCode = currencyCode,
                DebitCreditType = "D",
                IsExternal = true,
                Version = detail.Version,
                UserProfile = detail.UserProfile
            };

            detail.AccountTransactions = detail.AccountTransactions ??
            new List<AccountTransaction>();
            detail.AccountTransactions.Add(accountTransaction);

            detail.Balance = newBalance;
            return accountTransaction;
        }
        public static AccountTransaction IncomingTransfer(this UserProfile userProfile, decimal amount2, string currencyCode)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);
            detail.HoldVersion++;
            var newExtBalance = detail.IncomingRemittance + amount2;
            var external = new RemittanceTransaction()
            {
                Amount = amount2,
                RunningBalance = newExtBalance,
                CurrencyCode = currencyCode,
                Version = detail.HoldVersion,
                IsIncoming = true
            };

            detail.RemittanceTransactions = detail.RemittanceTransactions ??
            new List<RemittanceTransaction>();
            detail.RemittanceTransactions.Add(external);
            detail.IncomingRemittance = newExtBalance;



            var newBalance = detail.Balance + amount2;
            var accountTransaction = new AccountTransaction()
            {
                Amount = amount2,
                RunningBalance = detail.Balance + amount2,
                CurrencyCode = currencyCode,
                IsExternal = true,
                Version = detail.Version,
                UserProfile = detail.UserProfile,
                DebitCreditType = "C",
            };

            detail.AccountTransactions = detail.AccountTransactions ??
            new List<AccountTransaction>();
            detail.AccountTransactions.Add(accountTransaction);

            detail.Balance = newBalance;
            return accountTransaction;
        }

        [Obsolete]
        public static HoldTransaction RemoveHold(this UserProfile userProfile, decimal amount, Order order, string currencyCode, bool addToParent = true)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);
            detail.HoldVersion++;
            var hold = new HoldTransaction()
            {
                TransactionDate = DateTime.Now,
                Order = order,
                CurrencyCode = currencyCode,
                Amount = -1 * amount,
                Version = detail.HoldVersion

            };
            var prevHoldBalance = detail.HoldBalance;
            detail.HoldBalance -= amount;
            hold.RunningHoldBalance = detail.HoldBalance;


            if (hold.RunningHoldBalance < 0)
            {
                //throw new ArgumentException("minus hold balance when remove");
            }

            if (addToParent)
            {
                detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
                detail.HoldTransactions.Add(hold);
            }
            return hold;

        }
    }

}