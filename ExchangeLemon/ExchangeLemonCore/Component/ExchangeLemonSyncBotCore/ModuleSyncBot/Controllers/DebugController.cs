using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonSyncBotCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonSyncBotCore.Controllers {

    public class DebugSyncController : Controller {
        private readonly ApplicationContext context;

        public DebugSyncController (ApplicationContext context) {
            this.context = context;
        }

        public string Version () {
            var localVersion = 3;
            var globalVersion = BlueLight.Main.VersionItem.ParamVersion;
            var output = $"wave - {localVersion} - {globalVersion}";
            return output;

        }

        public async Task<int> Test () {
            var total = await this.context.Users.CountAsync ();
            return total;
        }
    }
}