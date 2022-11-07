using BlueLight.Main;
using Info.Blockchain.API.BlockExplorer;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public class BitcoinGetBalance : IBitcoinGetBalance
    {
        
        public async Task<BtcTransaction> Execute(string walletAddress)
        {
            
                var b = new BlockExplorer();

                var result = await b.GetBase58AddressAsync(walletAddress, filter: FilterType.All);

                var output = new BtcTransaction()
                {
                    Details = result.Transactions.Select(x => x.Hash).ToList(),
                    Balance = new Money(result.FinalBalance.GetBtc(), MoneyUnit.BTC)
                };

                return output;

            

           
            
        }

     
    }
}


//async Task<BalanceModel> OldExecute(string walletAddress)
//{
//    var addr = new BitcoinPubKeyAddress(walletAddress);


//    Network network = BtcService.network;
//    var url = BtcService.BaseAddress;

//    var uri = new Uri(url);
//    var client = new QBitNinjaClient(baseAddress: uri, network: network);
//    BalanceModel balanceModel = await client.GetBalance(dest: addr);
//    return balanceModel;
//}

//public async Task<decimal> GetTotalConfirmBalance(string addr)
//{
//    await Task.Delay(0);
//    //var t = await Execute(addr);
//    //var t2 = t.Operations.Where(x => x.Confirmations > 1).ToList();
//    //var t3 = t2.Sum(x => x.Amount);
//    var t3 = this.BitcoinService.GetBalance(addr);
//    return t3;
//}
