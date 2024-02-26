using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewConfigFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Config",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromEmailAddress",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromEmailAddressName",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MMSApiKey",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToEmailAddress",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToEmailAddressName",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnprocessedTransactionsTemplateId",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 2, 26, 19, 39, 4, 518, DateTimeKind.Utc).AddTicks(5694), new DateTime(2024, 2, 26, 19, 39, 4, 518, DateTimeKind.Utc).AddTicks(5697) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "FromEmailAddress",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "FromEmailAddressName",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "MMSApiKey",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "ToEmailAddress",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "ToEmailAddressName",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "UnprocessedTransactionsTemplateId",
                table: "Config");

            migrationBuilder.UpdateData(
                table: "HistoricData",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthEnd", "MonthStart" },
                values: new object[] { new DateTime(2024, 1, 21, 10, 49, 24, 360, DateTimeKind.Utc).AddTicks(6255), new DateTime(2024, 1, 21, 10, 49, 24, 360, DateTimeKind.Utc).AddTicks(6256) });
        }
    }
}
