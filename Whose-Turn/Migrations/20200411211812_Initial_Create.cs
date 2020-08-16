using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Whose_Turn.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseHolds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ManOfTheHouse = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseHolds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Repeat = table.Column<string>(nullable: false),
                    Priority = table.Column<string>(nullable: false),
                    Privacy = table.Column<string>(nullable: false),
                    EnableNotification = table.Column<bool>(nullable: false),
                    AllowEdits = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoPreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    SecurityToken = table.Column<string>(nullable: true),
                    TwoFactorRequired = table.Column<bool>(nullable: false),
                    AccountClosed = table.Column<bool>(nullable: false),
                    IsLockedOut = table.Column<bool>(nullable: false),
                    LockoutReason = table.Column<string>(nullable: true),
                    LockoutEndDate = table.Column<DateTime>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    HouseHoldId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_HouseHolds_HouseHoldId",
                        column: x => x.HouseHoldId,
                        principalTable: "HouseHolds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DueOn = table.Column<DateTime>(nullable: false),
                    Task = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CompletedBy = table.Column<Guid>(nullable: true),
                    CompletedOn = table.Column<DateTime>(nullable: true),
                    PreferencesId = table.Column<Guid>(nullable: false),
                    AssignedTo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_TodoPreferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "TodoPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseHolds_Id",
                table: "HouseHolds",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoPreferences_Id",
                table: "TodoPreferences",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Id",
                table: "Todos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_PreferencesId",
                table: "Todos",
                column: "PreferencesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HouseHoldId",
                table: "Users",
                column: "HouseHoldId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Email",
                table: "Users",
                columns: new[] { "Id", "Email" },
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TodoPreferences");

            migrationBuilder.DropTable(
                name: "HouseHolds");
        }
    }
}
