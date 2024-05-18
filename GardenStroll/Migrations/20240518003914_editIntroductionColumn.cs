using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenStroll.Migrations
{
    /// <inheritdoc />
    public partial class editIntroductionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "introduction",
                table: "Users",
                newName: "Introduction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "Users",
                newName: "introduction");
        }
    }
}
