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

        private HubConnection _hubConnection;


        public async Task Execute()
        {
            string hostname = HostNameHelper.GetHostName();
            var url = $"{hostname}/signal/pulse";
            var watcherFactory = new SignalFactory();
            watcherFactory.Generate(url);

            Console.WriteLine($"hostname: {hostname}");
            _hubConnection = watcherFactory._hubConnection;
            //this._hubConnection = new HubConnectionBuilder()
            //      .WithUrl()
            //     .AddMessagePackProtocol()
            //     .Build();


            
            _hubConnection.On<string>("listenPulse",  (moduleName) =>
             {
                 Console.WriteLine($"Received Message: {moduleName}-{DateTime.Now.ToLongTimeString()}");
             });
            //await _hubConnection.StartAsync();
            await watcherFactory.Connect();


        }
    }
}

