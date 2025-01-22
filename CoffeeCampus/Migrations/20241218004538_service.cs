using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeCampus.Migrations
{
    /// <inheritdoc />
    public partial class service : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Refills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Refills_UserId",
                table: "Refills",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refills_Users_UserId",
                table: "Refills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refills_Users_UserId",
                table: "Refills");

            migrationBuilder.DropIndex(
                name: "IX_Refills_UserId",
                table: "Refills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Refills");
        }
    }
}
