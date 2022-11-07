using MediatR;


namespace BlueLight.Main
{
    public class SendOneCommand : IRequest<int>
    {
        public int Input { get; set; }
    }
}
