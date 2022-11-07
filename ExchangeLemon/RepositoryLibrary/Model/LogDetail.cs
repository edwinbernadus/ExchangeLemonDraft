using System;

namespace BlueLight.Main
{
    public class LogDetail
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }


}


// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// {
//     base.OnConfiguring(optionsBuilder);

//     optionsBuilder.UseLoggerFactory(_loggerFactory);
// }


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<OrderTransaction>()
//        .HasKey(bc => new { bc.OrderId, bc.TransactionId});

//    modelBuilder.Entity<OrderTransaction>()
//        .HasOne(bc => bc.Order)
//        .WithMany(b => b.OrderTransactions)
//        .HasForeignKey(bc => bc.OrderId);

//    modelBuilder.Entity<OrderTransaction>()
//        .HasOne(bc => bc.Transaction)
//        .WithMany(c => c.OrderTransactions)
//        .HasForeignKey(bc => bc.TransactionId);
//}



//public System.Data.Entity.DbSet<SpotMarket> SpotMarkets { get; set; }

//public DbSet<UserProfile> UserProfiles { get; set; }

//protected override void OnModelCreating(DbModelBuilder modelBuilder)
//{

//    modelBuilder.Entity<UserProfileDetail>()
//             .HasMany<AccountTransaction>(s => s.AccountTransactions)
//             .WithRequired(x => x.UserProfileDetail);

//}

//modelBuilder.Entity<UserProfile>()
//         .HasMany<AccountTransaction>(s => s.AccountTransactions);

//modelBuilder.Entity<Transaction>()
//         .HasMany<Order>(s => s.Orders)
//         .WithMany(c => c.Transactions)
//         .Map(cs =>
//         {
//             cs.MapLeftKey("Transaction_Id");
//             cs.MapRightKey("Order_Id");
//             cs.ToTable("TransactionOrder");
//         });

//modelBuilder.Entity<Student>()
//            .HasMany<Course>(s => s.Courses)
//            .WithMany(c => c.Students)
//            .Map(cs =>
//            {
//                cs.MapLeftKey("StudentRefId");
//                cs.MapRightKey("CourseRefId");
//                cs.ToTable("StudentCourse");
//            });



// You can add custom code to this file. Changes will not be overwritten.
// 
// If you want Entity Framework to drop and regenerate your database
// automatically whenever you change your model schema, please use data migrations.
// For more information refer to the documentation:
// http://msdn.microsoft.com/en-us/data/jj591621.aspx

//public DBContext() : base("name=DBContext")
//{
//}

//public DBContext(string connString) : base(connString)
//{
//}

//public System.Data.Entity.DbSet<Currency> Currencies { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.Region> Regions { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.Country> Countries { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.City> Cities { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.IDRTransferMethod> IDRTransferMethods { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.IDRTransferSource> IDRTransferSources { get; set; }

//public System.Data.Entity.DbSet<ExchangeLemonNet.Models.MarketTrading> MarketTradings { get; set; }

//public System.Data.Entity.DbSet<ExchangeLemonNet.Models.PlayerVerOne> Players { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.PlayerAccountTransactionOld> PlayerAccountTransactions { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.TrxDepositIDR> PlayerAccountTransactionDeposits { get; set; }
////public System.Data.Entity.DbSet<PlayerAccountTransferDummy> PlayerAccountTransfers { get; set; }
//public System.Data.Entity.DbSet<BlueLight.Main.PlayerAccount> PlayerAccounts { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.TrxWithdrawalIDROld> PlayerAccountTransactionWithdrawals { get; set; }

//public System.Data.Entity.DbSet<BlueLight.Main.CurrencyPair> CurrencyPairs { get; set; }
