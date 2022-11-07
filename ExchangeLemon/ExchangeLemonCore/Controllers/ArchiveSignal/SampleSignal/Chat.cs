using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BlueLight.Main
{

//     public class ValuesHub : Hub<IValuesClient>
// {
//     /// <summary>
//     /// Notify all users that a value has been added.
//     /// </summary>
//     /// <param name="value">The new value</param>
//     public async Task Add(string value) => await Clients.All.PostValue(value);

//     /// <summary>
//     /// Notify all users that a value has been removed.
//     /// </summary>
//     /// <param name="value">The removed value</param>
//     public async Task Delete(string value) => await Clients.All.DeleteValue(value);

// }

 public class Chat : Hub
    {      
        //public async Task Send(string message)
        //{
        //    await Clients.All.SendAsync("Send", message);
        //}


        public void BroadcastMessage(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public void Echo(string name, string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync("echo", name, message + " (echo from server)");
        }
    }
}