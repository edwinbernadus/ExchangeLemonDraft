using System.Linq;
using BlueLight.Main;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeLemonCore.Controllers
{
    public class DebugAuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DebugAuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
            
        // /debugAuth/GetListUser
        public string GetListUser()
        {
            var items = this.userManager.Users.Select(x => x.Email).ToList();
            var output = string.Join(" - ",  items);
            return output;
        }

    }
}