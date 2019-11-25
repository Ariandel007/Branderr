using Microsoft.EntityFrameworkCore.Migrations;

namespace Brander.Data.Migrations
{
    public partial class addCambiosDetailsx2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderDetails");
        }
    }
}
