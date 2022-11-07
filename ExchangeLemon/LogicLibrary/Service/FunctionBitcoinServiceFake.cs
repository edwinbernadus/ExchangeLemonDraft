using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class FunctionBitcoinServiceFake : IFunctionBitcoinService
    {

        public async Task<decimal> InquiryBalance(string address)
        {
            var detailUrl = "http://localhost:5050/btc/inquiry/abc";
            var httpClient = new HttpClient();
            var r = await httpClient.GetStringAsync(detailUrl);

            var w = decimal.Parse(r);

            return w;
        }

        public async Task<BitcoinGenerateAddress> GenerateAddress()
        {
            await Task.Delay(0);
            //var detailUrl = ParamRepo.FunctionUrlGenerateAddress;
            //var httpClient = new HttpClient();
            //var r = await httpClient.GetStringAsync(detailUrl);
            //var w = JsonConvert.DeserializeObject<BitcoinGenerateAddress>(r);
            //return w;


            var output = new BitcoinGenerateAddress()
            {
                PrivateKey = Guid.NewGuid().ToString(),
                PublicAddress = Guid.NewGuid().ToString()
            };
            return output;
        }



        public async Task Register(string i)
        {
            await Task.Delay(0);
        }

        //public async Task<InquiryDiffResult> InquiryDiff(string walletAddress, int lastPosition)
        //{
        //    await Task.Delay(0);
        //    return new InquiryDiffResult();
        //}
    }
}