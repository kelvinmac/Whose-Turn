using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class AddedSomeMoreColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountClosed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedOut",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEndDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockoutReason",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityToken",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorRequired",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountClosed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLockedOut",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEndDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutReason",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorRequired",
                table: "Users");
        }
    }
}
