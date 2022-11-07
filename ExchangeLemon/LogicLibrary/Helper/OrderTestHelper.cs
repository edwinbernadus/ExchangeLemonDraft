//using System.Collections.Generic;
//using System.Linq;

//namespace BlueLight.Main
//{


//    public class OrderTestHelper
//    {
//        public static OrderResult ExecuteTest(ICollection<Order> inputOrders,Order order)
//        {
//            //var output = new OrderResult();
//            //return output;
//            var orders = inputOrders.AsQueryable();
//            var currencyPair = "btc_idr";
//            OrderResult output = null;
//            //#if DEBUG
//            //DevOrderMatchService.Orders = orders;
//            var orderLogic = FakeServiceFactory.GenerateTransactionService(orders);
//            output = orderLogic.ExecuteSync(order, currencyPair, skipBalanceNegativeValidation: true);
//            // output = this.Execute(orders, currencyPair);

//            //#endif
//            return output;

//        }
//    }
//}
