using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class CancelAllCommandHandler : IRequestHandler<CancelAllCommand, bool>
    {
        public CancelAllCommandHandler(OrderItemCancelAllService service)
        {
            Service = service;
        }

        public OrderItemCancelAllService Service { get; }

        public async Task<bool> Handle(CancelAllCommand request, CancellationToken cancellationToken)
        {
            var userInput = new UserProfileLite()
            {
                UserId = request.UserId,
                UserName = request.UserName
            };
            var result = await Service.DirectExecute(userInput);
            return result;
        }



    }
}