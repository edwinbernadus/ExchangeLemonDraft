// using System.Collections.Generic;
// using System.Linq;

// namespace BlueLight.Main
// {
//     public class AccountTransactionService

//     {




//         public decimal Execute(AccountTransactionInput t)
//         {

//             var rate = t.transaction.TransactionRate;
//             var detail = t.player.UserProfileDetails.First(x => x.CurrencyCode == t.currencyCode);

//             var amount = GetAmount(t.transaction, t.isMultiplyRate, t.isNegativeAmount, rate);

//             // update balance
//             var runningBalance = detail.Balance + amount;
//             detail.Balance = runningBalance;

//             // create history

//             var accountTransaction = t.accountTransaction;

//             detail.Version++;
//             accountTransaction.Version = detail.Version;

//             accountTransaction.Amount = amount;
//             accountTransaction.CurrencyPair = t.currencyPair;
//             accountTransaction.UserProfileDetail = detail;

//             accountTransaction.Transaction = t.transaction;

//             accountTransaction.CurrencyCode = t.currencyCode;
//             accountTransaction.Rate = rate;

//             accountTransaction.RunningBalance = runningBalance;

//             detail.AccountTransactions = detail.AccountTransactions ?? new List<AccountTransaction>();
//             detail.AccountTransactions.Add(accountTransaction);

//             return amount;
//         }



//         private decimal GetAmount(Transaction transaction, bool isMultiplyRate,
//         bool isNegativeAmount, decimal rate)
//         {
//             var amount = transaction.Amount;

//             if (isMultiplyRate)
//             {
//                 amount = transaction.Amount * rate;
//             }

//             if (isNegativeAmount)
//             {
//                 amount = amount * -1;
//             }
//             return amount;
//         }

//     }

// }