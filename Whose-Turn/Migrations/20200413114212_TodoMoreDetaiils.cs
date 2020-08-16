using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class TodoMoreDetaiils : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Task",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "TodoDescription",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TodoName",
                table: "Todos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodoDescription",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "TodoName",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "Todos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
