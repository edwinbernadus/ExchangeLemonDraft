using System.Threading.Tasks;

//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{
    public class RepoBalance
    {
        private readonly RepoUser repoUser;

        public RepoBalance(RepoUser repoUser)
        {
            this.repoUser = repoUser;

        }

        public async Task<MvOutputBalanceFromPair>
            Execute(string userName, string currencyPair)
        {
            var output = new MvOutputBalanceFromPair()
            {
                First = -1,
                Second = -1,

            };


            var user = await repoUser.GetUser(userName);
            {
                var currency_code = PairHelper.GetFirstPair(currencyPair);

                var detail = user.GetUserProfileDetail(currency_code);
                var balance = UserProfileLogic.GetAvailableBalance(detail);

                output.First = balance;
            }

            {
                var currency_code = PairHelper.GetSecondPair(currencyPair);
                var detail = user.GetUserProfileDetail(currency_code);
                var balance = UserProfileLogic.GetAvailableBalance(detail);
                output.Second = balance;
            }

            return output;
        }
    }
}