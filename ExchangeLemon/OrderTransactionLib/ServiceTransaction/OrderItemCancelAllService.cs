//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;
using ExchangeLemonCore.Controllers;

namespace BlueLight.Main
{

    public class OrderItemCancelAllService
    {
        public ApplicationContext _context;
        private readonly OrderItemCancelService event1;

        public OrderItemCancelAllService(ApplicationContext context,
            OrderItemCancelService event1)
        {
            this._context = context;
            this.event1 = event1;
        }

        //public async Task<bool> Execute(UserProfile userProfile)
        public async Task<bool> DirectExecute(UserProfileLite input)
        {
            var userProfile = input;
            var userId = userProfile.UserId;
            var userName = userProfile.UserName;

            var repo = new RepoOpenOrder(this._context);
            var items = await repo.GetOpenOrdersList(userId);
            var list = items.ToList();

            var result = false;

            Console2.WriteLine($"Cancelling all orders - {userName} - [{list.Count}]");
            var counter = 0;
            // throw new ArgumentException("[debug-sync] two");
            foreach (var orderId in list)
            {
                counter++;
                try
                {
                    Console2.WriteLine($"Cancelling all orders - {userName} - [{counter}/{list.Count}]");
                    await event1.DirectFromOrder(orderId, userName);
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                }

                result = true;
            }
            return result;

        }
    }
}