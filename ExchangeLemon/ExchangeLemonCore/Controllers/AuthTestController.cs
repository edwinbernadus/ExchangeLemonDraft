using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore.Models;
using ExchangeLemonCore.Models.ManageViewModels;
using ExchangeLemonCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExchangeLemonCore.Controllers {

    public class AuthTestController : Controller {

        [Authorize]
        public string Index () {
            return "woot";
        }

    }
}