using Microsoft.EntityFrameworkCore.Migrations;

namespace Brander.Data.Migrations
{
    public partial class addPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Key");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Game",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Game");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Key",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
