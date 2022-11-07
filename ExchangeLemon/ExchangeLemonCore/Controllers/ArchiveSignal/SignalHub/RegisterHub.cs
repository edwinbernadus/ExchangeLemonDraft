// using System.Threading.Tasks;
// using Microsoft.AspNetCore.SignalR;

// public class RegisterHub : Hub {
//     public async Task OnConnected () {
//         string name = Context.User.Identity.Name;
//         // await Groups.AddAsync(Context.ConnectionId, name);

//         await Groups.AddToGroupAsync (Context.ConnectionId, name);
//         await base.OnConnectedAsync ();

//     }

//     public async Task Submit (string userName) {

//         // .Group(groupName).
//         // await Clients.All.SendAsync(currency, rate);
//         var groupName = userName;

//         await Clients.Group (groupName).SendAsync ("registered");
//     }
// }