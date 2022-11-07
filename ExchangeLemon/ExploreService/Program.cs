using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LogLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseLogic;
//using PulseLogic;

namespace BotWalletWatcher
{
    class Program
    {

        private static async Task Main(string[] args)
        {

            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //DependencyWatcherHelper.Populate(services);
                    services.AddHostedService<BackgroundService>();
                });

            Console3.EnableSaveFile("send-sync");
            Console3.WriteLine("start bot watcher send-ver18");
            if (isService)
            {
                Console3.WriteLine($"[item-step] - 1");
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }



    }
}


//static string path = "C:\\Users\\edwin\\Resilio Sync\\output";

//         static void Main(string[] args)
//         {
//             Console.WriteLine("start bot watcher send-ver14");

// #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//             Logic();
// #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
//             Console.ReadLine();
//         }
