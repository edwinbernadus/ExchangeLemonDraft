using System;
using System.Threading.Tasks;

namespace BlueLight.Main
{

    public partial class FakeTransactionHubService : ITransactionHubService
    {
        public async Task ConsoleLog(string input)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public async Task Debug(string content)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }


        public async Task SubmitOrderChangeExt(string currencyPair)
        {
            await Task.Delay(0);
            // throw new NotImplementedException();
        }

        public async Task SubmitBalanceExt(string userName, string currencyPair)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public Task SubmitDashboard(string result)
        {
            return Task.Delay(0);
        }

        public async Task SubmitGraph(string pair, decimal lastRate, DateTime transactionDate)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public async Task SubmitHistoryTransaction(string userName, string currencyPair)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public async Task SubmitMarketHistory(string currencyPair)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public Task SubmitMatchOrder(string user, string content2)
        {
            return Task.Delay(0);

        }



        public async Task SubmitPair(string currencyPair, decimal rate)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public async Task SubmitPendingOrder(string userName, string currencyPair, int orderId, bool isCancel)
        {
            await Task.Delay(0);
            //throw new NotImplementedException();
        }

        public async Task SubmitReceiveDeposit(string username, string content2)
        {
            await Task.Delay(0);
            // throw new NotImplementedException();
        }


    }
}