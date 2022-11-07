using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class RemittanceOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPosition",
                table: "UserProfileDetails");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmTransfer",
                table: "PendingTransferLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastCheckTransferDate",
                table: "PendingTransferLists",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "PendingTransferListHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    ConfirmTransfer = table.Column<int>(nullable: false),
                    IsError = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    PendingTransferListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingTransferListHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingTransferListHistory_PendingTransferLists_PendingTransferListId",
                        column: x => x.PendingTransferListId,
                        principalTable: "PendingTransferLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceIncomingTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProfileDetailId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(38, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceIncomingTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceIncomingTransactions_UserProfileDetails_UserProfileDetailId",
                        column: x => x.UserProfileDetailId,
                        principalTable: "UserProfileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PendingTransferListHistory_PendingTransferListId",
                table: "PendingTransferListHistory",
                column: "PendingTransferListId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceIncomingTransactions_UserProfileDetailId",
                table: "RemittanceIncomingTransactions",
                column: "UserProfileDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingTransferListHistory");

            migrationBuilder.DropTable(
                name: "RemittanceIncomingTransactions");

            migrationBuilder.DropColumn(
                name: "ConfirmTransfer",
                table: "PendingTransferLists");

            migrationBuilder.DropColumn(
                name: "LastCheckTransferDate",
                table: "PendingTransferLists");

            migrationBuilder.AddColumn<int>(
                name: "LastPosition",
                table: "UserProfileDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
