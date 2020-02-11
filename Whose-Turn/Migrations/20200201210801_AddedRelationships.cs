using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class AddedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_HouseHoldId",
                table: "Users",
                column: "HouseHoldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HouseHolds_HouseHoldId",
                table: "Users",
                column: "HouseHoldId",
                principalTable: "HouseHolds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_HouseHolds_HouseHoldId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HouseHoldId",
                table: "Users");
        }
    }
}
