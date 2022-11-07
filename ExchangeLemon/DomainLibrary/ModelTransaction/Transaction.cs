using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class Transaction
    {
        public int Id { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal TransactionRate { get; set; }



        public Order GetBuyOrderFirst()
        {
            //var output = Orders.First(x => x.IsBuy);
            var output = OrderTransactions.Select(x => x.Order).First(x => x.IsBuy);
            return output;
        }

        public Order GetSellOrderFirst()
        {
            //var output = Orders.First(x => x.IsBuy == false);
            var output = OrderTransactions.Select(x => x.Order).First(x => x.IsBuy == false);
            return output;
        }



        [JsonIgnore]
        public ICollection<OrderTransaction> OrderTransactions { get; set; }

        internal void AddOrder(Order order)
        {
            var transaction = this;
            transaction.OrderTransactions = transaction.OrderTransactions ??
                new List<OrderTransaction>();
            transaction.OrderTransactions.Add(new OrderTransaction()
            {
                Order = order,
                Transaction = transaction,
                
            });

            order.Transactions.Add(transaction);


        }


        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public bool IsBuyTaker { get; set; }




        public string CurrencyPair { get; set; }

        public void UpdateAmount(Order buyOrder, Order sellOrder)
        {
            var transaction = this;
            var amount = transaction.Amount;


            buyOrder.UpdateAmount(amount);
            sellOrder.UpdateAmount(amount);


            transaction.AddOrder(buyOrder);
            transaction.AddOrder(sellOrder);

        }
    }
}