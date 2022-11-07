//using System;
//using System.Threading.Tasks;
////using DebugWorkplace;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using BlueLight.Main;
//using System.Collections.Generic;
//using BlockCypher.Objects;
//using System.Linq;

//namespace ConsoleDev
//{
//    public class LogicSubmitRegister
//    {
//        public IServiceProvider serviceProvider { get; }

//        public LogicSubmitRegister(IServiceProvider serviceProvider)
//        {
//            this.serviceProvider = serviceProvider;
//        }

//        public async Task ExecuteRegisterNotificationSendConfirm()
//        {



//            var s = BtcCloudService.Generate();
//            var p2 = await s.GetListHook();

//            var address = new string[]
//            {
//                //"msv6zoMW59BbEWHeb6k9kMrd852WU1XpqZ"
//                "2MuzH4LvTZqXqJh5ynEn3gjzcMVc8RfSjFT"
//            };
//            //var address = new string[] {
//            //    "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ",
//            //    "mrM1ZEkrUnVpnGuPyef8ufFB5v1uU5XCFL",
//            //    "miMbjYNYb1bxNtDzBTjN7EeG4YnxT2hT16"};

//            var item = address[0];
            
//            //var url = BtcCloudService.url;
//            var p = new BtcCloudService();
//            await p.RegisterNotifyTransfer(address.ToList().First());

//        }


//        public async Task ExecuteRegister()
//        {
//            var s = BtcCloudService.Generate();
//            var p2 = await s.GetListHook();

//            var address = new string[]
//            {
//                "msv6zoMW59BbEWHeb6k9kMrd852WU1XpqZ"
//            };
//            //var address = new string[] {
//            //    "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ",
//            //    "mrM1ZEkrUnVpnGuPyef8ufFB5v1uU5XCFL",
//            //    "miMbjYNYb1bxNtDzBTjN7EeG4YnxT2hT16"};

//            var item = address[0];
//            var p = new BtcCloudService();
//            await p.Register(item);

//        }

//        internal async Task ClearAllHook()
//        {
//            var p = BtcCloudService.Generate();
//            var items = await p.GetListHook();
//            foreach (var item in items)
//            {
//                await p.DeleteHook(item.Id);
//            }
            

//        }

//        public async Task<List<HookInfo>> GetList()
//        {
//            //var p = new BtcCloudService();
//            //List<HookInfo> p2 = await p.List();
//            var p = BtcCloudService.Generate();
//            var p2 = await p.GetListHook();
//            return p2;
//        }
//        public async Task RegisterReceive()
//        {
//            var p = new BtcCloudService();
            
//            var s = BtcCloudService.Generate();
//            var p2 = await s.GetListHook();

//            var address = new string[] {
//                "msv6zoMW59BbEWHeb6k9kMrd852WU1XpqZ",
//                "mfcfM5uGmpw8nFFbnQYWZaKM1b1VhWW6cw"};


            
//            //var url = BtcCloudService.url;
//            foreach (var item in address)
//            {
//                await p.Register(item);
//            }
//        }

       
//    }
//}


////internal async Task RegisterNotifyTransactionHookTest()
////{

////    var p = new BtcCloudService();
////    var transactionId = "d20c31e24019e67951f4384b3e68297edf01078f777852564062cefb685f8b99";
////    await p.RegisterNotifyTransfer(transactionId);
////}

////internal async Task RegisterNotifyTransactionHookTestTwo(string transactionId)
////{

////    var p = new BtcCloudService();
////    //var transactionId = "d20c31e24019e67951f4384b3e68297edf01078f777852564062cefb685f8b99";
////    //transactionId = "307dfb45819a559163050f81dac3fb48e8de3fe847e5104473a744018fc4e4bd";
////    await p.RegisterNotifyTransferWithConfirmations(transactionId, 1);

////}

////internal async Task RegisterNotifyTransactionHookTestThree(string transactionId)
////{

////    var p = new BtcCloudService();
////    //var transactionId = "d20c31e24019e67951f4384b3e68297edf01078f777852564062cefb685f8b99";
////    //transactionId = "307dfb45819a559163050f81dac3fb48e8de3fe847e5104473a744018fc4e4bd";

////    await p.RegisterAll(transactionId);
////}