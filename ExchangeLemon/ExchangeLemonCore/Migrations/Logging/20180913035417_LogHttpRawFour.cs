using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.Logging
{
    public partial class LogHttpRawFour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "LogHttpRaws",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "LogHttpRaws",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
