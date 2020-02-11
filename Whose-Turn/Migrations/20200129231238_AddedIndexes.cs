using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class AddedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Email",
                table: "Users",
                columns: new[] { "Id", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Id",
                table: "Todos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHolds_Id",
                table: "HouseHolds",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Id",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_HouseHolds_Id",
                table: "HouseHolds");
        }
    }
}
