using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExchangeLemonSyncBotCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeLemonSyncBotCore.Controllers {
    public class AuthTestTwoController : Controller {

        public string Index () {
            var user = User.Identity.Name;
            return user;
        }
    }
}