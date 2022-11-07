using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class TuningQuery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TransactionRate",
                table: "AccountTransactions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileid",
                table: "AccountTransactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_UserProfileid",
                table: "AccountTransactions",
                column: "UserProfileid");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_UserProfiles_UserProfileid",
                table: "AccountTransactions",
                column: "UserProfileid",
                principalTable: "UserProfiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_UserProfiles_UserProfileid",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_UserProfileid",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionRate",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "UserProfileid",
                table: "AccountTransactions");
        }
    }
}
