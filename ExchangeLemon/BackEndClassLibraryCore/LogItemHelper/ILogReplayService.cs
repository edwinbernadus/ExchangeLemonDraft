////using System.Web.Mvc;
////using Microsoft.AspNetCore.Cors;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.EntityFrameworkCore;

//using System;
//using System.Runtime.CompilerServices;
////using System.Data.Entity;
//using System.Threading.Tasks;

//namespace BlueLight.Main
//{
//    public interface ILogReplayService
//    {
//        Task SaveLogCancel(Type type, string orderId, string userNameLogCapture, [CallerMemberName] string propertyName = null);
//        Task SaveLogSubmitOrder(Type type, InputTransactionRaw input, string userName );
//    }
//}


////[Obsolete]
////async Task Execute()
////{
////    //var orders = FactoryOrders.Generate(this._context);
////    var orders = _context.Orders;
////    var order = await orders.FirstAsync(x => x.GuidId== id);
////    var isNegative = order.Cancel();
////    await _context.SaveChangesAsync();

////    if (isNegative)
////    {
////        string event1 = $"NegativeHoldExecute";
////        var dashboardService = new dashboardService(_context);
////        await dashboardService.SendObjectStat(event1);
////        var input = event1 + "-" + userNameLogCapture;

////        var LogHelper = new LogHelper(_context);
////        LogHelper.SaveObject(input);
////    }
////}