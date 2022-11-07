using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class TransferOutBtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PendingTransferListId",
                table: "SentItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusTransfer",
                table: "PendingTransferLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "PendingTransferLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentItems_PendingTransferListId",
                table: "SentItems",
                column: "PendingTransferListId");

            migrationBuilder.AddForeignKey(
                name: "FK_SentItems_PendingTransferLists_PendingTransferListId",
                table: "SentItems",
                column: "PendingTransferListId",
                principalTable: "PendingTransferLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SentItems_PendingTransferLists_PendingTransferListId",
                table: "SentItems");

            migrationBuilder.DropIndex(
                name: "IX_SentItems_PendingTransferListId",
                table: "SentItems");

            migrationBuilder.DropColumn(
                name: "PendingTransferListId",
                table: "SentItems");

            migrationBuilder.DropColumn(
                name: "StatusTransfer",
                table: "PendingTransferLists");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "PendingTransferLists");
        }
    }
}
