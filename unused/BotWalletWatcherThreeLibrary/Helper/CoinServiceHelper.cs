using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Responses;
using BitcoinLib.Services;
using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Bitcoin;

namespace BotWalletWatcher
{
    public class CoinServiceHelper
    {
        public static ICoinService GenerateCoinService()
        {
            string walletPassword = "MyWalletPassword";
            string rpcPassword = "MyRpcPassword";
            string username = "MyRpcUsername";
            string url = "http://192.168.1.8:18332";
            //ICoinService CoinService = new BitcoinService(useTestnet: true);
            ICoinService CoinService = new BitcoinService(url, username, rpcPassword, walletPassword, 60);
            return CoinService;
        }
    }
}
