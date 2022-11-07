using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
// ;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main.Api
{

    public class OpenOrderController : Controller
    {

        private ApplicationContext _context;
        private readonly RepoUser repoUser;
        private readonly RepoOpenOrder repoOpenOrder;

        private readonly ExtRepoUser _extRepoUser;
        private readonly ExtRepoOpenOrder _extRepoOpenOrder;

        public OpenOrderController(ApplicationContext context,
                        RepoUser repoUser, RepoOpenOrder repoOpenOrder,
                        ExtRepoUser extRepoUser, ExtRepoOpenOrder extRepoOpenOrder)
        {
            _extRepoOpenOrder = extRepoOpenOrder;
            _extRepoUser = extRepoUser;
            this._context = context;
            this.repoUser = repoUser;
            this.repoOpenOrder = repoOpenOrder;
        }

        //[Authorize]
        [HttpGet]
        [Route("api/openOrder/{id}")]

        public async Task<List<MvPendingOrderLite>> Get(string id)
        {
            var currencyPair = id;

            var isDemo = false;

            if (isDemo)
            {
                var output1 = MvDetailSpotMarketItem.GenerateSample();
                var output2 = output1.Select(x => new MvPendingOrderLite()
                {
                    Amount = x.Amount + 100,
                    OrderId = x.OrderId + 2,
                    Rate = x.Rate + 3,
                    IsBuy = true,
                    //CurrencyPair = "pair_one"
                }).ToList();

                return output2;
            }
            else
            {

                var userName = User.Identity.Name;

                // var user = await repoUser.GetUser(userName);
                // var output = this.repoOpenOrder.GetOpenOrders(user, currencyPair);
                // var items = await output.ToListAsync();
                // var output2 = items.Select(x => new MvPendingOrder(x)).ToList();

                var user = await _extRepoUser.GetUserId(userName);
                var items = await _extRepoOpenOrder.GetAllOpenOrders(user.id, currencyPair);
                var output2 = items.Select(x => new MvPendingOrderLite()
                {
                    Amount = x.Amount,
                    IsBuy = x.IsBuy,
                    LeftAmount = x.LeftAmount,
                    OrderId = x.Id,
                    Rate = x.RequestRate
                }).ToList();


                return output2;
            }

        }
    }


}

