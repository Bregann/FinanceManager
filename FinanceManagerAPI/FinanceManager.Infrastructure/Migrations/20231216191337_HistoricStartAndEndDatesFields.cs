using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoricStartAndEndDatesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Month",
                table: "HistoricData",
                newName: "MonthStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "MonthEnd",
                table: "HistoricData",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthEnd",
                table: "HistoricData");

            migrationBuilder.RenameColumn(
                name: "MonthStart",
                table: "HistoricData",
                newName: "Month");
        }
    }
}
