using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class LastPositionUserProfileTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPosition",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "LastPosition",
                table: "UserProfileDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPosition",
                table: "UserProfileDetails");

            migrationBuilder.AddColumn<int>(
                name: "LastPosition",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);
        }
    }
}
