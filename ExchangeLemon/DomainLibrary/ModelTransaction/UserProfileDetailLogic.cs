using System;

namespace BlueLight.Main
{
    public class UserProfileDetailLogic
    {
        
        public AccountTransaction AddInternalBalance(UserProfileDetail detail,decimal amount, Transaction transaction)
        {
            var pair = transaction.CurrencyPair;
            //var detail = this;
            // update balance
            var runningBalance = detail.Balance + amount;
            detail.Balance = runningBalance;

            // create history

            var input = new AccountTransaction();

            detail.Version++;
            input.Version = detail.Version;

            input.Amount = amount;
            input.CurrencyPair = pair;
            input.UserProfileDetail = detail;
            input.UserProfile = detail.UserProfile;

            input.Transaction = transaction;
            //input.TransactionRate = transaction.TransactionRate;

            input.CurrencyCode = detail.CurrencyCode;
            input.Rate = transaction.TransactionRate;

            input.RunningBalance = runningBalance;
            return input;
        }

        //[Obsolete]
        public HoldTransaction AddHold(UserProfileDetail detail,decimal amount, Order order, string currencyCode)
        {

            //var detail = this;



            detail.HoldVersion++;

            var hold = new HoldTransaction()
            {
                TransactionDate = DateTime.Now,
                Order = order,
                // RunningHoldBalance = detail.HoldBalance,
                // OppositeHoldTransaction = null,
                CurrencyCode = currencyCode,
                Amount = amount,
                Version = detail.HoldVersion


            };

            var prevHoldBalance = detail.HoldBalance;
            detail.HoldBalance += amount;
            hold.RunningHoldBalance = detail.HoldBalance;
            //if (hold.RunningHoldBalance < -0.0001)
            if (hold.RunningHoldBalance < 0)
            {
                //throw new ArgumentException("minus hold balance when add");
            }

            //detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
            //detail.HoldTransactions.Add(hold);

            return hold;
        }

        //[Obsolete]
        public HoldTransaction RemoveHold(UserProfileDetail detail,decimal amount, Order order, string currencyCode)
        {
            //var detail = UserProfileDetails.First(x => x.CurrencyCode == currencyCode);
            //var detail = this;
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

            //if (addToParent)
            //{
            //    detail.HoldTransactions = detail.HoldTransactions ?? new List<HoldTransaction>();
            //    detail.HoldTransactions.Add(hold);
            //}
            return hold;

        }

    }

}
