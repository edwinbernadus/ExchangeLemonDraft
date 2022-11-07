using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PulseLogic;

namespace ConsoleSignalPlayground
{
 
    public class Logic
    {
        
        public async Task Execute()
        {
            var hostname = HostNameHelper.GetHostName();
            var p = new PulseLogic.PulseService();
            await p.InitAsync(hostname);
            p.ExecuteLooping("console-watcher-log-listener");

            Console.WriteLine($"hostname: {hostname}");

            var url = $"{hostname}/signal/log";
            var watcherFactory = new SignalFactory();
            watcherFactory.Generate(url);

            var _hubConnection = watcherFactory._hubConnection;
          
            _hubConnection.On<dynamic>("chat", async (message) =>
            {
                Console.WriteLine($"Received Message: {message}");
                await p.Send("console-watcher-log-listener-received-message");
            });
            

            await watcherFactory.Connect();

        }
    }
}

