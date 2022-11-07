using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class DebugDbKirinController : Controller
    {

        private ApplicationContext _context;

        public DebugDbKirinController(ApplicationContext context)
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