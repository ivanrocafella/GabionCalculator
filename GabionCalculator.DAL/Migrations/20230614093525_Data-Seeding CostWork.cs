using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GabionCalculator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingCostWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CostWorks",
                columns: new[] { "Id", "DateStart", "DateUpdate", "ExchangeDollar", "Margin", "PNR", "TimeSettingEguip", "TimeWeldingOneCrossBar" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 87.0, 1.2, 7.0, 5.0, 0.0050000000000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CostWorks",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
