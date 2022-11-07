using System;
using System.Threading;
using System.Threading.Tasks;
using BlueLight.Main;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ExchangeLemonCore.Controllers
{
    public class DebugSignalController : Controller
    {
        string userName = "guest1@server.com";
        private string currencyPair = CurrencyParam.BtcPair;
        
        private readonly ITransactionHubService transactionHubService;
        private readonly IHubContext<TransactionHub> transactionHubContext;

        public DebugSignalController(ITransactionHubService serviceTransaction,
            IHubContext<TransactionHub> transactionHubContext,
            TransactionNotificationService notificationService,
            IBtcServiceSendMoney btcServiceSendMoney,
            ApplicationContext applicationContext,
            IMediator mediator)
        {
            this.transactionHubService = serviceTransaction;
            this.transactionHubContext = transactionHubContext;
            NotificationService = notificationService;
            BtcServiceSendMoney = btcServiceSendMoney;
            ApplicationContext = applicationContext;
            Mediator = mediator;
        }

        // http://localhost:53252/debugSignal/sendBtc
        public async Task<string> SendBtc()
        {
            try
            {
                //TODO: [item9] Event source send money test
                var command = new SendMoneyTestCommand(){
                    UserName =  this.userName,
                    //address,
                    Amount = 808
                };
                
                var s = await Mediator.Send(command);
                
                return s;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

        //// http://localhost:53252/debugSignal/sendRpc
        //public async Task<int> SendRpc()
        //{
        //    var cts = new CancellationTokenSource();
        //    var t = Client.ConnectAsync("ws://localhost:8001/", cts.Token, c =>
        //    {
        //        c.Bind<ProgressAPI, ITaskAPI>(new ProgressAPI());
        //        c.OnOpen += async () =>
        //        {
        //            var r = await RPC.For<ITaskAPI>().CallAsync(x => x.LongRunningTask(5, 3));
        //            Console.WriteLine("\nResult: " + r.First());
        //        };
        //    },


        //}

        public async Task<bool> SubmitBtcUsd(double id)
        {
            var rate = id;

            var currencyPair = "btc_usd";
            await this.transactionHubContext.Clients.Group(currencyPair).SendAsync("listenAvailable", currencyPair, rate);
            return true;
        }

        public async Task<bool> SubmitEthUsd(double id)
        {
            var rate = id;
            var currencyPair = "eth_usd";
            await this.transactionHubContext.Clients.Group(currencyPair).SendAsync("listenAvailable", currencyPair, rate);
            return true;
        }


        // http://localhost:5000/debugSignal/submitReceiveDeposit
        public async Task<bool> SubmitReceiveDeposit()
        {

            //var message = "Receive amount XXX";
            //var userName = "guest3@server.com";

            //await this.transactionHubContext.Clients.Group(userName).SendAsync("SubmitReceiveTransfer", userName, message);
            //await this.transactionHubContext.Clients.Group(userName).SendAsync("ListenReceiveTransfer", userName, message);
            //await serviceTransaction.SubmitReceiveTransfer(userName, message);

            await this.NotificationService.NewDeposit(userName, 10.234m, "btc");
            return true;
        }

        public async Task<bool> SubmitPair(decimal id)
        {
            var rate = id;
            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.SubmitPair(currencyPair, rate);
            return true;
        }

        public async Task<bool> SubmitPairExt(decimal id, string currencyPairInput)
        {
            var rate = id;
            await transactionHubService.SubmitPair(currencyPairInput, rate);
            return true;
        }

        public async Task<bool> SubmitBalance()
        {
            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.SubmitBalanceExt(userName, currencyPair);
            return true;
        }

        public async Task SubmitPendingOrder(int id, bool isCancel = false)
        {
            var orderId = id;
            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.SubmitPendingOrder(userName, currencyPair, orderId, isCancel);
        }

        public async Task SubmitMarketHistory()
        {
            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.SubmitMarketHistory(currencyPair);
        }
        public string UserName { get; }
        public TransactionNotificationService NotificationService { get; }
        public BtcServiceServerSendMoney BtcServiceServerSendMoney { get; }
        public IBtcServiceSendMoney BtcServiceSendMoney { get; }
        public ApplicationContext ApplicationContext { get; }
        public IMediator Mediator { get; }

        public async Task SubmitHistoryTransaction()
        {
            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.SubmitHistoryTransaction(userName, currencyPair);
        }

        public async Task SubmitGraph(double id)
        {
            var rate = id;
            // var serviceTransaction = new TransactionHubService ();
            var pair = currencyPair;
            await transactionHubService.SubmitGraph(pair, -100, DateTime.Now);
        }

        public ActionResult Listener()
        {
            return View();
        }

        public ActionResult TestSignal()
        {
            return View();
        }

        public async Task<string> TestSignalLog(string id)
        {
            var input = id;
            var output = $"Send: {input}";

            // await TransactionHubProxy.ConsoleLog (input);

            // var serviceTransaction = new TransactionHubService ();
            await transactionHubService.ConsoleLog(input);

            return output;
        }


        // http://localhost:5000/debugSignal/testDashboard

        public async Task<ActionResult> TestDashboard()
        {
            // var type1 = "logTestTwo";
            // var type1 = "test-dashboard";
            var type1 = "hello woot woot";


            await this.transactionHubService.SubmitDashboard(type1);
            //await this.transactionHubContext.Clients.All.SendAsync("ListenDashboard", type1);

            await transactionHubService.Debug(type1);

            return Content("ok");
        }

        public async Task<ActionResult> TestDashboard2()
        {
            // var type1 = "logTestTwo";
            // var type1 = "test-dashboard";
            var type1 = "hello woot woot TWO TWO";


            await this.transactionHubContext.Clients.All.SendAsync("ListenHelloWorld", type1);

            return Content("ok");
        }

    }
}