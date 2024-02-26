using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixChatIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "Config",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 19, 41, 22, 433, DateTimeKind.Utc).AddTicks(2250), new DateTime(2024, 2, 26, 19, 41, 22, 433, DateTimeKind.Utc).AddTicks(2251) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "Config",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 19, 39, 4, 518, DateTimeKind.Utc).AddTicks(5694), new DateTime(2024, 2, 26, 19, 39, 4, 518, DateTimeKind.Utc).AddTicks(5697) });
        }
    }
}
