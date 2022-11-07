// using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class OrderItemNotificationService
    {
        

        private readonly TransactionNotificationService _notificationService;
        private readonly ITransactionHubService _transactionHub;
        private readonly SignalDashboard _signalDashboard;


        public Order createdOrder { get; private set; }
        public OrderResult orderResult { get; private set; }
        public string moduleName { get; private set; }

        public OrderItemNotificationService(
            ITransactionHubService transactionHubService,
            TransactionNotificationService notificationService,
            SignalDashboard signalDashboard)
        {

            this._notificationService = notificationService;
            this._transactionHub = transactionHubService;
            this._signalDashboard = signalDashboard;

        }

        internal void Populate(
            WorkingFolder workingFolder,
            OrderResult orderResult )
        {
            var w = workingFolder;

            this.createdOrder = w.CreatedOrder;
            this.moduleName = w.ModuleName;

            this.orderResult = orderResult;
            
            
        }


        async Task HandleNotificationDealOrder(string currencyPair,
            Order createdOrder, OrderResult orderResult)
        {
            var lastRate = orderResult.TransactionLastRate;
            var transactionDate = orderResult.TransactionLastRateTransactionDate;

            await HandleDashboardNotification();

            await _transactionHub.SubmitPair(currencyPair, lastRate);
            await _transactionHub.SubmitGraph(currencyPair,lastRate,transactionDate);
            await _transactionHub.SubmitMarketHistory(currencyPair);

            var targetNotificationNames = orderResult.GenerateTargetNotifName(createdOrder);
            foreach (var username in targetNotificationNames)
            {
                await _transactionHub.SubmitBalanceExt(username, currencyPair);
                await _transactionHub.SubmitHistoryTransaction(username, currencyPair);
                await _transactionHub.SubmitPendingOrder(username, currencyPair, -1, false);
            }
            await _notificationService.SubmitOrder(orderResult, createdOrder);

            orderResult.TargetNotificationNames = targetNotificationNames;
        }

        async Task HandleNotificationSubmitOrder(string currencyPair,string orderUserName)
        {
            await _transactionHub.SubmitBalanceExt(orderUserName, currencyPair);
            await _transactionHub.SubmitPendingOrder(orderUserName, currencyPair, -1, false);
        }

        public async Task HandleNotification()
        {
            var currencyPair = createdOrder.CurrencyPair;
            var isDealOrder = orderResult.IsDealOrder;
            
            if (isDealOrder)
            {
                await HandleNotificationDealOrder(currencyPair, createdOrder, orderResult);                
            }
            else
            {
                var orderUserName = createdOrder.UserProfile.username;
                await HandleNotificationSubmitOrder(currencyPair: currencyPair, orderUserName: orderUserName);
            }

            

            
            // await transactionHubService.SubmitOrderChange(currencyPair);
            await _transactionHub.SubmitOrderChangeExt(currencyPair);
            
        }

        async Task HandleDashboardNotification()
        {            
            await _signalDashboard.SubmitMatchOrderBuy(createdOrder);
            await _signalDashboard.SubmitMatchOrderSell(createdOrder);
            await _signalDashboard.Submit("matchOrder");
            
        }

     
    }
}