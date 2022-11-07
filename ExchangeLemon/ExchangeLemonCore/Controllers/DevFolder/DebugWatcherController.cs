using System;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ExchangeLemonCore.Controllers
{
    public class DebugWatcherController : Controller
    {
        public DebugWatcherController(WatcherHub watcherHub, ILogHubService logHubService)
        {
            WatcherHub = watcherHub;
            LogHubService = logHubService;
        }
        public WatcherHub WatcherHub { get; }
        public ILogHubService LogHubService { get; }


        //http://localhost:53252/debugWatcher
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<String> Index()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //await this.WatcherHub.Send(1, "abc", 1);
            return "woot";
        }

        //http://localhost:53252/debugWatcher/testsend/woot
        public async Task<bool> TestSend(string id)
        {
            var content = id;
            await this.LogHubService.SendMessage(content);
            return true;
        }
    }
}