using System;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace ExchangeSerilog
{
    public class SerilogHelper
    {

        // string z1 = "LPKty2CK9O/V6bg+4x4ZOhqpFEETsBiq9dqc/ypctku1+XK2BLCZttMEmTcAbCpHVWM4HbbsOhHaBr5dFs6W/A==";
        // string webHookUri = "https://outlook.office.com/webhook/41e25f2f-0ed1-47e3-b70b-4f81785ab02a@e9689a0c-43c7-48cc-8bc3-fb3d79f6e2b8/IncomingWebhook/8413cbf0f0c347d2b2f46ea2b1b4e39f/ce0890d7-cf9c-42bb-b244-75a0ce68e3f8";
        // string title = "title 1";


        public void SendMessage(string moduleName, string contentInput)
        {




            Logger log = null;
            if (log == null)
            {
                log = new LoggerConfiguration()
                // .WriteTo.Sentry("https://75068368569c424783ed423cb3b1e99d@sentry.io/210698")
                // .WriteTo.Console()
                // .WriteTo.MicrosoftTeams(webHookUri, title: title)
                // .WriteTo.AzureAnalytics("ecffecac-a7d3-446a-8afe-74e96ba4d7ae", z1)
                // .WriteTo.ApplicationInsightsEvents("e902f1d8-7d55-459e-8fd6-1e2ba2401a82")
                .Enrich.FromLogContext()
                .CreateLogger();
            }

            var content = $"[{moduleName} {contentInput}]";
            log.Error(content);
            //System.Diagnostics.Trace.TraceError(content);
            //Log.CloseAndFlush();
        }


    }
}