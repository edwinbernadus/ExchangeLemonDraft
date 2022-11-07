using System.Threading.Tasks;
using System;
using BlueLight.Main;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ExchangeLemonCore.Controllers;
//using PCLStorage;

namespace DebugWorkplace
{
    public class ValidationCancelOrderAllService : IValidationCancelOrderAllService
    {
        private readonly IConfiguration configuration;

        public ValidationCancelOrderAllService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ApplicationContext GetContext()
        {
            var Configuration = this.configuration;
            var connString = Configuration.GetConnectionString("DefaultConnection");
            var context2 = ApplicationContext.Generate(connString);
            return context2;
        }

        //public async Task<bool> Execute(LogItem item = null)
        public async Task<Tuple<bool, decimal>> Execute(LogItemEventSource item)
        {
            var context2 = GetContext();
            var orders = await context2.Orders
            .Include(x => x.UserProfile)
            .ThenInclude(x => x.UserProfileDetails)
            .Where(x => x.IsOpenOrder).ToListAsync();
            foreach (var order in orders)
            {
                try
                {
                    order.Cancel();
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                }
            }

            decimal totalHold = await GetTotalHold(context2);
            if (totalHold != 0)
            {
                var output = new Tuple<bool, decimal>(false, totalHold);
                return output;
                //return false;
                //throw new ArgumentException("total hold not zero");
            }
            context2.Dispose();
            //return true;
            {
                var output = new Tuple<bool, decimal>(true, totalHold);
                return output;
            }
        }

        //public async Task<decimal> InquiryTotalHold()
        //{
        //    var context = GetContext();
        //    var output = await GetTotalHold(context);
        //    return output;
        //}
        private async Task<decimal> GetTotalHold(ApplicationContext context2)
        {

            var items = await context2.UserProfileDetails.ToListAsync();
            if (items.Any(x => x.HoldBalance < 0))
            {
                // throw new ArgumentException("hold negative - 2");
            }
            var total = items.Sum(x => x.HoldBalance);

            return total;
        }

        
    }
}