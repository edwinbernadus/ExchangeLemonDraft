using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderItemQueueCommandHandler : IRequestHandler<OrderItemQueueCommand, OrderResult>
    {



        public async Task<OrderResult> Handle(OrderItemQueueCommand request,
        CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var r = new RpcClient();
            string s = r.CallTransactionExt(request);
            r.Close();
            var output = r.ReceiveTransaction<OrderResult>(s);
            
            return output;
        }
    }
}


// public OrderItemQueueCommandHandler(OrderItemMainService service)
// {
//     Service = service;
// }

// public OrderItemMainService Service { get; }


//var inputUserProfile = new InputUser(request.userName);
//var workingFolder = new WorkingFolderInput()
//{
//    inputTransactionRaw = request.inputTransactionRaw,
//    inputUser = inputUserProfile,
//    includeLog = request.includeLog,
//};

//await Service.DirectExecuteFromHandler(workingFolder);
//OrderResult output = Service.OrderResult;

