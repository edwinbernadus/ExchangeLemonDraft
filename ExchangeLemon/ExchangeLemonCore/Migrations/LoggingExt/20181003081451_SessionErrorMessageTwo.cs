using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.LoggingExt
{
    public partial class SessionErrorMessageTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "LogSessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "LogSessions");
        }
    }
}
