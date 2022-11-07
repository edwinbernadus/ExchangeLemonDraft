using System.Threading;
using System.Threading.Tasks;
using MediatR;


namespace BlueLight.Main
{
    public class SendOneCommandHandler : IRequestHandler<SendOneCommand, int>

    {
        public SendOneCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        public async Task<int> Handle(SendOneCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var output = request.Input + 100;
            var command = new SendTwoCommand()
            {
                Input = output
            };
            var output2 = await this.Mediator.Send(command);
            return output2;
        }
    }
}
