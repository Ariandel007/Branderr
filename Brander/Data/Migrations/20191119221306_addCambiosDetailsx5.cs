using Microsoft.EntityFrameworkCore.Migrations;

namespace Brander.Data.Migrations
{
    public partial class addCambiosDetailsx5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Key_KeyId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_KeyId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Game_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Game_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_KeyId",
                table: "OrderDetails",
                column: "KeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Key_KeyId",
                table: "OrderDetails",
                column: "KeyId",
                principalTable: "Key",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
