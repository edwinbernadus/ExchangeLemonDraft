using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.Main
{
    public class MvOpenOrder
    {

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public decimal LeftAmount { get; set; }

        public bool IsBuy { get; set; }


        public decimal RequestRate { get; set; }



        public bool IsComplete { get; set; }


        public string TransactionIds { get; set; }
        public decimal DetailRate { get; }
        public string CreatedBy { get; }
        public string CurrencyPair { get; }
        public Order Order { get; }

        public MvOpenOrder(Order input)
        {
            this.Id = input.Id;
            this.CreatedDate = input.CreatedDate;
            this.Amount = input.Amount;
            this.LeftAmount = input.LeftAmount;
            this.IsBuy = input.IsBuy;
            this.RequestRate = input.RequestRate;
            this.IsComplete = input.IsFillComplete;
            this.TransactionIds = GetTransactionIds(input.Transactions);
            this.DetailRate = -1;
            this.CreatedBy = input.UserProfile?.username ?? "NoName";
            this.CurrencyPair = input.CurrencyPair;
            this.Order = input;
        }

        private string GetTransactionIds(ICollection<Transaction> transactions)
        {
            //var ids = this.GetTransactions().Select(x => x.Id + "-" + x.TransactionRate);
            var ids = transactions.Select(x => x.Id + "-" + x.TransactionRate).ToList();
            var output = String.Join(",", ids);
            return output;
        }

      
    }
}
