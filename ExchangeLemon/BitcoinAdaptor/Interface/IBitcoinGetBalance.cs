using NBitcoin;
using QBitNinja.Client.Models;
using System.Threading.Tasks;

namespace BlueLight.Main
{
    public interface IBitcoinGetBalance
    {
        
        Task<BtcTransaction> Execute(string addr);
    }
}
