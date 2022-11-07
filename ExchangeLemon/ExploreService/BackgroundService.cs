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

    public class BackgroundService : IHostedService, IDisposable
    {
        public void Dispose()
        {

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console3.WriteLine($"[item-step] - 2");
                await Core();
            }
            catch (Exception ex)
            {
                Console3.WriteLine($"[item-step] - error2");
                var m = ex.Message;
                Console3.WriteLine($"error - {m}");
            }
        }


        private async Task Core()
        {
            await Task.Delay(0);
            Console3.WriteLine($"[item-step] - 4");
            var builder = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(AppContext.BaseDirectory));

#if DEBUG
            Console.WriteLine("Dev");
            builder.AddJsonFile("appsettings.Development.json", optional: true);
#else
            Console.WriteLine("Release");
            builder.AddJsonFile($"appsettings.json", optional: false);
#endif

            Console3.WriteLine($"[item-step] - 5");
            IConfigurationRoot configuration = builder.Build();
            var serviceCollection = DependencyWatcherHelper.Create();
            serviceCollection.AddSingleton<IConfiguration>(configuration);


            Console3.WriteLine($"[item-step] - 6");
            serviceCollection.AddSingleton<PulseService>();
            ServiceProvider provider = serviceCollection.BuildServiceProvider();

            Console3.WriteLine($"[item-step] - 7");
            // var blockChainUploadServiceExtService =
            //     provider.GetService<BlockChainUploadServiceExt>();

            var hostName = configuration.GetValue<string>("SignalHostName");
            Console.WriteLine(hostName);


            //var folderSourceString = configuration.GetValue<string>("FolderSource");
            //BlockChainUploadServiceExt.PathConfig = folderSourceString;
            //BlockChainUploadServiceExt.HostNameConfig = hostName;

            Console3.WriteLine($"[item-step] - 8");
            // var logService = provider.GetService<PulseService>();
            Console3.WriteLine($"[item-step] - 9");
            // await logService.InitAsync(hostName);
            Console3.WriteLine($"[item-step] - 10");
            // logService.ExecuteLooping("bot-wallet-watcher-status-send");
            Console3.WriteLine($"[item-step] - 11");



            // WatchLogic.Execute(blockChainUploadServiceExtService);
            // await blockChainUploadServiceExtService.ExecuteUpload();

        }



        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            Console3.ResetLog();

        }
    }
}
