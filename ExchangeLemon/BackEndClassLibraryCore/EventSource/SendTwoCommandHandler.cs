using System.Threading;
using System.Threading.Tasks;
using MediatR;


namespace BlueLight.Main
{
    public class SendTwoCommandHandler : IRequestHandler<SendTwoCommand, int>

    {
        public async Task<int> Handle(SendTwoCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var output = request.Input + 1000;
            return output;
        }
    }
}
