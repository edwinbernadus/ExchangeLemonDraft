using System;
using System.Threading.Tasks;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers
{
    public class DevValidationCancelOrderAllService : IValidationCancelOrderAllService
    {
        //public async Task<bool> Execute(LogItem item = null)
        //{
        //    await Task.Delay(0);
        //    return true;
        //}

        //public async Task<decimal> InquiryTotalHold()
        //{
        //    await Task.Delay(0);
        //    return 0;
        //}

        public async Task<Tuple<bool, decimal>> Execute(LogItemEventSource item)
        {
            await Task.Delay(0);
            return new Tuple<bool, decimal>(true, 0);
        }
    }
}