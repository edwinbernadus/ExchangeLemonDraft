using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

using Newtonsoft.Json;

namespace BlueLight.Main
{

    public partial class TransactionHub : Hub
    {
        private readonly RepoBalance _repoBalance;
        private readonly RepoOrderList _repoOrderList;


        private readonly SignalDashboard _signalDashboard;

        public TransactionHub(RepoBalance repoBalance,
                    RepoOrderList repoOrderList,

                    SignalDashboard signalDashboard,
                    RepoGraphExt repoGraphExt)
        {
            _signalDashboard = signalDashboard;
            _repoBalance = repoBalance;
            _repoOrderList = repoOrderList;

            RepoGraphExt = repoGraphExt;
        }

        public RepoGraphExt RepoGraphExt { get; }

        public async Task Register()
        {
            string name = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
        }

        public async Task SubmitHelloWorld(string input)
        {
            await Clients.All.SendAsync("ListenHelloWorld", input);

        }



        public async Task SubmitSyncBot()
        {
            await this._signalDashboard.Submit("sync-bot");
        }


        public async Task SubmitOrderChangeExt(string currencyPair)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
            // var items = await _repoOrderList.GetOrderList(currencyPair);
            // var data = JsonConvert.SerializeObject(items);
            // await Clients.All.SendAsync("ListenOrderChangeExt", currencyPair, data);
        }

        public async Task SubmitBalanceExt(string userName, string currencyPair)
        {
            var pairBalance = await this._repoBalance.Execute(userName, currencyPair);
            var data = JsonConvert.SerializeObject(pairBalance);
            await Clients.Groups(userName).SendAsync("ListenBalanceExt", currencyPair, data);
        }



        public async Task SubmitPair(string pair, double rate)
        {
            // await Clients.All.SendAsync("ListenPair", pair, rate);
            // await this.transactionHubContext.Clients.Group(currencyPair).SendAsync("listenAvailable", currencyPair, rate);
            await Clients.Group(pair).SendAsync("ListenPair", pair, rate);
        }

        public async Task SubmitMatchOrder(string userName, string content)
        {
            await Clients.Groups(userName).SendAsync("ListenMatchOrder", content);
        }


        public async Task SubmitPendingOrder(string userName, string currencyPair, int orderId, bool isCancel)
        {
            await Clients.Groups(userName).SendAsync("ListenPendingOrder", currencyPair, orderId, isCancel);

        }

        public async Task SubmitMarketHistory(string currencyPair)
        {
            await Clients.All.SendAsync("ListenMarketHistory", currencyPair);
        }

        public async Task SubmitHistoryTransaction(string userName, string currencyPair)
        {
            await Clients.Groups(userName).SendAsync("ListenHistoryTransaction", currencyPair);
        }

        public async Task SubmitGraph(string pair)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
            // //var item = await _repoGraph.GetLastItem();
            // //var rate = item.Value;
            // //var sequence = item.Sequence;
            // //await Clients.All.SendAsync("ListenGraph", pair, sequence, rate);


            // //var output = await _repoGraph.GetItems();
            // var output = await RepoGraphExt.GetItemsDraft();
            // var output2 = output
            //     .OrderBy(x => x.DateTimeSequence)
            //     .Take(25).ToList();

            // await Clients.Group(pair).SendAsync("ListenGraph", output);
        }

        // public async Task SubmitOrderChange(string currencyPair)
        // {
        //     await Clients.All.SendAsync("ListenOrderChange", currencyPair);
        // }


        // public async Task SubmitOrderChangeDetail(string input)
        // {
        //     await Clients.All.SendAsync("ListenOrderChangeDetail", input);
        // }

        public async Task RegisterCurrencyPair(string id)
        {
            var currencyPair = id;
            await Groups.AddToGroupAsync(Context.ConnectionId, currencyPair);
            Debug.WriteLine($"RegisterCurrencyPair - {currencyPair}");
        }

        public async Task UnregisterCurrencyPair(string id)
        {
            var currencyPair = id;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, currencyPair);
            Debug.WriteLine($"UnregisterCurrencyPair - {currencyPair}");
        }

    }
}