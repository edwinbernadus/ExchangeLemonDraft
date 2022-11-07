// using System.Threading.Tasks;
// using Microsoft.AspNetCore.SignalR;

// public class BalanceHub : Hub {
//     public async Task Submit (string userName, string currency, double rate) {
//         // .Group(groupName).
//         // await Clients.All.SendAsync(currency, rate);
//         var groupName = userName;
//         await Clients.Group (groupName).SendAsync (currency, rate);
//     }
// }