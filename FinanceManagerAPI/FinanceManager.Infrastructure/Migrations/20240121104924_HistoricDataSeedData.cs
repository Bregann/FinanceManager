using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoricDataSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HistoricData",
                columns: new[] { "Id", "AmountSaved", "AmountSpent", "MonthEnd", "MonthStart", "MonthlyIncome" },
                values: new object[] { 1, 0m, 0m, new DateTime(2024, 1, 21, 10, 49, 24, 360, DateTimeKind.Utc).AddTicks(6255), new DateTime(2024, 1, 21, 10, 49, 24, 360, DateTimeKind.Utc).AddTicks(6256), 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
