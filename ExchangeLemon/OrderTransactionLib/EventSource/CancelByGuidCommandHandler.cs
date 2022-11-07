using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class CancelByGuidCommandHandler : IRequestHandler<CancelByGuidCommand,bool>
    {
        public CancelByGuidCommandHandler(OrderItemCancelService service)
        {
            Service = service;
        }

        public OrderItemCancelService Service { get; }

        public async Task<bool> Handle(CancelByGuidCommand request, CancellationToken cancellationToken)
        {
            await Service.DirectFromOrderGuid(request.guidId, 
                request.userNameLogCapture, request.includeLog);
            return true;
        }
    }
}