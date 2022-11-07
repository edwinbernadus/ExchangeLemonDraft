using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BlueLight.Main
{

    public class TransactionNotificationService
    {
        private readonly LoggingContext context;
        private readonly ITransactionHubService transactionHubService;

        public ApplicationContext ApplicationContext { get; }
        public ILogHubService LogHubService { get; }

        public TransactionNotificationService(LoggingContext context,
            ITransactionHubService transactionHubService,
            ApplicationContext applicationContext,
            ILogHubService logHubService)
        {
            this.context = context;
            this.transactionHubService = transactionHubService;
            ApplicationContext = applicationContext;
            LogHubService = logHubService;
        }

        public async Task NewDeposit(string username, decimal amount, string currency)
        {
            Log.Information("notification to: " + username);

            //var content2 = $"New Deposit: {DisplayHelper.RateDisplay(amount)} {currency}";
            //var content2 = $"New Deposit: {currency}";
            var content2 = $"Incoming Deposit - {currency}";
            await transactionHubService.SubmitReceiveDeposit(username, content2);
            await LogHubService.SendMessage($"[{username}]-{content2}");
        }

        //public async Task NewTransferConfirmStatus(string username, long id, decimal amount)
        public async Task NewTransferConfirmStatus(string username, PendingTransferList item)
        {
            Log.Information("notification to: " + username);
            var amount = item.Amount;
            var id = item.Id;
            var createdDate = item.CreatedDate;
            var diffTime = DateTime.Now - createdDate;
            var content2 = $"Confirm Transfer Changed - {amount} BTC - ID:{id} - send transfer created {diffTime.Minutes} minutes ago";
            await transactionHubService.SubmitReceiveDeposit(username, content2);
            await LogHubService.SendMessage($"[{username}]-{content2}");
        }

        public async Task SubmitOrder(OrderResult result, Order order)
        {

            var output1 = result.OppositePlayers.Select(x => x.username).ToList();
            var output2 = result.InputOrder.UserProfile.username;

            output1.Add(output2);
            var output = output1.Distinct();



            var from = order.UserProfile.username;
            var amount = order.Amount;

            var rate = result.TransactionLastRate;

            var rate2 = DisplayHelper.RateDisplay(rate);
            var currencyPair = order.CurrencyPair;
            var message = $"{from} buy {amount} at rate: {rate2} - {currencyPair}";
            foreach (var i in output)
            {
                Log.Information("notification to: " + i);
                var user = i;
                var content2 = message;

                await transactionHubService.SubmitMatchOrder(user, content2);
            }
        }

        public async Task NewDepositFromAddress(string address, decimal amount, string currency)
        {


            var detail = await this.ApplicationContext
                .UserProfileDetails
                .Include(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.PublicAddress == address);

            await this.NewDeposit(detail.UserProfile.username, amount, currency);
        }



    }
}