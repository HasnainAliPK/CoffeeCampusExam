using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeCampus.Migrations
{
    /// <inheritdoc />
    public partial class emptybin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoseChanges_CoffeeMachines_CoffeeMachineID",
                table: "HoseChanges");

            migrationBuilder.RenameColumn(
                name: "CoffeeMachineID",
                table: "HoseChanges",
                newName: "CoffeeMachineId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "HoseChanges",
                newName: "ChangeDate");

            migrationBuilder.RenameIndex(
                name: "IX_HoseChanges_CoffeeMachineID",
                table: "HoseChanges",
                newName: "IX_HoseChanges_CoffeeMachineId");

            migrationBuilder.CreateTable(
                name: "BinEmptyings",
                columns: table => new
                {
                    EmptyingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoffeeMachineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinEmptyings", x => x.EmptyingID);
                    table.ForeignKey(
                        name: "FK_BinEmptyings_CoffeeMachines_CoffeeMachineID",
                        column: x => x.CoffeeMachineID,
                        principalTable: "CoffeeMachines",
                        principalColumn: "CoffeeMachineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoseChangeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoseChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinEmptyings_CoffeeMachineID",
                table: "BinEmptyings",
                column: "CoffeeMachineID");

            migrationBuilder.AddForeignKey(
                name: "FK_HoseChanges_CoffeeMachines_CoffeeMachineId",
                table: "HoseChanges",
                column: "CoffeeMachineId",
                principalTable: "CoffeeMachines",
                principalColumn: "CoffeeMachineID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoseChanges_CoffeeMachines_CoffeeMachineId",
                table: "HoseChanges");

            migrationBuilder.DropTable(
                name: "BinEmptyings");

            migrationBuilder.DropTable(
                name: "HoseChangeLogs");

            migrationBuilder.DropTable(
                name: "ServiceLogs");

            migrationBuilder.RenameColumn(
                name: "CoffeeMachineId",
                table: "HoseChanges",
                newName: "CoffeeMachineID");

            migrationBuilder.RenameColumn(
                name: "ChangeDate",
                table: "HoseChanges",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_HoseChanges_CoffeeMachineId",
                table: "HoseChanges",
                newName: "IX_HoseChanges_CoffeeMachineID");

            migrationBuilder.AddForeignKey(
                name: "FK_HoseChanges_CoffeeMachines_CoffeeMachineID",
                table: "HoseChanges",
                column: "CoffeeMachineID",
                principalTable: "CoffeeMachines",
                principalColumn: "CoffeeMachineID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
