using MediatR;

namespace BlueLight.Main
{
    public class CancelByIdQueueCommand : IRequest<bool>
    {
        public long orderId;
        public string userNameLogCapture;
        public bool includeLog = true;
    }



}
