using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GabionCalculator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingnewtableCostWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExchangeDollar = table.Column<double>(type: "double", nullable: false),
                    TimeWeldingOneCrossBar = table.Column<double>(type: "double", nullable: false),
                    TimeSettingEguip = table.Column<double>(type: "double", nullable: false),
                    PNR = table.Column<double>(type: "double", nullable: false),
                    Margin = table.Column<double>(type: "double", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostWorks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostWorks");
        }
    }
}
