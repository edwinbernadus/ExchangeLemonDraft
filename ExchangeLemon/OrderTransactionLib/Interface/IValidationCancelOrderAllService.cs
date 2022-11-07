using System;
using System.Threading.Tasks;
using BlueLight.Main;

namespace ExchangeLemonCore.Controllers
{
    public interface IValidationCancelOrderAllService
    {
        Task<Tuple<bool,decimal>> Execute(LogItemEventSource item = null);
        //Task<decimal> InquiryTotalHold();
    }
}