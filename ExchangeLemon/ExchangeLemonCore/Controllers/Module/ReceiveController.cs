using BlueLight.Main;
using ExchangeLemonCore.Models.ReceiveModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeLemonCore.Controllers
{



    public class ReceiveController : Controller
    {
        public ReceiveController(WebHookRoutingService webHookRoutingService,
            IHubContext<PulseHub> pulseHubContext,
            PulseInsertService PulseInsertService)
        {
            WebHookRoutingService = webHookRoutingService;
            this.pulseHubContext = pulseHubContext;
            this.PulseInsertService = PulseInsertService;
        }

        public WebHookRoutingService WebHookRoutingService { get; }
        public IHubContext<PulseHub> pulseHubContext { get; }
        public PulseInsertService PulseInsertService { get; }



        public string Hello()
        {
            return "Hi";
        }

        [HttpPost]
        public string HelloPost()
        {
            return "Hi-Post";
        }

        [HttpPost]
        public async Task<string> Index()
        {
            try
            {
                var content = "";
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();

                }


                var headers = this.Request.Headers;
                var event1 = "x-eventtype";
                var event_type = headers[event1];
                var event_type1 = event_type.ToArray().ToList().FirstOrDefault();
                if (string.IsNullOrEmpty(event_type1))
                {
                    return "no_header";
                }
                var output = await this.WebHookRoutingService.Execute(content, event_type1);
                return output;

            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return m;
            }

        }


    }
}

// public async Task Signal(string id)
// {
//     var moduleName = "service_bus";

//     IHubClients Clients = pulseHubContext.Clients;
//     await Clients.All.SendAsync("listenPulse", moduleName);

//     var s = PulseInsertService;
//     var output = await s.InsertOrUpdate("service_bus");


//     var output2 = JsonConvert.SerializeObject(output);
//     await Clients.All.SendAsync("listenPulseDetail", output2);
// }