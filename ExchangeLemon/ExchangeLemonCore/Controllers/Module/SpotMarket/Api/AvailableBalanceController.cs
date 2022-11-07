using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;

//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    public class AvailableBalanceController : Controller
    {

        // private ApplicationContext _context;
        // private readonly RepoUser repoUser;

        private readonly RepoBalance _repoBalance;

        public AvailableBalanceController(
                // ApplicationContext context,
                // RepoUser repoUser, 
                RepoBalance repoBalance)
        {
            _repoBalance = repoBalance;
            // this._context = context;
            // this.repoUser = repoUser;
        }

        [Route("api/availableBalance/{id}")]
        [HttpGet]
        // [HttpGet("{id}")]
        // public async Task<List<OrderItem>> Get(int isBuy)
        public async Task<MvOutputBalanceFromPair> Get(string id)

        {

            var currentPair = id;

            bool isDemo = false;
            if (isDemo)
            {
                var output = new MvOutputBalanceFromPair()
                {
                    First = 0.54m,
                    Second = 0.79m,

                };
                return output;
            }
            else
            {
                var userName = User.Identity?.Name ?? "NoName";

                var output = await this._repoBalance.Execute(userName, currentPair);
                return output;

                // var output = new MvOutputBalanceFromPair()
                // {
                //     First = -1,
                //     Second = -1,

                // };

                // var userName = User.Identity?.Name ?? "NoName";
                // var user = await repoUser.GetUser(userName);
                // {
                //     var currency_code = PairHelper.GetFirstPair(currentPair);

                //     var detail = user.GetUserProfileDetail(currency_code);
                //     var balance = detail.AvailableBalance;

                //     output.First = balance;
                // }

                // {
                //     var currency_code = PairHelper.GetSecondPair(currentPair);
                //     var detail = user.GetUserProfileDetail(currency_code);
                //     var balance = detail.AvailableBalance;
                //     output.Second = balance;
                // }

                // return output;
            }

        }
    }
}