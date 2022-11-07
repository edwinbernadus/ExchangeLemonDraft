using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class DecimalAdjustTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LastRate",
                table: "SpotMarkets",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalTransactions",
                table: "Orders",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "LeftAmount",
                table: "Orders",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Orders",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "RunningBalance",
                table: "AdjustmentTransaction",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "PrevHoldBalance",
                table: "AdjustmentTransaction",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "AdjustmentAmount",
                table: "AdjustmentTransaction",
                type: "decimal(38, 8)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LastRate",
                table: "SpotMarkets",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalTransactions",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeftAmount",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RunningBalance",
                table: "AdjustmentTransaction",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrevHoldBalance",
                table: "AdjustmentTransaction",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdjustmentAmount",
                table: "AdjustmentTransaction",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 8)");
        }
    }
}
