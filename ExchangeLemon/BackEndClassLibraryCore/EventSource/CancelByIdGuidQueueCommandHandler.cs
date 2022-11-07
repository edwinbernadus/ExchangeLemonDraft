using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class CancelByIdGuidQueueCommandHandler : IRequestHandler<CancelByGuidQueueCommand, bool>
    {


        public async Task<bool> Handle(CancelByGuidQueueCommand request,
        CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var r = new RpcClient();
            string s2 = r.CallTransactionExt(request);
            r.Close();
            var output = r.ReceiveTransaction<bool>(s2);
            
            return output;
        }
    }



}


// var tempOutput = JsonConvert.DeserializeObject<RpcOutput>(s2);
// var content = tempOutput.Content;
// if (tempOutput.IsError)
// {
//     throw new ArgumentException(content);
// }


// var s = content;
