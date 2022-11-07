using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class CancelAllQueueCommandHandler : IRequestHandler<CancelAllQueueCommand, bool>
    {


        public async Task<bool> Handle(CancelAllQueueCommand request,
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
