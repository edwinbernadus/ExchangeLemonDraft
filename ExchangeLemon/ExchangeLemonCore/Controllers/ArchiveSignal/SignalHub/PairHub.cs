// using System.Threading.Tasks;
// using Microsoft.AspNetCore.SignalR;

// public class PairHub : Hub {
//     public async Task Submit (string pair, string rate) {
//         await Clients.All.SendAsync ("Listen", pair, rate);
//     }

//     public async Task Execute (string pair, string rate) {
//         await Clients.All.SendAsync ("Execute", pair, rate, "woot");
//     }
// }