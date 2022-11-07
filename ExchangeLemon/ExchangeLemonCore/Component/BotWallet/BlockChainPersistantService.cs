using System;
using System.Linq;
using System.Threading.Tasks;
using BotWalletWatcherLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BotWalletWatcher
{
    public class BlockChainPersistantService
    {
        public BlockChainPersistantService(BlockContext blockContext, IConfiguration configuration)
        {
            BlockContext = blockContext;
            Configuration = configuration;
        }

        public BlockContext BlockContext { get; }
        public IConfiguration Configuration { get; }

        public async Task SaveLogSend(int blockChainId, string transactionId, int confirmations)
        {
            var input = new WatcherTransaction()
            {
                TransactionId = transactionId,
                Confirmations = confirmations,
                BlockChainId = blockChainId,
                IsSend = false
            };
            BlockContext.WatcherTransactions.Add(input);
            await BlockContext.SaveChangesAsync();
        }

        public async Task SaveLogReceive(int blockChainId, string address)
        {
            var input = new WatcherAddressItem()
            {
                Address = address,
                BlockChainId = blockChainId,
                IsSend = false
            };
            BlockContext.WatcherAddresses.Add(input);
            await BlockContext.SaveChangesAsync();
        }

        public async Task<int> InquiryLastBlock()
        {
            var output = await this.BlockContext.WatcherBlockChains
              .OrderByDescending(x => x.BlockChainNumber).FirstOrDefaultAsync();
            int defaultLast = Configuration.GetValue<int>("LastBlockChainPosition");
            //var defaultLast = 1445935;
            int result = output?.BlockChainNumber ?? defaultLast;
            if (result == -1)
            {
                result = defaultLast;
            }
            return result;
        }
    }
}
