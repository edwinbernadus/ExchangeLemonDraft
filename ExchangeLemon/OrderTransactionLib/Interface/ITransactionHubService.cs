using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface ITransactionHubService
    {
        Task ConsoleLog(string input);
        Task Debug(string content);

        Task SubmitOrderChangeExt(string currencyPair);
        Task SubmitBalanceExt(string userName, string currencyPair);
        // Task SubmitBalance(string userName, string currencyPair);
        Task SubmitGraph(string pair, decimal lastRate, DateTime transactionDate);
        Task SubmitHistoryTransaction(string userName, string currencyPair);
        Task SubmitMarketHistory(string currencyPair);
        Task SubmitPair(string currencyPair, decimal rate);
        Task SubmitPendingOrder(string userName, string currencyPair, int orderId, bool isCancel);


        Task SubmitMatchOrder(string user, string content2);


        Task SubmitReceiveDeposit(string username, string content2);
        Task SubmitDashboard(string result);

        // Task SubmitOrderChange(string currencyPair);
        //Task SubmitOrderChangeDetail(OrderHistory orderHistory);
    }
}