using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class ChangeToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "amountLeftThisMonth",
                table: "Pots",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "amountAllocated",
                table: "Pots",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Pots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "amountAllocated", "amountLeftThisMonth" },
                values: new object[] { 0m, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "amountLeftThisMonth",
                table: "Pots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "amountAllocated",
                table: "Pots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Pots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "amountAllocated", "amountLeftThisMonth" },
                values: new object[] { 0L, 0L });
        }
    }
}
