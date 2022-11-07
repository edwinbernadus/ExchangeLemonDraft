using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    public class FunctionBitcoinServiceReal : IFunctionBitcoinService
    {

        public async Task<decimal> InquiryBalance(string address)
        {
            var detailUrl = ParamRepo.FunctionUrlInquiryBitcoin + $"&address={address}";
            var httpClient = new HttpClient();
            var r = await httpClient.GetStringAsync(detailUrl);

            var w = decimal.Parse(r);

            return w;
        }

        public async Task<BitcoinGenerateAddress> GenerateAddress()
        {
            var detailUrl = ParamRepo.FunctionUrlGenerateAddress;
            var httpClient = new HttpClient();
            var r = await httpClient.GetStringAsync(detailUrl);

            var w = JsonConvert.DeserializeObject<BitcoinGenerateAddress>(r);

            return w;
        }



        public async Task Register(string i)
        {
            await Task.Delay(0);
        }

      
    }
}