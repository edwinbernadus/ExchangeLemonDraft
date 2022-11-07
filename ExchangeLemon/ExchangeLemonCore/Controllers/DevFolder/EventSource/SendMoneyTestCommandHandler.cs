using System.Threading;
using System.Threading.Tasks;
using BlueLight.Main;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonCore.Controllers
{
    public class SendMoneyTestCommandHandler : IRequestHandler<SendMoneyTestCommand, string>
    {
        public SendMoneyTestCommandHandler(IBtcServiceSendMoney btcServiceSendMoney,
            ApplicationContext applicationContext)
        {
            BtcServiceSendMoney = btcServiceSendMoney;
            ApplicationContext = applicationContext;
        }

        public IBtcServiceSendMoney BtcServiceSendMoney { get; }
        public ApplicationContext ApplicationContext { get; }

        public async  Task<string> Handle(SendMoneyTestCommand request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var detail = await ApplicationContext.UserProfileDetails.
                FirstAsync(x => x.UserProfile.username == userName
                                && x.CurrencyCode == "btc");
            var address = detail.PublicAddress;
            //var address = "muMtCU1BG9Bnf3XAFiLFLGAdKh1T7PqUNL";
            var s = await this.BtcServiceSendMoney.SendMoney(address, 808);
            return s;
        }
    }
}