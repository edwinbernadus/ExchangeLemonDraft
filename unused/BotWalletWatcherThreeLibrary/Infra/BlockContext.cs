using BotWalletWatcherLibrary;
using Microsoft.EntityFrameworkCore;

namespace BotWalletWatcher
{
    public class BlockContext : DbContext
    {
        // private LoggerFactory _loggerFactory;

        public BlockContext(DbContextOptions<BlockContext> options) : base(options)
        {

        }


        //public DbSet<TransactionList> TransactionLists { get; set; }
        //public DbSet<TransactionResult> InquiryTransactionResults { get; set; }
        //public DbSet<BlockChainList> BlockChainLists { get; set; }



        public DbSet<WatcherBlockChain> WatcherBlockChains { get; set; }
        public DbSet<WatcherTransaction> WatcherTransactions { get; set; }
        public DbSet<WatcherAddressItem> WatcherAddresses { get; set; }

    }
}