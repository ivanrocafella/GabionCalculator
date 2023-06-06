using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GabionCalculator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changefieldUserIdinGabion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserlId",
                table: "Gabions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserlId",
                table: "Gabions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
