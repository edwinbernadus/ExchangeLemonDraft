using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class DevOrderMatchService : IOrderMatchService
    {
        IQueryable<Order> orders;
        public DevOrderMatchService(IQueryable<Order> orders)
        {
            this.orders = orders;
        }

        //public void Init (IQueryable<Order> orders)
        //{
        //    this.orders = orders;
        //}


        //public static IQueryable<Order> Orders { get; internal set; }

        public async Task<Order> SellMatchMaker(Order order)
        {
            //if (Orders == null)
            //{
            //    return null;
            //}
            await Task.Delay(0);
            //this.orders = Orders;
            
            var oppositeOrder = orders
                .Where(x => x.IsBuy && x.IsOpenOrder &&
                x.CurrencyPair == order.CurrencyPair)
                .OrderByDescending(x => x.RequestRate)
                // .OrderBy(x => x.RequestRate)
                .FirstOrDefault(x => x.RequestRate >= order.RequestRate);

            return oppositeOrder;
        }

   
        public async Task<Order> BuyMatchMaker(Order order)
        {
            //if (Orders == null)
            //{
            //    return null;
            //}

            await Task.Delay(0);
            //this.orders = Orders;

            var oppositeOrder = orders
                .Where(x => x.IsBuy == false && x.IsOpenOrder &&
                x.CurrencyPair == order.CurrencyPair)
                .OrderBy(x => x.RequestRate)
                .FirstOrDefault(x => x.RequestRate <= order.RequestRate);
            return oppositeOrder;
        }

    
    }
}