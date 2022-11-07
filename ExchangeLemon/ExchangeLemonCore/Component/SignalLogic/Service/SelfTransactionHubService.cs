using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
// using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public partial class SelfTransactionHubService : ITransactionHubService
    {
        private readonly RepoOrderList repoOrderList;
        private readonly RepoBalance _repoBalance;


        private readonly IHubContext<TransactionHub> transactionHubContext;
        public SelfTransactionHubService(
                RepoOrderList repoOrderList,
                RepoBalance repoBalance,
                IHubContext<TransactionHub> transactionHubContext,
                RepoGraphExt repoGraphExt)
        {
            this._repoBalance = repoBalance;
            this.repoOrderList = repoOrderList;
            this.transactionHubContext = transactionHubContext;
            this.RepoGraphExt = repoGraphExt;
        }

        public IHubClients Clients
        {
            get
            {
                return this.transactionHubContext.Clients;
            }
        }

        public RepoGraphExt RepoGraphExt { get; }

        public async Task Debug(string content)
        {
            await Clients.All.SendAsync("ListenHelloWorld", content);
            //await transactionHubContext.Clients.All.SendAsync("SubmitHelloWorld", content);

            //HubConnection connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitHelloWorld", content);
        }


        public async Task SubmitBalanceExt(string userName, string currencyPair)
        {
            var pairBalance = await this._repoBalance.Execute(userName, currencyPair);
            var data = JsonConvert.SerializeObject(pairBalance);

            await Clients.Groups(userName).SendAsync("ListenBalanceExt", currencyPair, data);

        }

        public async Task SubmitOrderChangeExt(string currencyPair)
        {

            var items = await repoOrderList.GetOrderList(currencyPair);
            var data = JsonConvert.SerializeObject(items);
            await transactionHubContext.Clients.All.SendAsync("ListenOrderChangeExt", currencyPair, data);
            //var connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitOrderChangeExt", currencyPair, data);
        }

        public async Task SubmitPair(string currencyPair, decimal rate)
        {
            //var connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitPair", currencyPair, rate);

            await Clients.Group(currencyPair).SendAsync("ListenPair", currencyPair, rate);
            //await transactionHubContext.Clients.All.SendAsync("SubmitPair", currencyPair, rate);
        }


        public async Task SubmitPendingOrder(string userName, string currencyPair, int orderId, bool isCancel)
        {
            //var connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitPendingOrder", userName, currencyPair, orderId, isCancel);

            await Clients.Groups(userName).SendAsync("ListenPendingOrder", currencyPair, orderId, isCancel);
            //await transactionHubContext.Clients.All.SendAsync("SubmitPendingOrder", userName, currencyPair,orderId,isCancel);
        }

        public async Task SubmitMarketHistory(string currencyPair)
        {
            //var connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitMarketHistory", currencyPair);


            await Clients.All.SendAsync("ListenMarketHistory", currencyPair);
            //await transactionHubContext.Clients.All.SendAsync("SubmitMarketHistory", currencyPair);
        }

        public async Task SubmitHistoryTransaction(string userName, string currencyPair)
        {
            //var connection = await GenerateAndStart();
            //await connection.InvokeAsync("SubmitHistoryTransaction", userName, currencyPair);

            await Clients.Groups(userName).SendAsync("ListenHistoryTransaction", currencyPair);
            //await transactionHubContext.Clients.All.SendAsync("SubmitHistoryTransaction", userName, currencyPair);
        }

        public async Task SubmitMatchOrder(string user, string content)
        {
            await Clients.Groups(user).SendAsync("ListenMatchOrder", content);
        }



        public async Task SubmitGraph(string pair, decimal lastRate, DateTime transactionDate)
        {


            var output = await RepoGraphExt.GetItemsDraft();

            if (output.Count > 0)
            {
                var output2 = output
                    .OrderBy(x => x.DateTimeSequence)
                    .ToList()
                    .Last();

                ModelTableTwo result3 = ModelTableTwo.Convert(output2);
                await Clients.Group(pair).SendAsync("ListenGraph", result3);
            }

        }

        public async Task SubmitReceiveDeposit(string username, string content2)
        {
            await Clients.Groups(username).SendAsync("ListenReceiveDeposit", content2);
        }

        public async Task ConsoleLog(string input)
        {
            await Task.Delay(0);

        }

        public async Task SubmitDashboard(string result)
        {
            await this.Clients.All.SendAsync("ListenDashboard", result);
        }




        //Task<HubConnection> GenerateAndStart()
        //{
        //    var item = this.signalFactory.Generate();
        //    return item;
        //}



    }
}
