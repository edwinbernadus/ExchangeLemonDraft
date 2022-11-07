using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Serilog;

namespace BlueLight.Main
{
    public class DashboardDebugController : Controller
    {
        private readonly SignalDashboard _signalDashboard;

        private readonly LogHelperObject _logHelperObject;
        private readonly DashboardContext dashboardContext;

        public DashboardDebugController(SignalDashboard signalDashboard,
        LogHelperObject logHelperObject,DashboardContext dashboardContext)
        {
            _logHelperObject = logHelperObject;
            this.dashboardContext = dashboardContext;
            _signalDashboard = signalDashboard;

        }

        // http://localhost:5000/DashboardDebug/CreateNegativeLog

        public async Task<bool> CreateNegativeLog()
        {
            var item = new LogDetail()
            {
                Content = "woot",
                ModuleName = "module-one"
            };
            dashboardContext.LogDetails.Add(item);
            await dashboardContext.SaveChangesAsync();
            return true;
        }

        //negative
        // http://localhost:5000/DashboardDebug/TestCallNegativeHoldCancelAll
        //positive
        // http://localhost:5000/DashboardDebug/TestCallNegativeHoldCancelAll/1
            public async Task TestCallNegativeHoldCancelAll(int id = 0)
        {
            var user = new UserProfile()
            {
                username = "user-woot"
            };

            var ModuleName = "DashboardDebug";
            var itemService = this;

            var isValid = id == 1;
            if (isValid == false)
            {
                string event1 = $"NegativeCheck-CancelAllHoldBalance-{ModuleName}-{user.username}";
                await itemService._signalDashboard.Submit(event1);
                var input = event1 + "-" + user.username;
                await itemService._logHelperObject.SaveObject(input);
            }
            else
            {
                string event1 = $"PositiveCheck-CancelAllHoldBalance-{ModuleName}-{user.username}";
                await itemService._signalDashboard.Submit(event1);
                var input = event1 + "-" + user.username;
                await itemService._logHelperObject.SaveObject(input);
            }
        }
    }
}