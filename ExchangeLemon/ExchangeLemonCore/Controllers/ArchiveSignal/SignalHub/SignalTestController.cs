// using System.Threading.Tasks;
// using BlueLight.Main;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.SignalR;
// using Microsoft.AspNetCore.SignalR.Client;

// // using Microsoft.AspNet.SignalR;
// // using Microsoft.AspNet.SignalR.Infrastructure;
// // // using Microsoft.AspNet.Mvc;

// public class SignalTestController : Controller {

//     //    public TestController(IConnectionManager connectionManager)
//     //  {
//     //      testHub = connectionManager.GetHubContext<TestHub>();
//     //  }

//     //  public HomeController(IHubContext<LiveHub> hubContext)
//     //     {
//     //         _hubContext = hubContext;
//     //     }

//     // private readonly IHubContext<PairHub> _pairContext;

//     // protected SignalTestController (IHubContext<PairHub> pairContext) {
//     //     this._pairContext = pairContext;
//     // }

//     private readonly PairHub _pairHub;
//     private IHubContext<PairHub> _pairContext;

//     // protected SignalTestController (PairHub pairHub) {
//     //     this._pairHub = pairHub;
//     // }

//     public async Task<string> SendRegister () {
//         // this.pairContext = pairContext;
//         return "One";
//     }

//     public async Task<string> SendPair (string currencyPair, double rate) {

//         var url = "http://localhost:5000/signal/pair";
//         var connection = new HubConnectionBuilder ()
//             .WithUrl (url)
//             .Build ();
//         await connection.StartAsync ();
//         await connection.InvokeAsync ("Submit", currencyPair, rate);

//         // var currencyPair = "btc_usd";
//         // var rate = 5000;
//         // await TransactionHubProxy.UpdateCurrencyPair (currencyPair, rate);

//         // new {
//         //     pair = currencyPair.ToString (),
//         //         rate = rate.ToString ()
//         // });

//         // await _pairHub.Clients.All.SendAsync ("Submit", new {
//         //     currencyPair = currencyPair,
//         //         rate = rate
//         // });

//         // await pairHub.Submit (currencyPair, rate);
//         return "One";
//     }

// }