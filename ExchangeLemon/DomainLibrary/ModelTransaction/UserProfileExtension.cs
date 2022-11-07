using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public static class UserProfileExtension
    {
        public static UserProfileDetail GetUserProfileDetail(this UserProfile userProfile, string code)
        {
            var item = userProfile.UserProfileDetails.FirstOrDefault(x => x.CurrencyCode == code);
            if (item != null)
            {
                return item;
            }
            else
            {
                return null;
            }
        }
        public static decimal GetAvailableBalanceFromCurrency(this UserProfile userProfile, string code)
        {
            var UserProfileDetails = userProfile.UserProfileDetails;
            var detail = UserProfileDetails.First(x => x.CurrencyCode == code);
            //var output = detail.Balance;
            var output = UserProfileLogic.GetAvailableBalance(detail);
            return output;
        }



    }

}

// [Obsolete]
// public static void AddExternalManuallyObsolete(this UserProfile userProfile, decimal amount2, string currencyCode)
// {
//     var UserProfileDetails = userProfile.UserProfileDetails;
//     var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);




//     var newBalance = detail.Balance + amount2;
//     var accountTransaction = new AccountTransaction()
//     {
//         Amount = amount2,
//         RunningBalance = detail.Balance + amount2,
//         CurrencyCode = currencyCode,
//         IsExternal = true
//     };

//     detail.AccountTransactions = detail.AccountTransactions ?? new List<AccountTransaction>();
//     detail.AccountTransactions.Add(accountTransaction);

//     detail.Balance = newBalance;
// }
