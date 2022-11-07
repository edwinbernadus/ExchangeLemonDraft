using System;
using MediatR;

namespace BlueLight.Main
{
    public class CancelByGuidCommand : IRequest<bool>
    {
        public Guid guidId;
        public string userNameLogCapture;
        public bool includeLog = true;
    }
}