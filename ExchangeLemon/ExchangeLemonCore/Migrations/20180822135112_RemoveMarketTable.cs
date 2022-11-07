using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class RemoveMarketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Markets_MarketId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MarketId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionRate",
                table: "AccountTransactions",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionRate",
                table: "AccountTransactions",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MarketId",
                table: "Orders",
                column: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Markets_MarketId",
                table: "Orders",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
