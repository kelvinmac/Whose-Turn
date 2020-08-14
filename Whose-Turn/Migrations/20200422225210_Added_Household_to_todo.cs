using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class Added_Household_to_todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HouseHoldId",
                table: "Todos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Todos_HouseHoldId",
                table: "Todos",
                column: "HouseHoldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_HouseHolds_HouseHoldId",
                table: "Todos",
                column: "HouseHoldId",
                principalTable: "HouseHolds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_HouseHolds_HouseHoldId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_HouseHoldId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "HouseHoldId",
                table: "Todos");
        }
    }
}
