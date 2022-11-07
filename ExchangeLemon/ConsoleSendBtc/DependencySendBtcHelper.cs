using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueLight.Main;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ConsoleSignalPlayground
{

    public class DependencySendBtcHelper
    {
        public static ServiceCollection Create()
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

            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IBtcServiceSendMoney, BtcServiceClientSendMoney>();
            //serviceCollection.AddSingleton(configuration);
            
            serviceCollection.AddSingleton<IConfiguration>(s => configuration);

            //serviceCollection.AddTransient<INotificationWatcherService, NotificationWatcherClientServiceExt>();
            //serviceCollection.AddDbContext<BlockContext>(options =>
            //    options.UseSqlServer(MainParam.connString));
            ServiceCollection output = serviceCollection;
            return output;
        }

        public static ServiceProvider GenerateService()
        {
            var serviceCollection = Create();
            ServiceProvider service = serviceCollection.BuildServiceProvider();
            return service;
        }

        public static ServiceProvider GenerateServiceProduction()
        {
            var serviceCollection = Create();

            ServiceProvider service = serviceCollection.BuildServiceProvider();
            return service;
        }

    
    }
}


//public async Task Execute()
//{
//    await SetupSignalRHubAsync();
//    // await SendItem();
//}

// _hubConnection.On<string>("chat", (message) =>
//                    {
//                        Console.WriteLine($"Received Message: {message}");
//                    });

// static string hostname = "https://localhost:5001";
// static string hostname = "http://localhost:5000";
// static string hostname = "https://waterbear.azurewebsites.net";

// static string hostname = "http://192.168.1.25:53252";
// static string hostname = "http://f16e098e.ngrok.io";


// async Task SendItemOld()
// {
//     var item = "woot";
//     await _hubConnection.SendAsync("Send", item);
//     Console.WriteLine("SendAsync to Hub");
// }

// async Task SendItem()
// {
//     // var input = new Dashboard()
//     // {
//     //     TypeEvent = "bot_sync",
//     //     Counter = 1
//     // };
//     // var item = JsonConvert.SerializeObject(input);

//     // await _hubConnection.SendAsync("test");

//     await _hubConnection.SendAsync("SubmitSyncBot");
//     // await _hubConnection.SendAsync("SubmitDashboard", item);
//     // await _hubConnection.SendAsync("ListenDashboard", 123, new System.Threading.CancellationToken());
//     // await _hubConnection.SendAsync("ListenDashboard", "item", "1");
//     // await _hubConnection.SendAsync("listenDashboard", item);
//     // await _hubConnection.SendCoreAsync("ListenDashboard", new object[] { item });
//     Console.WriteLine("SendAsync to Hub");
// }


// public class Dashboard
// {
//     public int Id { get; set; }
//     public string TypeEvent { get; set; }
//     public int Counter { get; set; }
//     //public DateTime LastUpdate { get; set; } = DateTime.Now;
//     public DateTimeOffset LastUpdate { get; set; } = DateTime.Now;
// }
