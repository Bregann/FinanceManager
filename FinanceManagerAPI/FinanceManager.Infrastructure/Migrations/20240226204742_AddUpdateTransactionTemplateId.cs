using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateTransactionTemplateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedTransactionTemplateId",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 20, 47, 41, 918, DateTimeKind.Utc).AddTicks(3), new DateTime(2024, 2, 26, 20, 47, 41, 918, DateTimeKind.Utc).AddTicks(4) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedTransactionTemplateId",
                table: "Config");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 19, 46, 35, 594, DateTimeKind.Utc).AddTicks(129), new DateTime(2024, 2, 26, 19, 46, 35, 594, DateTimeKind.Utc).AddTicks(130) });
        }
    }
}
