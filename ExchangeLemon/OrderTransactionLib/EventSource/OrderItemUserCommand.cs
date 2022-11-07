using MediatR;
using System;

namespace BlueLight.Main
{
    public class OrderItemUserCommand : IRequest<OrderResult>
    {
        internal UserProfile userProfile { get; set; }
        internal InputTransactionRaw inputTransactionRaw { get; set; }
        internal bool includeLog { get; set; } = true;


    }
}