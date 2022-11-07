using MediatR;

namespace ExchangeLemonCore.Controllers
{
    public class SendMoneyTestCommand  : IRequest<string>
    {
        //public string Address { get; }
        public int Amount { get; set; }
        public string UserName { get; set; }

        //public SendMoneyTestCommand(string address, int amount)
        //{
        //    Address = address;
        //    Amount = amount;
        //}

      
    }
}