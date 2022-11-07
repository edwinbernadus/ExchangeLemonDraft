using System.Collections.Generic;
// ;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main.Api
{

    public class OrderListApiController : Controller
    {

        private ApplicationContext _context;
        private readonly RepoUser repoUser;

        public OrderListApiController(ApplicationContext context,
        RepoUser repoUser)
        {
            this._context = context;
            this.repoUser = repoUser;
        }

        //[Authorize]
        //http://localhost:53252/api/orderlist/btc_usd/10
        [HttpGet]
        [Route("api/orderList/{currencyPair}/{sequence}")]

        public async Task<List<MvOrderHistory>> Get(string currencyPair, long sequence)
        {


            var isDemo = false;


            if (isDemo)
            {
                var output = new List<OrderHistory>();
                output.Add(new OrderHistory()
                {
                    Id = 1,
                    CurrencyPair = "abc"
                });
                output.Add(new OrderHistory()
                {
                    Id = 2,
                    CurrencyPair = "def"
                });

                var result = output.Select(x => new MvOrderHistory(x)).ToList();
                return result;


            }
            else
            {
                var output = await _context.OrderHistories
                    .Include(x => x.Order)
                    .Include(x => x.Transaction)
                    .Where(x => x.CurrencyPair == currencyPair
                    && x.Id > sequence)
                    .Take(50)
                    .OrderBy(x => x.Id)
                    .ToListAsync();

                var result = output.Select(x => new MvOrderHistory(x)).ToList();
                return result;


            }

        }
    }
}
