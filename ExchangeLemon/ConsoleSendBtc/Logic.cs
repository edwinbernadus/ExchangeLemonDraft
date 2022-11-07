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

            var p = new PulseService();
            await p.InitAsync(hostname);
            p.ExecuteLooping("console-send-btc");
            Console.WriteLine($"hostname: {hostname}");


            await InitWatcherFactory(hostname,p);
            

            

        }

        private async Task InitWatcherFactory(string hostname, PulseService p)
        {
            var watcherFactory = new SignalFactory();

            var url = $"{hostname}/signal/watcher";
            watcherFactory.Generate(url);

            var _hubConnection = watcherFactory._hubConnection;


            var service = DependencySendBtcHelper.GenerateService();
            var s = service.GetService<IBtcServiceSendMoney>();
            _hubConnection.On<string, long>("sendTransfer", async (address, amountInSatoshi) =>
            {
                Console.WriteLine($"Received Message From Send Transfer: {address}-{amountInSatoshi}");
                try
                {
                    var transactionId = await s.SendMoney(address, amountInSatoshi);

                    await _hubConnection.SendAsync("inquiryTransfer", transactionId);
                    await p.Send("console-send-btc-inquiry-transfer");
                }
                catch (Exception)
                {
                    await _hubConnection.SendAsync("inquiryTransfer", "error");
                    await p.Send("console-send-btc-inquiry-transfer-failed");
                }

            });
            await watcherFactory.Connect();
        }
    }
}
