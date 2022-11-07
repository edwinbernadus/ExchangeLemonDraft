using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations.Logging
{
    public partial class LogHttpRawTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "LogHttpRaws",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "LogHttpRaws");
        }
    }
}
