using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.Logging
{
    public partial class ReceiveLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogReceiveAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<Guid>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogReceiveAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogReceiveRaws",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<Guid>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogReceiveRaws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogReceiveAddressDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogReceiveAddressId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogReceiveAddressDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogReceiveAddressDetails_LogReceiveAddresses_LogReceiveAddressId",
                        column: x => x.LogReceiveAddressId,
                        principalTable: "LogReceiveAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogReceiveAddressDetails_LogReceiveAddressId",
                table: "LogReceiveAddressDetails",
                column: "LogReceiveAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogReceiveAddressDetails");

            migrationBuilder.DropTable(
                name: "LogReceiveRaws");

            migrationBuilder.DropTable(
                name: "LogReceiveAddresses");
        }
    }
}
