using System.Linq;
using System.Threading.Tasks;
using BitcoinLib.Services.Coins.Base;
using BotWalletWatcher.Helper;

namespace BotWalletWatcher
{
    public class BlockChainPositionService : IBlockChainPositionService
    {
        public BlockChainPositionService()
        {
            CoinService = CoinServiceHelper.GenerateCoinService();
            
        }

        public ICoinService CoinService { get; }
        

        public async Task<int> GetLocalPosition()
        {
            await Task.Delay(0);

            var lastBlockChain = await FileHelper.LoadAsync("last_block");
            if (string.IsNullOrEmpty(lastBlockChain))
            {
                return MainParam.DefaultLastBlockChainPosition;
            }
            else
            {
                var output = int.Parse(lastBlockChain);
                return output;
            }
            
        }

        public async Task<uint> GetNodePosition()
        {
            await Task.Delay(0);
            uint output = CoinService.GetBlockCount();
            return output;
        }
    }
}
