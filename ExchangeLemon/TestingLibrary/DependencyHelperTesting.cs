using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlueLight.Main
{

public class DependencyHelperTesting
    {

        static void PopulateDevOrderMatch(IQueryable<Order> orders, 
            IServiceCollection service)     
        {
            service.AddTransient<IOrderMatchService>(s => new DevOrderMatchService(orders));
        }

        static void Init(IServiceCollection service)
        {


            service.AddTransient<IContextSaveService, FakeContextSaveService>();
            service.AddTransient<OrderTransactionService>();
            
            //service.AddTransient<IOrderMatchService,DevOrderMatchService>();
            service.AddTransient<ILogMatchService,FakeLogMatchService>();
            service.AddTransient<OrderSellService>();
            service.AddTransient<OrderBuyService>();
            service.AddTransient<AccountTransactionService>();

        }

        public static ServiceProvider Get(IQueryable<Order> orders)
        {
            var service = Create(orders);
            var output = service.BuildServiceProvider();
            return output;
        }

        public static ServiceCollection Create(IQueryable<Order> orders)
        {
            var service = new ServiceCollection();
            Init(service);
            PopulateDevOrderMatch(orders, service);
            return service;
        }
    }
}