using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class LogHubService : ILogHubService
    {
        public LogHubService(IHubContext<LogHub> logHubContext){
            LogHubContext = logHubContext;
        }

        public IHubContext<LogHub> LogHubContext  { get; }

        public async Task SendMessage(string content)
        {
            await LogHubContext .Clients.All.SendAsync("chat", content);
        }
    }
}
