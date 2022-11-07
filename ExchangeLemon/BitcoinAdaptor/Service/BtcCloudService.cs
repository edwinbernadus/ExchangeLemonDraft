//using BlockCypher;
//using System.Threading.Tasks;

//namespace BlueLight.Main
//{
//    public class BtcCloudService : IBtcCloudServiceRegisterNotification
//    {
//        public static Blockcypher Generate()
//        {
//            var blockcypher = new Blockcypher(token, Endpoint.BtcTest3);
//            return blockcypher;
//        }

//        static string token = "1e431b27175d402bb28f621c48e97e4f";

//        Blockcypher blockcypher;
//        public BtcCloudService()
//        {
//            blockcypher = Generate();
//        }



//        //https://webhook.site/b5191c1a-9338-46c7-b893-6bdf3b24764a
//        //http://postb.in/Cjhj2BAj
//        //http://requestbin.fullcontact.com/q6d636q6

//        //admin
//        //https://webhook.site/#/b5191c1a-9338-46c7-b893-6bdf3b24764a/9b92df5b-851e-45ba-94ad-6cdee94428d1/1
//        //http://postb.in/b/Cjhj2BAj
//        //https://requestbin.fullcontact.com/q6d636q6?inspect

//        public string[] UrlDict()
//        {
//            var urls = new string[] {
//                "https://echochain.azurewebsites.net/receive",
//                "https://globalreceiver.azurewebsites.net/receive",
//                "https://orange.free.beeceptor.com",
//                "https://webhook.site/b5191c1a-9338-46c7-b893-6bdf3b24764a",
//                "http://postb.in/Cjhj2BAj",
//                "http://requestbin.fullcontact.com/q6d636q6"
//            };
//            return urls;
//        }

    

     

        
//        public async Task Register(string address)
//        {
//            var urls = UrlDict();
//            foreach (var item in urls)
//            {
//                var s2 = await blockcypher.GenerateHook(address, HookEvent.UnconfirmedTransaction, item);
//            }
            
            
//        }



//        public async Task RegisterNotifyTransfer(string accountId)
//        {
//            var urls = UrlDict();
//            foreach (var item in urls)
//            {
//                var s = await blockcypher.GenerateHook(accountId, HookEvent.TransactionConfirmation, item);
//            }
            
            
//        }

       
//    }
//}


////public async Task RegisterAll(string transactionId)
////{
////    await blockcypher.GenerateHook(transactionId, HookEvent.TransactionConfirmation, url);
////    await blockcypher.GenerateHook(transactionId, HookEvent.ConfirmedTransaction, url);
////    await blockcypher.GenerateHook(transactionId, HookEvent.UnconfirmedTransaction, url);
////    //return s;
////}

////public async Task RegisterNotifyTransferWithConfirmations(string address, int totalConfirmations = 1)
////{
////    var s = await blockcypher.GenerateHookSendTransferConfirmation(address, url, totalConfirmations);
////    //return s;
////}


////public async Task Delete(string id)
////{

////    var s = await blockcypher.DeleteHook(id);
////}

////public async Task InquiryHook(string id)
////{

////    var s = await blockcypher.InquiryHook(id);
////}

////public async Task<List<HookInfo>> List()
////{

////    var s = await blockcypher.GetListHook();
////    return s;

////}


////public async Task<InquiryTransactionResult> InquiryTransaction(string id)
////{

////    var s = await blockcypher.InquiryTransaction(id);
////    return s;

////}

////public async Task Transfer()
////{


////}