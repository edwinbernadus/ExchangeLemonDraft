//using System.Net.Http;
//using System.Threading.Tasks;

//namespace BlueLight.Main
//{
//    public class FunctionProxyBitcoin
//    {
//        public FunctionProxyBitcoin()
//        {
//        }

//        public async Task<decimal> InquiryBalance(string address)
//        {
//            // http://localhost:7071/api/GenerateAddress
//            var detailUrl = ParamRepo.FunctionUrlInquiryBitcoin + $"&address={address}";
//            var httpClient = new HttpClient();
//            var r = await httpClient.GetStringAsync(detailUrl);

//            var w = decimal.Parse(r);

//            return w;
//            //throw new NotImplementedException();
//        }

      

//        public async Task Register(string i)
//        {
//            //throw new NotImplementedException();
//        }
//    }
//}


////public async Task<BitcoinGenerateAddress> GenerateAddress()
////{
////    // http://localhost:7071/api/GenerateAddress
////    var detailUrl = ParamRepo.FunctionUrlGenerateAddress;
////    var httpClient = new HttpClient();
////    var r = await httpClient.GetStringAsync(detailUrl);

////    var w = JsonConvert.DeserializeObject<BitcoinGenerateAddress>(r);

////    return w;
////    //throw new NotImplementedException();
////}

////public async Task<double> SyncJob(string userName)
////{
////    //http://localhost:7071/api/Sync?username=guest1@server.com
////    // http://localhost:7071/api/GenerateAddress
////    var detailUrl = ParamRepo.FunctionSyncBtcUrl + $"&username={userName}";
////    var httpClient = new HttpClient();
////    var r = await httpClient.GetStringAsync(detailUrl);
////    var w = double.Parse(r);
////    return w;
////}