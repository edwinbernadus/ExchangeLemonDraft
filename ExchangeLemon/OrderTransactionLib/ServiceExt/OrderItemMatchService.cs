using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{




    public class OrderItemMatchService : IOrderMatchService
    {
        private ApplicationContext _context;

        public OrderItemMatchService(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task<Order> SellMatchMaker(Order order)
        {
            var orders = this._context.Orders;
            var oppositeOrderId = await orders
                
                .Where(x => x.IsBuy && x.IsOpenOrder &&
                x.CurrencyPair == order.CurrencyPair &&
                x.RequestRate >= order.RequestRate)
                .OrderByDescending(x => x.RequestRate)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (oppositeOrderId == 0)
            {
                return null;
            }

            var output = await _context.Orders
                .Include(x => x.Transactions)
                .Include(x => x.UserProfile)
                .ThenInclude(x => x.UserProfileDetails)
                .FirstAsync(x => x.Id == oppositeOrderId);

            return output;
        }

        public async Task<Order> BuyMatchMaker(Order order)
        {
            var orders = this._context.Orders;
            var oppositeOrderId = await orders
                .Where(x => x.IsBuy == false && x.IsOpenOrder &&
                x.CurrencyPair == order.CurrencyPair &&
                x.RequestRate <= order.RequestRate)
                .OrderBy(x => x.RequestRate)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (oppositeOrderId == 0)
            {
                return null;
            }

            var output = await _context.Orders
            .Include(x => x.Transactions)
            .Include(x => x.UserProfile)
            .ThenInclude(x => x.UserProfileDetails)
            .FirstAsync(x => x.Id == oppositeOrderId);

            return output;
        }

      
    }
}
