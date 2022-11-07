using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class RemittanceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PendingBulkTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingBulkTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PendingTransferLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    UserProfileDetailId = table.Column<int>(nullable: true),
                    AccountTransactionId = table.Column<int>(nullable: true),
                    AddressDestination = table.Column<string>(nullable: true),
                    IsApprove = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    PendingBulkTransferId = table.Column<int>(nullable: true),
                    HoldTransactionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingTransferLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingTransferLists_AccountTransactions_AccountTransactionId",
                        column: x => x.AccountTransactionId,
                        principalTable: "AccountTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingTransferLists_HoldTransactions_HoldTransactionId",
                        column: x => x.HoldTransactionId,
                        principalTable: "HoldTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingTransferLists_PendingBulkTransfers_PendingBulkTransferId",
                        column: x => x.PendingBulkTransferId,
                        principalTable: "PendingBulkTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingTransferLists_UserProfileDetails_UserProfileDetailId",
                        column: x => x.UserProfileDetailId,
                        principalTable: "UserProfileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransferLists_AccountTransactionId",
                table: "PendingTransferLists",
                column: "AccountTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransferLists_HoldTransactionId",
                table: "PendingTransferLists",
                column: "HoldTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransferLists_PendingBulkTransferId",
                table: "PendingTransferLists",
                column: "PendingBulkTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransferLists_UserProfileDetailId",
                table: "PendingTransferLists",
                column: "UserProfileDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingTransferLists");

            migrationBuilder.DropTable(
                name: "PendingBulkTransfers");
        }
    }
}
