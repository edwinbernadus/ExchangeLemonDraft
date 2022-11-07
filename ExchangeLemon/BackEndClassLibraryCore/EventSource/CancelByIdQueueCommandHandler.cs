using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class CancelByIdQueueCommandHandler : IRequestHandler<CancelByIdQueueCommand, bool>
    {


        public async Task<bool> Handle(CancelByIdQueueCommand request,
        CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var r = new RpcClient();
            string s = r.CallTransactionExt(request);
            r.Close();
            var output = r.ReceiveTransaction<bool>(s);
            
            return output;
        }
    }
}