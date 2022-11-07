//using System.Web.Mvc;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
//using System.Data.Entity;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class ReplayCaptureService
    //: ILogReplayService
    {
        public ReplayCaptureService(LoggingExtContext loggingExtContext)
        {
            _loggingExtContext = loggingExtContext;
        }

        public LoggingExtContext _loggingExtContext { get; }

        string ModuleName = "ReplayMode";

        private decimal LastCancelAllBalance
        {
            get
            {
                return ParamSpecial.LastCancelAllBalance;
            }
        }

        public async Task SaveLogCancel(Type type, string orderId, string userNameLogCapture,
        [CallerMemberName] string propertyName = null)
        {

            var t = new LogItem()
            {
                //ModuleName = this.ModuleName,
                //ClassName = type.ToString(),
                //Content = orderId,
                //CallerName = propertyName,
                //AddtionalContent = userNameLogCapture

                ModuleName = this.ModuleName,
                ClassName = type.ToString(),

                Content = orderId,
                CallerName = propertyName,
                UserName = userNameLogCapture,
                AddtionalContent = LastCancelAllBalance.ToString()
            };
            _loggingExtContext.LogItems.Add(t);
            await _loggingExtContext.SaveChangesAsync();
        }

        public async Task<Guid> SaveLogSubmitOrder(Type type, InputTransactionRaw input, string userName)
        {

            var sessionId = Guid.NewGuid();
            var inputJson = JsonConvert.SerializeObject(input);
            var t = new LogItem()
            {
                ModuleName = this.ModuleName,
                ClassName = type.ToString(),


                Content = inputJson,
                UserName = userName,
                AddtionalContent = LastCancelAllBalance.ToString(),
                SessionId = sessionId.ToString()

                //ModuleName = this.ModuleName,
                //ClassName = type.ToString(),
                //Content = inputJson,
                //AddtionalContent = userName
            };
            _loggingExtContext.LogItems.Add(t);
            await _loggingExtContext.SaveChangesAsync();
            return sessionId;
        }
    }
}


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