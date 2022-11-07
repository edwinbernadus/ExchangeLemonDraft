﻿// <auto-generated />
using System;
using BlueLight.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExchangeLemonCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlueLight.Main.AccountBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance");

                    b.Property<Guid>("GuidCode");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<long>("Version")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.ToTable("AccountBalances");
                });

            modelBuilder.Entity("BlueLight.Main.AccountHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountBalanceId");

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<double>("RunningBalance");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("AccountBalanceId");

                    b.ToTable("AccountHistory");
                });

            modelBuilder.Entity("BlueLight.Main.AccountTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CurrencyCode");

                    b.Property<string>("CurrencyPair");

                    b.Property<string>("DebitCreditType");

                    b.Property<bool>("IsExternal");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("RunningBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int?>("TransactionId");

                    b.Property<int?>("UserProfileDetailId");

                    b.Property<int?>("UserProfileid");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("UserProfileDetailId");

                    b.HasIndex("UserProfileid");

                    b.ToTable("AccountTransactions");
                });

            modelBuilder.Entity("BlueLight.Main.AdjustmentTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AdjustmentAmount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CurrencyCode");

                    b.Property<decimal>("PrevHoldBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("RunningBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int?>("UserProfileDetailId");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileDetailId");

                    b.ToTable("AdjustmentTransaction");
                });

            modelBuilder.Entity("BlueLight.Main.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BlueLight.Main.HoldTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<string>("CurrencyCode");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ParentId");

                    b.Property<decimal>("RunningHoldBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ParentId");

                    b.ToTable("HoldTransactions");
                });

            modelBuilder.Entity("BlueLight.Main.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTime?>("CancelDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CurrencyPair");

                    b.Property<Guid>("GuidId");

                    b.Property<bool>("IsBuy");

                    b.Property<bool>("IsCancelled");

                    b.Property<bool>("IsFillComplete");

                    b.Property<bool>("IsOpenOrder");

                    b.Property<decimal>("LeftAmount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("RequestRate")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<decimal>("TotalTransactions")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int?>("UserProfileid");

                    b.Property<long>("Version")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.HasIndex("UserProfileid");

                    b.HasIndex("IsOpenOrder", "RequestRate");

                    b.HasIndex("IsBuy", "IsOpenOrder", "RequestRate");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BlueLight.Main.OrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyPair");

                    b.Property<bool>("IsBuy");

                    b.Property<int?>("OrderId");

                    b.Property<decimal>("RequestRate")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("RunningAmount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("RunningLeftAmount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int?>("TransactionId");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("TransactionId");

                    b.ToTable("OrderHistories");
                });

            modelBuilder.Entity("BlueLight.Main.OrderTransaction", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("TransactionId");

                    b.Property<int>("Id");

                    b.HasKey("OrderId", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("OrderTransactions");
                });

            modelBuilder.Entity("BlueLight.Main.PendingBulkTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("PendingBulkTransfers");
                });

            modelBuilder.Entity("BlueLight.Main.PendingTransferList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountTransactionId");

                    b.Property<string>("AddressDestination");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int>("ConfirmTransfer");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<int?>("HoldTransactionId");

                    b.Property<bool>("IsApprove");

                    b.Property<DateTimeOffset>("LastCheckTransferDate");

                    b.Property<int?>("PendingBulkTransferId");

                    b.Property<string>("StatusTransfer");

                    b.Property<string>("TransactionId");

                    b.Property<int?>("UserProfileDetailId");

                    b.HasKey("Id");

                    b.HasIndex("AccountTransactionId");

                    b.HasIndex("HoldTransactionId");

                    b.HasIndex("PendingBulkTransferId");

                    b.HasIndex("UserProfileDetailId");

                    b.ToTable("PendingTransferLists");
                });

            modelBuilder.Entity("BlueLight.Main.PendingTransferListHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfirmTransfer");

                    b.Property<string>("Content");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsError");

                    b.Property<bool>("IsManual");

                    b.Property<int?>("PendingTransferListId");

                    b.HasKey("Id");

                    b.HasIndex("PendingTransferListId");

                    b.ToTable("PendingTransferListHistory");
                });

            modelBuilder.Entity("BlueLight.Main.RemittanceIncomingTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("TransactionId");

                    b.Property<int?>("UserProfileDetailId");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileDetailId");

                    b.ToTable("RemittanceIncomingTransactions");
                });

            modelBuilder.Entity("BlueLight.Main.RemittanceTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CurrencyCode");

                    b.Property<bool>("IsIncoming");

                    b.Property<decimal>("RunningBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int?>("UserProfileDetailId");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileDetailId");

                    b.ToTable("RemittanceTransaction");
                });

            modelBuilder.Entity("BlueLight.Main.SentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<int>("From");

                    b.Property<int?>("PendingTransferListId");

                    b.HasKey("Id");

                    b.HasIndex("PendingTransferListId");

                    b.ToTable("SentItems");
                });

            modelBuilder.Entity("BlueLight.Main.SpotMarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyPair");

                    b.Property<decimal>("LastRate")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<double>("PercentageMovement");

                    b.Property<double>("Volume");

                    b.HasKey("Id");

                    b.ToTable("SpotMarkets");
                });

            modelBuilder.Entity("BlueLight.Main.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<string>("CurrencyPair");

                    b.Property<bool>("IsBuyTaker");

                    b.Property<int?>("OrderId");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<decimal>("TransactionRate")
                        .HasColumnType("decimal(38, 8)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BlueLight.Main.UserProfile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("BlueLight.Main.UserProfileDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<string>("CurrencyCode");

                    b.Property<decimal>("HoldBalance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<long>("HoldVersion")
                        .IsConcurrencyToken();

                    b.Property<decimal>("IncomingRemittance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<decimal>("OutgoingRemittance")
                        .HasColumnType("decimal(38, 8)");

                    b.Property<string>("PrivateKey");

                    b.Property<string>("PublicAddress");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("UserProfileid");

                    b.Property<long>("Version")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.HasIndex("UserProfileid");

                    b.ToTable("UserProfileDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Changed");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Kind");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("AutoHistory");
                });

            modelBuilder.Entity("BlueLight.Main.AccountHistory", b =>
                {
                    b.HasOne("BlueLight.Main.AccountBalance")
                        .WithMany("AccountHistories")
                        .HasForeignKey("AccountBalanceId");
                });

            modelBuilder.Entity("BlueLight.Main.AccountTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");

                    b.HasOne("BlueLight.Main.UserProfileDetail", "UserProfileDetail")
                        .WithMany("AccountTransactions")
                        .HasForeignKey("UserProfileDetailId");

                    b.HasOne("BlueLight.Main.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileid");
                });

            modelBuilder.Entity("BlueLight.Main.AdjustmentTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.UserProfileDetail")
                        .WithMany("AdjustmentTransactions")
                        .HasForeignKey("UserProfileDetailId");
                });

            modelBuilder.Entity("BlueLight.Main.HoldTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("BlueLight.Main.UserProfileDetail", "Parent")
                        .WithMany("HoldTransactions")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("BlueLight.Main.Order", b =>
                {
                    b.HasOne("BlueLight.Main.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileid");
                });

            modelBuilder.Entity("BlueLight.Main.OrderHistory", b =>
                {
                    b.HasOne("BlueLight.Main.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("BlueLight.Main.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("BlueLight.Main.OrderTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.Order", "Order")
                        .WithMany("OrderTransactions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlueLight.Main.Transaction", "Transaction")
                        .WithMany("OrderTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlueLight.Main.PendingTransferList", b =>
                {
                    b.HasOne("BlueLight.Main.AccountTransaction", "AccountTransaction")
                        .WithMany()
                        .HasForeignKey("AccountTransactionId");

                    b.HasOne("BlueLight.Main.HoldTransaction", "HoldTransaction")
                        .WithMany()
                        .HasForeignKey("HoldTransactionId");

                    b.HasOne("BlueLight.Main.PendingBulkTransfer", "PendingBulkTransfer")
                        .WithMany("Collection")
                        .HasForeignKey("PendingBulkTransferId");

                    b.HasOne("BlueLight.Main.UserProfileDetail", "UserProfileDetail")
                        .WithMany()
                        .HasForeignKey("UserProfileDetailId");
                });

            modelBuilder.Entity("BlueLight.Main.PendingTransferListHistory", b =>
                {
                    b.HasOne("BlueLight.Main.PendingTransferList", "PendingTransferList")
                        .WithMany("PendingTransferListHistories")
                        .HasForeignKey("PendingTransferListId");
                });

            modelBuilder.Entity("BlueLight.Main.RemittanceIncomingTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.UserProfileDetail", "UserProfileDetail")
                        .WithMany("RemittanceIncomingTransactions")
                        .HasForeignKey("UserProfileDetailId");
                });

            modelBuilder.Entity("BlueLight.Main.RemittanceTransaction", b =>
                {
                    b.HasOne("BlueLight.Main.UserProfileDetail")
                        .WithMany("RemittanceTransactions")
                        .HasForeignKey("UserProfileDetailId");
                });

            modelBuilder.Entity("BlueLight.Main.SentItem", b =>
                {
                    b.HasOne("BlueLight.Main.PendingTransferList", "PendingTransferList")
                        .WithMany()
                        .HasForeignKey("PendingTransferListId");
                });

            modelBuilder.Entity("BlueLight.Main.Transaction", b =>
                {
                    b.HasOne("BlueLight.Main.Order")
                        .WithMany("Transactions")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("BlueLight.Main.UserProfileDetail", b =>
                {
                    b.HasOne("BlueLight.Main.UserProfile", "UserProfile")
                        .WithMany("UserProfileDetails")
                        .HasForeignKey("UserProfileid");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BlueLight.Main.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BlueLight.Main.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlueLight.Main.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BlueLight.Main.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
