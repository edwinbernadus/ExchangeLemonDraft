using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// ;

namespace BlueLight.Main
{
    public class OpenOrderPagingController : Controller
    {

        private ApplicationContext _context;

        public OpenOrderPagingController(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task<PagingClass<MvPendingOrderExt>> List(int counter = 1)
        {

            var userName = User.Identity.Name;

            //var uri = this.Request.Url;
            var uri = WebHelper.GetUrlToUri(Request);
            // var newUrl = DisplayHelper.GetNewUrlReportExt (uri, counter);
            var newUrl = DisplayHelper.GetNewUrlReportExtWithSuffixPaging(uri, counter);

            var userProfile = await _context.UserProfiles.FirstAsync(x => x.username == userName);

            var repo = new RepoOpenOrder(_context);
            var output = repo.GetOpenOrdersForPaging(userProfile);

            var result = new PagingClass<Order>();
            await result.PopulateQuery(output, counter, newUrl);

            var result2 = new PagingClass<MvPendingOrderExt>();
            PagingClass.Copy<Order, MvPendingOrderExt>(result, result2);
            result2.data = result.data
                .Select(x => new MvPendingOrderExt(x))
                .ToList();

            return result2;
        }


    }
}
