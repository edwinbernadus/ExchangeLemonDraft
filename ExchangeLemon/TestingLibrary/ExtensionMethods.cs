using System.Collections.Generic;
using System.Linq;


namespace BlueLight.Main
{
    public static class ExtensionMethods
    {
        //public static string UppercaseFirstLetter(this string value)
        public static OrderResult ExecuteTest(this Order input,ICollection<Order> inputOrders)
        {

            var orders = inputOrders.AsQueryable();
            
            OrderResult output = null;
            //#if DEBUG

            //var s = DependencyHelperTesting.Get();
            //var orderLogic = s.GetService<OrderTransactionService>();
            //OrderTransactionService orderLogic = GenerateTransactionService(orders);
            //output = orderLogic.ExecuteSync(value, currencyPair, skipBalanceNegativeValidation: true);
            //output = ExecuteSync(orderLogic,input, currencyPair, skipBalanceNegativeValidation: true);

            var orderLogic = FakeServiceFactory.GenerateTransactionService(orders);
            output = orderLogic.ExecuteSyncTest(input, skipBalanceNegativeValidation: true);
            //#endif
            return output;
        }

        public static OrderResult ExecuteSyncTest(this OrderTransactionService orderLogic , 
            Order order, bool skipBalanceNegativeValidation)
        {
            var currencyPair = "btc_idr";
            var t = orderLogic.Execute(order, currencyPair, skipBalanceNegativeValidation);
            var result = t.Result;
            return result;
        }

     
    }
}
