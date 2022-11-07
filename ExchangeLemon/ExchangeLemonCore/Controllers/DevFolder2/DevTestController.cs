using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BlueLight.Main;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonCore.Controllers
{
    public class DevTestController : Controller
    {
        private readonly RepoUser _repoUser;

        private readonly ApplicationContext _applicationContext;

        private readonly OrderItemMainService _orderItemMainService;

        public DevTestController(RepoUser repoUser,
        ApplicationContext applicationContext,
        OrderItemMainService orderItemMainService,
        TransactionNotificationService notificationService,
        IMediator mediator
            )
        {
            _orderItemMainService = orderItemMainService;
            NotificationService = notificationService;
            _applicationContext = applicationContext;
            _repoUser = repoUser;
            Mediator = mediator;
        }

        public TransactionNotificationService NotificationService { get; }
        public IMediator Mediator { get; }

        public async Task<bool> CreateUser(string id)
        {
            var userName = id;
            await _repoUser.GetOrPopulateUserProfile(userName);
            string const_userName = "performance_user100";
            var user = await _applicationContext.UserProfiles
            .FirstOrDefaultAsync(x => x.username == const_userName);

            var isExists = user != null;

            return isExists;

        }

        // http://localhost:5000/devTest/testNotif
        public async Task TestNotif()
        {
            string username = "guest1@server.com";
            decimal amount = 10.3m;
            string currency = "eth";
            await this.NotificationService.NewDeposit(username, amount, currency);
        }
        // public async Task Transaction(string id,
        //     double amount, int rate, bool isBuy)
        public async Task<long> Transaction(MvInputTransactionDevTest input)
        {
            var userName = input.id;
            var input2 = input.Export();


            //var event1 = this._orderItemMainService;
            //await event1.DirectExecute(input2, userName);

            var command = new OrderItemCommand()
            {
                inputTransactionRaw = input2,
                userName = userName,
            };
            OrderResult result = await Mediator.Send(command);

            
            var total = await _applicationContext.Orders.CountAsync();
            Console2.WriteLine($"Total: {total}");


            return total;
        }
    }


}
