using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class RemittanceTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalTransaction");

            migrationBuilder.RenameColumn(
                name: "ExtBalance",
                table: "UserProfileDetails",
                newName: "OutgoingRemittance");

            migrationBuilder.AddColumn<decimal>(
                name: "IncomingRemittance",
                table: "UserProfileDetails",
                type: "decimal(38, 8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RemittanceTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsIncoming = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(38, 8)", nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    RunningBalance = table.Column<decimal>(type: "decimal(38, 8)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    UserProfileDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceTransaction_UserProfileDetails_UserProfileDetailId",
                        column: x => x.UserProfileDetailId,
                        principalTable: "UserProfileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTransaction_UserProfileDetailId",
                table: "RemittanceTransaction",
                column: "UserProfileDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemittanceTransaction");

            migrationBuilder.DropColumn(
                name: "IncomingRemittance",
                table: "UserProfileDetails");

            migrationBuilder.RenameColumn(
                name: "OutgoingRemittance",
                table: "UserProfileDetails",
                newName: "ExtBalance");

            migrationBuilder.CreateTable(
                name: "ExternalTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "decimal(38, 8)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    RunningBalance = table.Column<decimal>(type: "decimal(38, 8)", nullable: false),
                    UserProfileDetailId = table.Column<int>(nullable: true),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTransaction_UserProfileDetails_UserProfileDetailId",
                        column: x => x.UserProfileDetailId,
                        principalTable: "UserProfileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTransaction_UserProfileDetailId",
                table: "ExternalTransaction",
                column: "UserProfileDetailId");
        }
    }
}
