using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class OrderItemUserCommandHandler : 
        IRequestHandler<OrderItemUserCommand, OrderResult>
    {
        public OrderItemUserCommandHandler(OrderItemMainService service)
        {
            Service = service;
        }

        public OrderItemMainService Service { get; }

        public async Task<OrderResult> Handle(OrderItemUserCommand request,
        CancellationToken cancellationToken)
        {
            var inputUserProfile = new InputUser(request.userProfile);
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