
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlueLight.Main
{


    public class FakeServiceFactory
    {
        public static OrderTransactionService GenerateTransactionService(IQueryable<Order> orders)
        {
            var s = DependencyHelperTesting.Get(orders);
            var orderLogic = s.GetService<OrderTransactionService>();

            return orderLogic;
            //var output = new OrderTransactionService(
            //    new FakeContextSaveService(),
            //    new DevOrderMatchService(orders),
            //    new FakeLogMatchService()

            //    );

            //output.IsSkipBalanceNegativeValidation = true;
            //return output;
        }
    }
}