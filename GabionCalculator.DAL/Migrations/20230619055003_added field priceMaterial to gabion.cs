using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GabionCalculator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldpriceMaterialtogabion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceMaterialBatch",
                table: "Gabions",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceMaterialBatch",
                table: "Gabions");
        }
    }
}
