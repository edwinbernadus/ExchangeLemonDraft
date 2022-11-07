using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.LoggingExt
{
    public partial class SessionErrorMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "LogSessions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsError",
                table: "LogSessions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "LogSessions");

            migrationBuilder.DropColumn(
                name: "IsError",
                table: "LogSessions");
        }
    }
}
