//using ExchangeLemonCore.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.Main
{

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {


        // public static int TestGetTotalUsers()
        // {
        //     var output = -1;
        //     using (var context = ApplicationContext.Generate())
        //     {
        //
        //         int total = DbTestingHelper.GetTotalUsers(context);
        //         output = total;
        //     }
        //
        //     return output;
        // }

        // public static ApplicationContext Generate()
        // {
        //     var connString = ParamRepo.SqlConnString;
        //
        //     var context = Generate(connString);
        //
        //     return context;
        // }

        public static ApplicationContext Generate(string connString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(connString);
            var context = new ApplicationContext(optionsBuilder.Options);

            return context;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderTransaction>()
                .HasKey(bc => new { bc.OrderId, bc.TransactionId });

            modelBuilder.Entity<OrderTransaction>()
                .HasOne(bc => bc.Order)
                .WithMany(b => b.OrderTransactions)
                .HasForeignKey(bc => bc.OrderId);

            modelBuilder.Entity<OrderTransaction>()
                .HasOne(bc => bc.Transaction)
                .WithMany(c => c.OrderTransactions)
                .HasForeignKey(bc => bc.TransactionId);

            //modelBuilder.Entity<TreePark>()
            //    .HasKey(t => new { t.TreeId, t.ParkId });

            modelBuilder.Entity
            <Order>()
                    .HasIndex(t => new { t.IsBuy, t.IsOpenOrder, t.RequestRate });


            modelBuilder.Entity
            <Order>()
                    .HasIndex(t => new { t.IsOpenOrder, t.RequestRate });

            modelBuilder.EnableAutoHistory(null);

            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

        //public DbSet<StudentFive> Students { get; set; }
        //public DbSet<AddressFive> Addresses { get; set; }
        //public DbSet<Student> StudentsOriginal { get; set; }

        public DbSet<Order> Orders { get; set; }

        //public DbSet<Market> Markets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<OrderTransaction> OrderTransactions { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserProfileDetail> UserProfileDetails { get; set; }
        public DbSet<RemittanceIncomingTransaction> RemittanceIncomingTransactions { get; set; }

        public DbSet<SpotMarket> SpotMarkets { get; set; }

        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public DbSet<AccountBalance> AccountBalances { get; set; }
        public DbSet<SentItem> SentItems { get; set; }
        

        public DbSet<HoldTransaction> HoldTransactions { get; set; }

        //public DbSet<Tree> Trees { get; set; }
        //public DbSet<Park> Parks { get; set; }

        public DbSet<OrderHistory> OrderHistories { get; set; }











        public DbSet<OrderTransaction> OrderTransactions { get; set; }

        //public DbSet<MvPendingTransferList> MvPendingTransferLists { get; set; }
        public DbSet<PendingBulkTransfer> PendingBulkTransfers { get; set; }
        public DbSet<PendingTransferList> PendingTransferLists { get; set; }



    }

}


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
