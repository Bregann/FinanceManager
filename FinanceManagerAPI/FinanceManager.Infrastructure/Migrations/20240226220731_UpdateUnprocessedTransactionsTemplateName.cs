using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnprocessedTransactionsTemplateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnprocessedTransactionsTemplateId",
                table: "Config",
                newName: "DailyStatsTemplateId");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 22, 7, 30, 798, DateTimeKind.Utc).AddTicks(3033), new DateTime(2024, 2, 26, 22, 7, 30, 798, DateTimeKind.Utc).AddTicks(3035) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DailyStatsTemplateId",
                table: "Config",
                newName: "UnprocessedTransactionsTemplateId");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 20, 47, 41, 918, DateTimeKind.Utc).AddTicks(3), new DateTime(2024, 2, 26, 20, 47, 41, 918, DateTimeKind.Utc).AddTicks(4) });
        }
    }
}
