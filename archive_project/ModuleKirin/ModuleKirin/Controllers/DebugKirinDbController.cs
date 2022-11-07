//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BlueLight.Main;
namespace BlueLight.KirinLogic
{
    public class DebugKirinDbController : Controller
    {

        private ApplicationContext _context;

        public DebugKirinDbController(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task<int> Index()
        {
            var total = await _context.Orders.CountAsync();
            return total;
        }

    }

}