using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonSyncBotCore.Controllers {
    public class TestTwoController : Controller {
        private readonly ApplicationContext _context;

        public TestTwoController (ApplicationContext context) {
            _context = context;
        }

        public async Task<int> Index () {
            var total = await this._context.UserProfiles.CountAsync ();
            return total;
        }
    }
}