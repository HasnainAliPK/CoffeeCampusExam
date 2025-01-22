using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeCampus.Migrations
{
    /// <inheritdoc />
    public partial class Refill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Refills",
                newName: "RefillDate");

            migrationBuilder.AddColumn<int>(
                name: "RefillAmount",
                table: "Refills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefillAmount",
                table: "Refills");

            migrationBuilder.RenameColumn(
                name: "RefillDate",
                table: "Refills",
                newName: "DateTime");
        }
    }
}
