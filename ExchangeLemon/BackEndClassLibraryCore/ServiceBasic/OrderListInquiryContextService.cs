
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderListInquiryContextService : IOrderListInquiryContextService
    {
        private readonly ApplicationContext _context;
        private readonly OrderListInquiryQueryService _orderListInquiryService;

        public OrderListInquiryContextService(ApplicationContext applicationContext,
            OrderListInquiryQueryService orderListInquiryService)
        {
            this._context = applicationContext;
            this._orderListInquiryService = orderListInquiryService;
        }
        public async Task<List<Order>> GetItemsBuy(
         int take, string currentPair)
        {
            var items = _context.Orders.Include(x => x.UserProfile); ;
            var col0 = _orderListInquiryService.GetItemsBuyLogic(items, take, currentPair);
            List<Order> output = await col0.ToListAsync();
            return output;
        }

        public async Task<List<Order>> GetItemsSell(
         int take, string currentPair)
        {
            var items = _context.Orders.Include(x => x.UserProfile);
            var col0 = _orderListInquiryService.GetItemsSellLogic(items, take, currentPair);
            List<Order> output = await col0.ToListAsync();
            return output;
        }

        public async Task<List<Order>> GetItemsKirin(
         string currencyPair)
        {
            IOrderedQueryable<Order> queryOrders = _context.Orders
             .Where(x => x.IsOpenOrder == false && x.CurrencyPair == currencyPair)
             .Take(50)
             .OrderByDescending(x => x.Id);
            List<Order> orders = await queryOrders
                .ToListAsync();

            return orders;
        }

        public Task ClearCache(string pair)
        {
            return Task.Delay(0);
        }
    }
}