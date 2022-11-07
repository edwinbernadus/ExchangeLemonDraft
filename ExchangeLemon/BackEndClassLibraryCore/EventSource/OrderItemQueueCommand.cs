using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BlueLight.Main
{
    public class OrderItemQueueCommand : IRequest<OrderResult>
    {
        public InputTransactionRaw inputTransactionRaw;
        public bool includeLog { get; set; } = true;
        public string userName { get; set; }
    }



}
