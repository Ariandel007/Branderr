using Microsoft.EntityFrameworkCore.Migrations;

namespace Brander.Data.Migrations
{
    public partial class addCambiosCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyId",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyId",
                table: "ShoppingCart");
        }
    }
}
