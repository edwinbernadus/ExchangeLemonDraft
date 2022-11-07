using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class PulseHub : Hub
    {
        public PulseHub(PulseInsertService pulseInsertService)
        {
            
            PulseInsertService = pulseInsertService;
        }

        public PulseInsertService PulseInsertService { get; }

        public async Task RegisterPulse(string moduleName)
        {

            //IHubCallerClients c = Clients;
            Pulse output = await PulseInsertService.InsertOrUpdate(moduleName);

            await Clients.All.SendAsync("listenPulse", moduleName);

            var output2 = JsonConvert.SerializeObject(output);
            await Clients.All.SendAsync("listenPulseDetail", output2);
            //await RegisterPulseLogic(moduleName, c);
        }


        //public async Task RegisterPulseLogic(string moduleName, IHubClients c)
        //{
        //    Pulse output = await PulseInsertService.InsertOrUpdate(moduleName);

        //    await c.All.SendAsync("listenPulse", moduleName);

        //    var output2 = JsonConvert.SerializeObject(output);
        //    await c.All.SendAsync("listenPulseDetail", output2);
        //}




    }
}

//public async Task Send(int blockChainId,List<string> transactionLists, int confirmations)
//{
//    await WatcherService.SaveLogSendAsync(blockChainId, transactionLists, confirmations);
//    await WatcherService.CompareSend(transactionLists, confirmations);
//}

//public async Task Receive(int blockChainId,List<string> addresses)
//{
//    await WatcherService.SaveLogReceiveAsync(blockChainId, addresses);
//    await WatcherService.CompareReceive(addresses);
//}

//public async Task Test(string input1)
//{

//}