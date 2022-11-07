//using System.Threading.Tasks;
//using BlueLight.Main;
//using BotWalletWatcher;
//using BotWalletWatcherLibrary;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Newtonsoft.Json;

//namespace ExchangeLemonCore.Controllers
//{
//    public class CaptureController : Controller
//    {
//        public CaptureController(WebHookRoutingService webHookRoutingService,
//            IHubContext<PulseHub> pulseHubContext,
//            PulseInsertService PulseInsertService,
//            BlockChainDownloadService BlockChainDownloadService,
//            WatcherService watcherService,
//            BlockContext blockContext)
//        {
//            WebHookRoutingService = webHookRoutingService;
//            this.pulseHubContext = pulseHubContext;
//            this.PulseInsertService = PulseInsertService;
//            this.BlockChainService = BlockChainDownloadService;
//            WatcherService = watcherService;
//            BlockContext = blockContext;
//        }

//        public WebHookRoutingService WebHookRoutingService { get; }
//        public IHubContext<PulseHub> pulseHubContext { get; }
//        public PulseInsertService PulseInsertService { get; }
//        public BlockChainDownloadService BlockChainService { get; }
//        public WatcherService WatcherService { get; }
//        public BlockContext BlockContext { get; }

//        public async Task Signal(string id)
//        {
//            var moduleName = "service_bus";

//            IHubClients Clients = pulseHubContext.Clients;
//            await Clients.All.SendAsync("listenPulse", moduleName);

//            var s = PulseInsertService;
//            var output = await s.InsertOrUpdate("service_bus");


//            var output2 = JsonConvert.SerializeObject(output);
//            await Clients.All.SendAsync("listenPulseDetail", output2);
//        }


//        public async Task Send(string id)
//        {
//            var content = id;
//            var item = JsonConvert.DeserializeObject<SendMessage>(content);

//            var blockChainId = item.blockChainId;
//            var transactionId = item.transactionId;
//            var confirmations = item.confirmations;
//            await this.BlockChainService.SaveLogSend(blockChainId, transactionId, confirmations);
//            await WatcherService.CompareSendOneItem(transactionId, confirmations);
//        }



//        public async Task Receive(string id)
//        {
//            var content = id;
//            var item = JsonConvert.DeserializeObject<ReceiveMessage>(content);

//            var blockChainId = item.blockChainId;
//            var address = item.address;

//            await this.BlockChainService.SaveLogReceive(blockChainId, address);
//            await WatcherService.CompareReceiveOneItem(address);
//        }


//        // http://localhost:5000/capture/test/123
//        public async Task Test(string id)
//        {
//            var content = id;
//            var moduleName = "test-WatcherBlockChain";

//            this.BlockContext.WatcherBlockChains.Add(new WatcherBlockChain()
//            {
//                BlockChainNumber = -1,
//            });
//            await this.BlockContext.SaveChangesAsync();
//            // IHubClients Clients = pulseHubContext.Clients;
//            // await Clients.All.SendAsync("listenPulse", moduleName);

//            Pulse output = await PulseInsertService.InsertOrUpdate(moduleName);

//            IHubClients Clients = pulseHubContext.Clients;
//            await Clients.All.SendAsync("listenPulse", moduleName);

//            var output2 = JsonConvert.SerializeObject(output);
//            await Clients.All.SendAsync("listenPulseDetail", output2);


//        }


//    }
//}
