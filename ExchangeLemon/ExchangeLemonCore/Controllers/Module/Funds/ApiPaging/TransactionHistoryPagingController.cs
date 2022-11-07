using System;
using Microsoft.AspNetCore.Mvc;
//// ;
using System.Linq;
//using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
namespace BlueLight.Main
{

    public class TransactionHistoryPagingController : Controller
    {

        private ApplicationContext _context;
        private readonly RepoUser repoUser;

        public TransactionHistoryPagingController(ApplicationContext context,
        RepoUser repoUser)
        {
            this._context = context;
            this.repoUser = repoUser;
        }

        // [Route ("/api/historyTransactionExternal/{id}&counter={counter}")]
        // [Route ("/api/historyTransactionExternal/{id}")]
        public async Task<PagingClass<AccountTransaction>> List(string id, int counter = 1)
        {
            var currency = id;
            var uri = WebHelper.GetUrlToUri(Request);
            var newUrl = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri, counter);
            bool isDemo = false;
            if (isDemo)
            {
                var output = Enumerable.Range(0, 20)
                    .Select(x => new AccountTransaction()
                    {
                        CreatedDate = DateTime.Now,
                        Amount = x
                    });

                var result = new PagingClass<AccountTransaction>();
                return result;
            }
            else
            {
                var userName = this.User.Identity?.Name ?? "NoName";
                var user = await repoUser.GetUser(userName);
                var detail = user.GetUserProfileDetail(currency);
                IQueryable<AccountTransaction> output = _context.AccountTransactions
                    .Where(x => x.IsExternal && x.UserProfileDetail.Id == detail.Id)
                    .OrderByDescending(x => x.Id);

                var result = new PagingClass<AccountTransaction>();
                await result.PopulateQuery(output, counter, newUrl, itemPerPage: 7);
                AdjustAmount(result);
                return result;
            }

        }

        private void AdjustAmount(PagingClass<AccountTransaction> result)
        {
            var items = result.data;
            foreach (var item in items)
            {
                if (item.DebitCreditType == "D")
                {
                    item.Amount = item.Amount * -1;
                }
            }
        }
    }
}