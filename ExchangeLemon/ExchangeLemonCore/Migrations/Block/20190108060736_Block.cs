using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.Block
{
    public partial class Block : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatcherAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    BlockChainId = table.Column<int>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatcherBlockChains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockChainNumber = table.Column<int>(nullable: false),
                    Confirmations = table.Column<int>(nullable: false),
                    IsFinish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherBlockChains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatcherTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<string>(nullable: true),
                    Confirmations = table.Column<int>(nullable: false),
                    BlockChainId = table.Column<int>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherTransactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatcherAddresses");

            migrationBuilder.DropTable(
                name: "WatcherBlockChains");

            migrationBuilder.DropTable(
                name: "WatcherTransactions");
        }
    }
}
