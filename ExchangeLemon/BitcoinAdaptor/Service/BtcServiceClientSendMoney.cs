using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class BtcServiceClientSendMoney : IBtcServiceSendMoney
    {
        public IConfiguration Configuration { get; }

        public BtcServiceClientSendMoney(IConfiguration configuration)
        {

            this.Configuration = configuration;
        }
        ICoinService GenerateCoinService()
        {

            // string walletPassword = "MyWalletPassword";
            // string rpcPassword = "MyRpcPassword";
            // string username = "MyRpcUsername";
            // string url = "http://192.168.1.8:18332";

            //   "walletPassword": "MyWalletPassword",
            //   "rpcPassword": "MyRpcPassword",
            //   "username": "MyRpcUsername",
            //   "url": "http://192.168.1.8:18332",

            var walletPassword = this.Configuration.GetValue<string>("walletPassword");
            var rpcPassword = Configuration.GetValue<string>("rpcPassword");
            var username = Configuration.GetValue<string>("username");
            var url = Configuration.GetValue<string>("url");

            //ICoinService CoinService = new BitcoinService(useTestnet: true);
            ICoinService CoinService = new BitcoinService(url, username, rpcPassword, walletPassword, 60);
            return CoinService;
        }

        public async Task<string> SendMoney(string address, long amountInSatoshi)
        {
            await Task.Delay(0);
            ICoinService CoinService = GenerateCoinService();
            var input = (decimal)amountInSatoshi / (decimal)100000000;
            string result = CoinService.SendToAddress(address, input, "", "", subtractFeeFromAmount: false);
            return result;

        }

    }
}



//    public async Task<string> SendMoney(string sourcePrivateKey,
// string targetAddress,
// long amountSatoshi,
// long amountFeeSatoshi)
//    {
//        var input = (decimal)amountSatoshi / (decimal)100000000;
//        var result = await BitcoinLib.Services.CoinService.SendToAddress(targetAddress, input, "", "", subtractFeeFromAmount: false);

//        return resullt;
//    }