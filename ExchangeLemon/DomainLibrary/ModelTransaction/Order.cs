using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace BlueLight.Main
{

    public partial class Order
    {


        public DateTime? CancelDate { get; set; }
        public int Id { get; set; }

        private bool _isFillComplete;



        [Column(TypeName = "decimal(38, 8)")]
        public decimal RequestRate { get; set; }


        [Column(TypeName = "decimal(38, 8)")]
        private decimal _leftAmount;

        [Column(TypeName = "decimal(38, 8)")]
        private decimal _totalTransactions;



        public string FirstPair
        {
            get
            {
                if (this.CurrencyPair == null)
                {
                    return "yyy";
                }
                var output = PairHelper.GetFirstPair(this.CurrencyPair);
                return output;
            }
        }

        public string SecondPair
        {
            get
            {
                if (this.CurrencyPair == null)
                {
                    return "yyy";
                }
                var output = PairHelper.GetSecondPair(this.CurrencyPair);
                return output;
            }
        }

        public Guid GuidId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        [JsonIgnore]
        public ICollection<OrderTransaction> OrderTransactions { get; set; }

        //todo001 :15 tuning order from calc to field
        #region calc

        public bool IsOpenOrder { get; set; }

        public bool IsFillComplete
        {
            get { return _isFillComplete; }
            set { _isFillComplete = value; }
        }

        #endregion

        public virtual UserProfile UserProfile { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //todo001 :15 tuning order from calc to field
        #region tuning

        [Column(TypeName = "decimal(38, 8)")]
        public decimal Amount { get; set; }

        public OrderHistory GenerateOrderHistory(Transaction transaction = null)
        {
            var transactionId = transaction?.Id ?? -1;

            var order = this;
            order.Version++;
            var orderHistory = new OrderHistory()
            {

                RequestRate = order.RequestRate,
                RunningAmount = order.LeftAmount,
                CurrencyPair = order.CurrencyPair,
                IsBuy = order.IsBuy,

                RunningLeftAmount = order.LeftAmount,

                // TransactionId = transactionId,
                // OrderId = order.Id,

                Transaction = transaction,
                Order = order,
                Version = order.Version

            };
            return orderHistory;

        }


        [Column(TypeName = "decimal(38, 8)")]
        public decimal TotalTransactions
        {
            get { return _totalTransactions; }
            set { _totalTransactions = value; }
        }

        [Column(TypeName = "decimal(38, 8)")]
        public decimal LeftAmount
        {
            get { return _leftAmount; }
            set { _leftAmount = value; }
        }

        public bool IsCancelled { get; set; }



        public void SetOpenOrderCondition()
        {
            var output = IsFillComplete == false && IsCancelled == false;
            if (output)
            {
                IsOpenOrder = true;
            }
            else
            {
                IsOpenOrder = false;
            }
        }

        #endregion

        public string CurrencyPair { get; set; }
        public bool IsBuy { get; set; }


        //public OrderResult ExecuteTest(ICollection<Order> inputOrders)
        //{
        //    var output = new OrderResult();
        //    return output;
        //    //var orders = inputOrders.AsQueryable();
        //    //var currencyPair = "btc_idr";
        //    //OrderResult output = null;
        //    ////#if DEBUG
        //    ////DevOrderMatchService.Orders = orders;
        //    //var orderLogic = FakeServiceFactory.GenerateTransactionService(orders);
        //    //output = orderLogic.ExecuteSync(this, currencyPair, skipBalanceNegativeValidation: true);
        //    //// output = this.Execute(orders, currencyPair);

        //    ////#endif
        //    //return output;

        //}


        public void UpdateAmount(decimal amount)
        {
            var order = this;

            order.LeftAmount -= amount;

            if (order.LeftAmount == 0)
            {
                order.IsFillComplete = true;
                order.SetOpenOrderCondition();
            }
            else
            {
                order.IsFillComplete = false;
                order.SetOpenOrderCondition();
            }


            order.TotalTransactions += amount;


        }

        [ConcurrencyCheck]
        public long Version { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}