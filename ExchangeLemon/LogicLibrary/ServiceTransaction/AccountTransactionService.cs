using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public class AccountTransactionService
    {
        // public AccountTransactionService accountTransactionService { get; }
        // public OrderPopulateAccountService(AccountTransactionService accountTransactionService)
        // {

        //     this.accountTransactionService = accountTransactionService;
        // }



        public List<AccountTransactionOutput> Execute(Order order,
        Transaction transaction,
        UserProfile buyer, UserProfile seller)
        {
            var output = new List<AccountTransactionOutput>();
            var currencyPair = order.CurrencyPair;
            {

                var input1 = new AccountTransactionInput()
                {
                    currencyPair = currencyPair,
                    transaction = transaction,
                    player = buyer,
                };
                input1.SetBtcModeDebit();
                var output1 = this.Logic(input1);
                output.Add(output1);


                var input2 = new AccountTransactionInput()
                {
                    currencyPair = currencyPair,
                    transaction = transaction,
                    player = buyer,
                };
                input2.SetAltModeCredit();
                var output2 = this.Logic(input2);
                output.Add(output2);
            }

            {


                var input1 = new AccountTransactionInput()
                {
                    currencyPair = currencyPair,
                    transaction = transaction,
                    player = seller,
                };
                input1.SetAltModeDebit();
                var output1 = this.Logic(input1);
                output.Add(output1);

                var input2 = new AccountTransactionInput()
                {
                    currencyPair = currencyPair,
                    transaction = transaction,
                    player = seller,
                };
                input2.SetBtcModeCredit();
                var output2 = this.Logic(input2);
                output.Add(output2);
            }

            return output;
        }




        public AccountTransactionOutput Logic(AccountTransactionInput t)
        {

            var rate = t.transaction.TransactionRate;
            var detail = t.player.UserProfileDetails.First(x => x.CurrencyCode == t.currencyCode);

            var amount = GetAmount(t.transaction, t.isMultiplyRate, t.isNegativeAmount, rate);

            var logic = new UserProfileDetailLogic();
            var accountTransaction = logic.AddInternalBalance(detail,amount, transaction: t.transaction);

        
            var output = new AccountTransactionOutput()
            {
                userProfileDetail = detail,
                accountTransaction = accountTransaction
            };
            return output;
            // return amount;
        }



        private decimal GetAmount(Transaction transaction,
        bool isMultiplyRate, bool isNegativeAmount, decimal rate)
        {
            var amount = transaction.Amount;

            if (isMultiplyRate)
            {
                amount = transaction.Amount * rate;
            }

            if (isNegativeAmount)
            {
                amount = amount * -1;
            }
            return amount;
        }

        //internal void UpdateAmount(Transaction transaction, Order buyOrder, Order sellOrder)
        //{
          
        //        var amount = transaction.Amount;
        //        buyOrder.UpdateAmount(amount);
        //        sellOrder.UpdateAmount(amount);
            

        //        transaction.AddOrder(buyOrder);
        //        transaction.AddOrder(sellOrder);
         
        //}

        internal void InsertIntoCollection(List<AccountTransactionOutput> z1)
        {
            foreach (var item in z1)
            {
                var detail = item.userProfileDetail;
                var accountTransaction = item.accountTransaction;
                detail.AccountTransactions = detail.AccountTransactions ??
                new List<AccountTransaction>();
                detail.AccountTransactions.Add(accountTransaction);

            }
        }
    }
}