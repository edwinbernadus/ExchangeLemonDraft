using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{

    public class OrderListInquiryQueryHandler : IRequestHandler<OrderListInquiryQuery, MvDetailSpotMarketItemContent>
    {
        public OrderListInquiryQueryHandler(RepoOrderList repo)
        {
            this.repoOrderList = repo;
        }

        public RepoOrderList repoOrderList { get; }

        public async Task<MvDetailSpotMarketItemContent> 
            Handle(OrderListInquiryQuery request, CancellationToken cancellationToken)
        {
            var id = request.currencyPair;
            var output2 = await this.repoOrderList.GetOrderList(id);
            return output2;
        }
    }
}