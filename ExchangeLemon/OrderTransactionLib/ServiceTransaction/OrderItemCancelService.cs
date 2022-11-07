//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using System;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlueLight.Main
{
    public class OrderItemCancelService
    {

        public ApplicationContext _context;
        public OrderItemCancelService(ApplicationContext context,
            LogHelperObject LogHelper, SignalDashboard dashboardService,
            ReplayCaptureService replayCaptureService)
        {
            this._context = context;
            this._logHelper = LogHelper;
            this._signalDashboard = dashboardService;
            this._logReplayService = replayCaptureService;
        }

        public LogHelperObject _logHelper { get; }
        public SignalDashboard _signalDashboard { get; }

        private ReplayCaptureService _logReplayService;

        public async Task DirectFromOrder(long orderId, string userNameLogCapture, bool includeLog = true)
        {
            if (ParamSpecial.IsForceStop)
            {
                throw new ArgumentException("Force Stop Mode");
            }

            if (includeLog)
            {
                await _logReplayService.SaveLogCancel(GetType(), orderId.ToString(), userNameLogCapture);
            }
            var order = await _context.Orders
                .Include(x => x.UserProfile)
                .ThenInclude(x => x.UserProfileDetails)
                .FirstAsync(x => x.Id == orderId);
            var isNegative = order.Cancel();
            await _context.SaveChangesAsync();

            if (isNegative)
            {
                var username = order.UserProfile.username;
                string event1 = $"NegativeCheck-HoldBalance-CancelExecuteFromOrder-{username}";
                await _signalDashboard.Submit(event1);
                var input = event1 + "-" + userNameLogCapture;

                await _logHelper.SaveObject(input);
            }

        }



        public async Task DirectFromOrderGuid(Guid guidId, string userNameLogCapture, bool includeLog = true)
        {
            if (includeLog)
            {
                await _logReplayService.SaveLogCancel(GetType(), guidId.ToString(), userNameLogCapture);
            }
            var order = await _context.Orders
                .Include(x => x.UserProfile)
                .ThenInclude(x => x.UserProfileDetails)
                .FirstAsync(x => x.GuidId == guidId);
            var isNegative = order.Cancel();
            await _context.SaveChangesAsync();

            if (isNegative)
            {
                var username = order.UserProfile.username;
                string event1 = $"NegativeCheck-HoldBalance-CancelExecuteFromOrderGuid-{username}";
                await _signalDashboard.Submit(event1);
                var input = event1 + "-" + userNameLogCapture;

                await _logHelper.SaveObject(input);
            }

        }



    }
}


//private Task SaveLogInput(Type type, string orderId, string userNameLogCapture, [CallerMemberName] string propertyName = null)
//{
//    return Task.Delay(0);
//}


//[Obsolete]
//async Task Execute()
//{
//    //var orders = FactoryOrders.Generate(this._context);
//    var orders = _context.Orders;
//    var order = await orders.FirstAsync(x => x.GuidId== id);
//    var isNegative = order.Cancel();
//    await _context.SaveChangesAsync();

//    if (isNegative)
//    {
//        string event1 = $"NegativeHoldExecute";
//        var dashboardService = new dashboardService(_context);
//        await dashboardService.SendObjectStat(event1);
//        var input = event1 + "-" + userNameLogCapture;

//        var LogHelper = new LogHelper(_context);
//        LogHelper.SaveObject(input);
//    }
//}