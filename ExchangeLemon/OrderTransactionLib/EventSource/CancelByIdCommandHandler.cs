using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class CancelByIdCommandHandler : IRequestHandler<CancelByIdCommand, bool>
    {
        public CancelByIdCommandHandler(OrderItemCancelService service)
        {
            Service = service;
        }

        public OrderItemCancelService Service { get; }

        public async Task<bool> Handle(CancelByIdCommand request, CancellationToken cancellationToken)
        {
            //throw new ArgumentException("one");
            await Service.DirectFromOrder(request.orderId,
                request.userNameLogCapture, request.includeLog);
            return true;
        }
    }
}