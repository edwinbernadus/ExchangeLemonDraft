using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PulseLogic;

namespace BotWalletWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start bot watcher-receive-ver12");

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Logic();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Console.ReadLine();
        }

        private static async Task Logic()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Path.Combine(AppContext.BaseDirectory));

#if DEBUG
            Console.WriteLine("Dev");
            builder.AddJsonFile("appsettings.Development.json", optional: true);
#else
            Console.WriteLine("Release");
            builder.AddJsonFile($"appsettings.json", optional: false);
#endif


            IConfigurationRoot configuration = builder.Build();
            var serviceCollection = DependencyWatcherHelper.Create();

            serviceCollection.AddSingleton<PulseService>();
            ServiceProvider provider = serviceCollection.BuildServiceProvider();


            var hostName = configuration.GetValue<string>("SignalHostName");
            Console.WriteLine(hostName);
            Console.WriteLine("result folder is inside output");



            var logService = provider.GetService<PulseService>();
            await logService.InitAsync(hostName);
            logService.ExecuteLooping("bot-wallet-watcher-status-receive");

            var fileService = provider.GetService<FileSaveService>();
            var currentPath = fileService.GetCurrentPath();
            Console.WriteLine($"current path: {currentPath}");
            var blockChainDownloadServiceExt = provider.GetService<BlockChainDownloadServiceExt>();
            await blockChainDownloadServiceExt.ExecuteDownload();
        }


    }
}
