using System.Collections.Generic;
using BlueLight.Main;
// ;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlueLight.Main.Api {
    //[Route("api/MarketHistory/Transaction")]
    public class MarketHistoryTransactionControllerApi : Controller {
        private readonly RepoGeneric repoGeneric;

        public MarketHistoryTransactionControllerApi (RepoGeneric repoGeneric) {
            
            this.repoGeneric = repoGeneric;
        }

        [HttpGet]
        [Authorize]
        [Route("api/marketHistoryTransaction/{id}")]
        public async Task<List<MvAccountTransaction>> Get(string id)
        {
            var currencyPair = id;
            var query = this.repoGeneric.GenerateMarketHistoryTransaction(currencyPair);

            var output =
                await query
                .Take(5)
                .ToListAsync();

            return output;
            

        }
    }
}