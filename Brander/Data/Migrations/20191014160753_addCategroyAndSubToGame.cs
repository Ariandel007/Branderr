using Microsoft.EntityFrameworkCore.Migrations;

namespace Brander.Data.Migrations
{
    public partial class addCategroyAndSubToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spicyness",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Game",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Game_CategoryId",
                table: "Game",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Category_CategoryId",
                table: "Game",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Category_CategoryId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_CategoryId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Spicyness",
                table: "Game",
                nullable: true);
        }
    }
}
