using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
// using NLog;
// using Serilog;
// using Serilog.Events;

namespace ExchangeLemonCore
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        // .ConfigureLogging((hostingContext, logging) =>
        // {
        //     // logging.AddConsole();
        //     // logging.AddDebug();
        // })
        // .UseApplicationInsights()
        .UseKestrel()
        // .UseUrls(urls: "http://192.168.1.11:5000")
        //.UseIISIntegration() // N
        .UseStartup<Startup>();
    }

}



// public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//     WebHost.CreateDefaultBuilder(args)
//.UseApplicationInsights()
//         .UseStartup<Startup>();

//     public static int Main(string[] args)
//     {

//         // BuildWebHost (args).Run ();
//         // return 0;

//         Log.Logger = new LoggerConfiguration()
//             .MinimumLevel.Debug()
//             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//             .Enrich.FromLogContext()
//             .WriteTo.Debug()
//             // .WriteTo.File("Logs/myapp-{Date}.txt")

//             // .WriteTo.Console ()
//             // .WriteTo.ColoredConsole ()
//             .CreateLogger();


//         try
//         {
//             Log.Information("Starting web host");
//             BuildWebHost(args).Run();
//             return 0;
//         }
//         catch (Exception ex)
//         {
//             Log.Fatal(ex, "Host terminated unexpectedly");
//             return 1;
//         }
//         finally
//         {
//             Log.CloseAndFlush();
//         }

//     }

//     public static IWebHost BuildWebHost(string[] args) =>
//         WebHost.CreateDefaultBuilder(args)
//         .UseStartup<Startup>()
//         .UseSerilog()
//         // .ConfigureLogging ((hostingContext, logging) => {
//         //     logging.AddConfiguration (hostingContext.Configuration.GetSection ("Logging"));
//         //     logging.AddConsole ();
//         //     logging.AddDebug ();
//         // })
//         .Build();

