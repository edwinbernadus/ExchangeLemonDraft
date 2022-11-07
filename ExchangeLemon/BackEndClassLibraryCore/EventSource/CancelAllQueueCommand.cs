using MediatR;

namespace BlueLight.Main
{
    public class CancelAllQueueCommand : IRequest<bool>
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
    }


}
