//using System.Collections.Generic;
//using System.Linq;

//namespace BlueLight.Main
//{

//    public class TransactionServiceGenerator
//    {
//        static OrderTransactionService GenerateTransactionService(IQueryable<Order> orders)
//        {
//            var output = new OrderTransactionService(
//                new FakeContextSaveService(),
//                new DevOrderMatchService(orders),
//                new FakeLogMatchService()

//                );

//            output.IsSkipBalanceNegativeValidation = true;
//            return output;
//        }

//        private OrderBuyService GenerateOrderBuyService()
//        {
//            OrderBuyService orderBuyService = new OrderBuyService(
//                this.accountTransactionService, this.contextSaveService,
//                this.orderMatchService, this.logMatchService);
//            return orderBuyService;
//        }

//        private OrderSellService GenerateOrderSellService()
//        {
//            var orderSellService = new OrderSellService(
//                   this.accountTransactionService, this.contextSaveService,
//                   this.orderMatchService, this.logMatchService);
//            return orderSellService;
//        }
//    }
//}
