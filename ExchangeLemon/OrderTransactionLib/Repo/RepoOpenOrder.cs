using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace BlueLight.Main
{
    public class RepoOpenOrder
    {
        public RepoOpenOrder(ApplicationContext context)
        {
            _context = context;
        }

        public ApplicationContext _context { get; }

        public async Task<List<Order>> GetOpenOrdersPerPairList(UserProfile userProfile, 
            string currencyPair)
        {

            IQueryable<Order> result2 = _context.Orders
                .Where(x => x.UserProfile.id == userProfile.id &&
                   x.CurrencyPair == currencyPair && x.IsOpenOrder);
            var t= await result2.ToListAsync();
            var t2 = t.OrderByDescending(x => x.CreatedDate).ToList();
            var output = t2;

            return output;
        }

        public async Task<List<int>> GetOpenOrdersList(long userProfileId)
        {

            IQueryable<Order> result2 = _context.Orders
                .Where(x => x.UserProfile.id == userProfileId &&
                   x.IsOpenOrder);

            var t = await result2
                .Select(x => new { x.Id, x.CreatedDate})
                .ToListAsync();
            var t2 = t.OrderByDescending(x => x.CreatedDate)
                .Select(x => x.Id)
                .ToList();
            var output = t2;
            return output;
        }

        public IQueryable<Order> GetOpenOrdersForPaging(UserProfile userProfile)
        {

            IQueryable<Order> result2 = _context.Orders
                .Where(x => x.UserProfile.id == userProfile.id &&
                   x.IsOpenOrder).OrderByDescending(x => x.CreatedDate);

            return result2;
        }
    }
}