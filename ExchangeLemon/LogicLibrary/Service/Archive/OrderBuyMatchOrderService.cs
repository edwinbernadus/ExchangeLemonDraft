//using System;
//using System.Threading.Tasks;

//namespace BlueLight.Main
//{


//    public class OrderBuyMatchOrderService
//    {
//        private Order inputOrder;
//        private Order oppositeOrder;

//        public Transaction Transaction { get; private set; }

//        void PersistanceLogic()
//        {
//            //update buyOrder
//            //update sellOrder
//            //insert transaction
//            //update hold amount buyer
//            //update hold amount seller

//            UpdateLastMarket();
//            // insert order history
//        }

//        void SignalLogic()
//        {
//            // send last market
//            // send order history
//        }

//        void PersistanceHistoryLogic()
//        {
//            // insert buyer order history
//            // insert seller order history
//        }

//        public void ExecuteTwo(Order oppositeOrder, Order inputOrder, UserProfile buyer)
//        {
//            this.inputOrder = inputOrder;
//            this.oppositeOrder = oppositeOrder;

//            Transaction transaction = CreateTransaction(oppositeOrder);

//            var buyOrder = inputOrder;
//            var sellOrder = oppositeOrder;

//            var amount = transaction.Amount;
//            buyOrder.UpdateAmount(amount);
//            sellOrder.UpdateAmount(amount);

//            RemoveHoldAmount(buyer, buyOrder, transaction);
//            AddHoldAmount(buyer, buyOrder, transaction);

//            var orderHistory = AddOrderHistory(oppositeOrder, transaction);


//            this.Transaction = transaction;

//        }

//        private void AddHoldAmount(UserProfile buyer, Order order, Transaction transaction)
//        {
//            var transactionRateAmount = transaction.TransactionRate * transaction.Amount;
//            buyer.AddHold(transactionRateAmount, order, order.SecondPair);
//        }


//        private void RemoveHoldAmount(UserProfile buyer, Order order, Transaction transaction)
//        {
//            decimal requestRateAmount = AmountCalculator.Calc(order.RequestRate, transaction.Amount);
//            buyer.RemoveHold(requestRateAmount, order, order.SecondPair);
//        }

//        private void AddHoldLeftAmount(UserProfile buyer, Order order)
//        {
            
//            var amount = AmountCalculator.Calc(order.RequestRate, order.LeftAmount);
//            buyer.AddHold(amount, inputOrder, inputOrder.SecondPair);
//        }

//        private void UpdateLastMarket()
//        {
//        }

//        private OrderHistory AddOrderHistory(Order oppositeOrder, Transaction transaction)
//        {
//            var orderHistory = oppositeOrder.GenerateOrderHistory(transaction);
//            return orderHistory;
//        }


//        public static void AddHold(UserProfile buyer, Order order)
//        {
//            var inputOrder = order;
            
//            var amount = AmountCalculator.Calc(order.Amount, order.RequestRate);
//            buyer.AddHold(amount, inputOrder, inputOrder.SecondPair);
//        }

//        private Transaction CreateTransaction(Order oppositeOrder)
//        {
//            var dealAmount = Math.Min(oppositeOrder.LeftAmount, inputOrder.LeftAmount);
//            var transaction = new Transaction()
//            {
//                CurrencyPair = inputOrder.CurrencyPair,
//                Amount = dealAmount,
//                TransactionRate = oppositeOrder.RequestRate,
//                IsBuyTaker = true,
//            };
//            return transaction;
//        }
//    }


//}
