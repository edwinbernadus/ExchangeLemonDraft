using MediatR;


namespace BlueLight.Main
{
    public class SendTwoCommand : IRequest<int>
    {
        public int Input { get; set; }
    }
}
