using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.Logging
{
    public partial class LogHttpRawThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalInfo",
                table: "LogHttpRaws",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "LogHttpRaws",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "LogHttpRaws",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "LogHttpRaws",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Method",
                table: "LogHttpRaws");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "LogHttpRaws");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "LogHttpRaws");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "LogHttpRaws",
                newName: "AdditionalInfo");
        }
    }
}
