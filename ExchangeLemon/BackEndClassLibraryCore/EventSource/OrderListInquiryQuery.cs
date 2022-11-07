using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{

    public class OrderListInquiryQuery : IRequest<MvDetailSpotMarketItemContent>
    {
        public string currencyPair { get; set; }
        //public string userName { get; set; }
    }
}
