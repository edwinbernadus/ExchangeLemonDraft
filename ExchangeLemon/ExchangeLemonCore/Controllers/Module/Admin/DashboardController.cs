using System;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
// using Serilog;

namespace BlueLight.Main
{

    // [Authorize]
    public class DashboardController : Controller
    {
        private DashboardContext _context;
        private ILogger<DashboardController> logger;

        // private readonly ILoggerFactory logger;

        // private readonly IHubContext<TransactionHub> transactionHubContext;
        // private readonly ITransactionHubService serviceTransaction;

        // private DBContext _context = new DBContext();

        public DashboardController(DashboardContext context,
            ILogger<DashboardController> logger
        // ILoggerFactory logger
        )
        {
            // IHubContext<TransactionHub> transactionHubContext,
            // ITransactionHubService serviceTransaction) {
            this._context = context;
            this.logger = logger;
            // this.transactionHubContext = transactionHubContext;
            // this.serviceTransaction = serviceTransaction;
        }

        public async Task<int> TestLog()
        {

            // Log.Information("Log: Log.Information");
            // Log.Warning("Log: Log.Warning");
            // Log.Error("Log: Log.Error");
            // Log.Fatal("Log: Log.Fatal");

            this.logger.LogInformation("info 1");

            this.logger.LogWarning("warning 2");

            // var log = ApplicationLogging.CreateLogger();
            // log.LogInformation("info 3");
            await Task.Delay(0);
            return 123;
        }

        public async Task<ActionResult> Index()
        {

            // Log.Warning ("Warning 3");
            var output = await this._context.Dashboards
                .OrderBy(x => x.TypeEvent)
                .ToListAsync();
            var output1 = JsonConvert.SerializeObject(output);
            ViewBag.Items = output1;

            ViewBag.SignalConnectionUrl = ParamRepo.SignalConnectionUrl;
            ViewBag.DateTime = DateTime.Now;
            return PartialView(output);
            //return View(output);
        }

        public async Task<ActionResult> Pulse()
        {

            var output = await this._context.Pulses
                .OrderBy(x => x.ModuleName)
                .ToListAsync();
            var output1 = JsonConvert.SerializeObject(output);
            ViewBag.Items = output1;

            ViewBag.SignalConnectionUrl = ParamRepo.SignalConnectionUrl;
            ViewBag.DateTime = DateTime.Now;
            return PartialView(output);
            //return View(output);
        }


    }
}