using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class OrderItemCommandHandler : IRequestHandler<OrderItemCommand, OrderResult>
    {
        public OrderItemCommandHandler(OrderItemMainService service)
        {
            Service = service;
        }

        public OrderItemMainService Service { get; }

        public async Task<OrderResult> Handle(OrderItemCommand request,
        CancellationToken cancellationToken)
        {
            var inputUserProfile = new InputUser(request.userName);
            var workingFolder = new WorkingFolderInput()
            {
                inputTransactionRaw = request.inputTransactionRaw,
                inputUser = inputUserProfile,
                includeLog = request.includeLog,
            };

            await Service.DirectExecuteFromHandler(workingFolder);
            var output = Service.OrderResult;
            return output;
        }

    }
}