using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main {

    public class LastRateController : Controller {

        private ApplicationContext _context;

        public LastRateController (ApplicationContext context) {
            this._context = context;

        }

        [Route ("api/lastRate/{id}")]
        [HttpGet]
        // [HttpGet("{id}")]
        // public async Task<List<OrderItem>> Get(int isBuy)
        public async Task<decimal> Get (string id) {

            var currentPair = id;

            var items = this._context.SpotMarkets;
            var currency = await items.FirstAsync (x => x.CurrencyPair == currentPair);
            var lastRate = currency.LastRate;

            return lastRate;

            //return 0.10101;
            //return lastRate;

        }
    }
}