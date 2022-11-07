using System;
using System.Threading.Tasks;
using BlueLight.Main;
using ExchangeLemonCore;
//using DebugWorkplace;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDev
{
    public class LogicArchive
    {
        public IServiceProvider serviceProvider { get; }

        public LogicArchive(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Logic4DiffBalance()
        {
            await Task.Delay(0);
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            string walletAddress = "mkV3963kayBNVH5ebLrTgc5qBx8vaiqz1n";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            var service = serviceProvider.GetService<BtcBusinessLogicNew>();
            //var r = await service.InquiryDiff(walletAddress, new List<string>());

        }

        public async Task Logic5InqBalance()
        {


            string walletAddress = "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ";
            walletAddress = "n24wZ2naAjHmhmkeHnyd2Y6BnfoMzR4tEt";
            walletAddress = "2NGRh3vtZaBGmxbyrLiM8nHMNJPzW3b2oXW";
            walletAddress = "mgfxuMwHZUo1vwJuSzDBAicAUM2mdWugzb";
            //walletAddress = "mrM1ZEkrUnVpnGuPyef8ufFB5v1uU5XCFL";
            var service = serviceProvider.GetService<IBitcoinGetBalance>();
            var r = await service.Execute(walletAddress);
            //var r2 = await service.GetTotalConfirmBalance(walletAddress);

            //r2  10000000    decimal


            //var p = BtcCloudService.Generate();
            //var z = await p.GetBalanceForAddress(walletAddress);
        }

        public async Task Logic7RemittanceOutputBtcSend()
        {
            await Task.Delay(0);
            //var s = new UnitTestOutgoingBtc();
            //var s2 = await s.TestConfirmTransfer();
            //Console.WriteLine(s2);
        }

        //public async Task<int> Logic8CheckStatusTransfer(string id)
        //{
        //    //var s = new BtcCloudService();
        //    ////var id = "b960f6939fa3507cbdd4c6d5cbe54717055a327bf6841f1ff8318b35759fc1c2";
        //    ////id = "7fb23d1f58bff51bd44fe00d34c4fa8254b08c55e8a6cd25bba016fa8919a56a";
        //    ////id = "d20c31e24019e67951f4384b3e68297edf01078f777852564062cefb685f8b99";
        //    ////id = "307dfb45819a559163050f81dac3fb48e8de3fe847e5104473a744018fc4e4bd";
        //    //var s2 = await s.InquiryTransaction(id);

        //    var p = BtcCloudService.Generate();
        //    var s2 = await p.InquiryTransaction(id);
        //    var confirmation = s2.confirmations;
        //    Console.WriteLine(confirmation);
        //    return confirmation;
        //}
        public async Task<(string transactionId, string toPublic)> Logic6Transfer()
        {

            await Task.Delay(0);

            //string fromPrivate = "cPFY3pPiUsAyXHNsqqjyGqgxofqQAzna4kvJHvVBdLCK77Rfn4rV";
            //var toPublic = "mrM1ZEkrUnVpnGuPyef8ufFB5v1uU5XCFL";

            //string fromPrivate = "cNdvzEcMVzoqKCBTEHHJhHSmuSyoEQjFc1KvcNqjowvezw8dPuHP"; // mmpvS8nchp7LS3ABmRUt8MHjZA8tuaqAtx
            //string fromPrivate = "cPFY3pPiUsAyXHNsqqjyGqgxofqQAzna4kvJHvVBdLCK77Rfn4rV"; // mgfxuMwHZUo1vwJuSzDBAicAUM2mdWugzb
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            string fromPrivate = "cNdvzEcMVzoqKCBTEHHJhHSmuSyoEQjFc1KvcNqjowvezw8dPuHP"; // mmpvS8nchp7LS3ABmRUt8MHjZA8tuaqAtx
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            //still empty
            //waiting for faucet donation
            //Item1   "cPtdogHSqtZi3J9SX18dVpkG8rsSBbgombo5dgF3znggZx8fdFjd"  string
            //Item2   "myxmaLA84jfDoLH5xAJuJGCEd8d6BmfUJ7"    string


            //var toPublic = "mgfxuMwHZUo1vwJuSzDBAicAUM2mdWugzb";
            var toPublic = "msv6zoMW59BbEWHeb6k9kMrd852WU1XpqZ";
            toPublic = "2NEEz3RGN9RGCKHGMtwmdRfZGRzey3eiofw";


            //string fromPrivate = "cN5WnvMgbjGFEVkUeuzr89anxcihQFR5WuyXhZAYnfb5KxqfvsNz";
            //var toPublic = "mrM1ZEkrUnVpnGuPyef8ufFB5v1uU5XCFL";

            //string fromPrivate = "cPFY3pPiUsAyXHNsqqjyGqgxofqQAzna4kvJHvVBdLCK77Rfn4rV";
            //var toPublic = "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ";


            var service = serviceProvider.GetService<IBtcServiceSendMoney>();

            //var s2 = service.GeneratePubKeyFromPrivateKey(fromPrivate);


            //var transactionId = await service.SendMoney(fromPrivate, toPublic, 1000, 501);
            var transactionId = await service.SendMoney(toPublic, 1000);
            //var transactionId = await service.SendMoney(fromPrivate, toPublic, 1000, 1001);

            //var result = await service.SendMoneyDraft(fromPrivate, toPublic, 1000, 501);
            //var b = BtcCloudService.Generate();
            //string rawContent = result.transactionRaw;
            //await b.PushRawTransaction(result.transactionId, rawContent);
            //var transactionId = result.transactionId;


            //var r = await service.Execute(walletAddress);
            //var r2 = await service.GetTotalConfirmBalance(walletAddress);

            //r2  10000000    decimal


            (string transactionId, string toPublic) output = (transactionId, toPublic);
            return output;

        }

        internal async Task Logic12Transfer()
        {
            string fromPrivate = "cNdvzEcMVzoqKCBTEHHJhHSmuSyoEQjFc1KvcNqjowvezw8dPuHP";

            var service = serviceProvider.GetService<BtcService>();
            var serviceGetBalance = serviceProvider.GetService<BitcoinGetBalance>();
            var serviceSendMoney = serviceProvider.GetService<IBtcServiceSendMoney>();
            var fromPublic = service.GeneratePubKeyFromPrivateKey(fromPrivate);
            //"mmpvS8nchp7LS3ABmRUt8MHjZA8tuaqAtx"

            var toPublic = "mgtnwqjCsPeP2rFai5xmaDbgAeZ6fNS3mM";

            var balance = serviceGetBalance.Execute(fromPublic);

            var transactionId = await serviceSendMoney.SendMoney(toPublic, 1000);

        }

        internal async Task Logic9DeleteAllHooks()
        {
            await Task.Delay(0);
            //var c = BtcCloudService.Generate();
            //var lists = await c.GetListHook();
            //foreach (var item in lists)
            //{
            //    await c.DeleteHook(item.Id);
            //}

            ////throw new NotImplementedException();
        }

        //public async Task<string> Logic8TransferAndMonitoringScenarioOne()
        //{
        //    var item = await Logic6Transfer();
        //    var transactionId = item.transactionId;

        //    var l = new LogicSubmitRegister(serviceProvider);

        //    await l.RegisterNotifyTransactionHookTestTwo(transactionId);
        //    //await l.RegisterNotifyTransactionHookTestThree(s);
        //    //await l.RegisterNotifyTransactionHookTestThree(item.toPublic);
        //    var m = await l.GetList();
        //    var s3 = await Logic8CheckStatusTransfer(transactionId);
        //    return transactionId;
        //}


        //public async Task<string> Logic10TransferAndMonitoringScenarioTwo()
        //{
        //    var item = await Logic6Transfer();
        //    var transactionId = item.transactionId;

        //    var l = new LogicSubmitRegister(serviceProvider);

        //    //await l.RegisterNotifyTransactionHookTestTwo(s);
        //    await l.RegisterNotifyTransactionHookTestThree(transactionId);
        //    //await l.RegisterNotifyTransactionHookTestThree(item.toPublic);
        //    var m = await l.GetList();
        //    var s3 = await Logic8CheckStatusTransfer(transactionId);
        //    return transactionId;
        //}

        //        public async Task<string> Logic11TransferAndMonitoringScenarioThree()
        //        {
        //            await Task.Delay(0);
        //            var logicSubmitRegister = new LogicSubmitRegister(serviceProvider);
        //#pragma warning disable CS0219 // Variable is assigned but its value is never used
        //            var new1 = "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ";
        //#pragma warning restore CS0219 // Variable is assigned but its value is never used
        //                              //await logicSubmitRegister.RegisterNotifyTransactionHookTestThree(new1);

        //            new1 = "1";



        //            var item = await Logic6Transfer();
        //            var transactionId = item.transactionId;



        //            //await l.RegisterNotifyTransactionHookTestTwo(s);
        //            //await l.RegisterNotifyTransactionHookTestThree(s);


        //            var k1 = "cN5WnvMgbjGFEVkUeuzr89anxcihQFR5WuyXhZAYnfb5KxqfvsNz";
        //            //var fromAddress = NBitcoin.Bitcoin.Instance.Testnet.CreateBitcoinAddress(k1);

        //            var btcService = new BtcService();
        //            var fromPublic = btcService.GeneratePubKeyFromPrivateKey(k1);
        //            var toPublic = item.toPublic;

        //            //var new1 = "mpkb1K1VKAk1aL3VD69hD2biFjsUfPvutZ";
        //            //await logicSubmitRegister.RegisterNotifyTransactionHookTestThree(toPublic);
        //            //var m = await logicSubmitRegister.GetList();
        //            var s3 = await Logic8CheckStatusTransfer(transactionId);
        //            return transactionId;
        //        }


        public async Task Logic3()
        {
            var replayPlayerService = serviceProvider.GetService<IReplayPlayerService>();
            var inquirySpotMarketService = serviceProvider.GetService<InquirySpotMarketService>();
            //var inquirySpotMarketService2 = serviceProvider.GetService<DbContextOptions>();
            //var inquirySpotMarketService3 = serviceProvider.GetService<InquirySpotMarketServiceTwo>();
            //var inquirySpotMarketService4 = serviceProvider.GetService<InquirySpotMarketServiceThree>();

            string currency_pair = "btc_usd";
            var result1 = await inquirySpotMarketService.GetLastChangeAsync(currency_pair);
            var result2 = await inquirySpotMarketService.GetVolumeAsync(currency_pair);
        }


        public async Task Logic2()
        {
            var replayPlayerService = serviceProvider.GetService<IReplayPlayerService>();
            await replayPlayerService.ExecuteFromTable();
        }

        public async Task Logic1()
        {
            await Task.Delay(0);
            var m1 = OrderItemMainHelper.GenerateItems();
            var m = OrderItemMainHelper.GenerateItemsPositive();
            var m2 = OrderItemMainHelper.GenerateItemsNegative();
            //return;
        }

        public async Task Logic12CheckManualDeposit()
        {
            var s = this.serviceProvider.GetService<BitcoinSyncJob>();
            var userName = "guest2@server.com";
            await s.ExecuteAsync(userName);

        }

    }
}
