using System;
using MediatR;

namespace BlueLight.Main
{

    public class CancelAllCommand : IRequest<bool>
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
    }
}