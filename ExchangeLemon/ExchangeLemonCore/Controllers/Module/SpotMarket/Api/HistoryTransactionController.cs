using System;
using System.Collections.Generic;
using BlueLight.Main;
using Newtonsoft.Json;
// ;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    //[EnableCors("CorsPolicy"), Route("api/[controller]")]
    //[Route("/api/historyTransaction")]
    public class HistoryTransactionController : Controller
    {
        private ApplicationContext _context;

        public HistoryTransactionController(ApplicationContext context)
        {
            this._context = context;

        }

        [HttpGet]
        [Authorize]

        [Route("/api/historyTransaction/{id}")]
        //[Route("/api/reportGraph/{id}")]
        public async Task<List<AccountTransaction>> Get(string id)
        {
            var username = User.Identity.Name;
            var currentPair = id;


            var userProfile = await _context.UserProfiles
                .FirstAsync(x => x.username == username);

            var output = await _context.AccountTransactions
                .Include(x => x.Transaction)
                .Where(x => x.UserProfileDetail.UserProfile.id == userProfile.id &&
                        x.CurrencyPair == currentPair)
                .OrderByDescending(x => x.Id)
                .Take(20)
                .ToListAsync();
            return output;

        }
    }
}