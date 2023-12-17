using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDbGeneratedids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PotAmountSpent",
                table: "Pots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "PotAmountLeft",
                table: "Pots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<long>(
                name: "PotAmount",
                table: "Pots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PotAmountSpent",
                table: "Pots",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotAmountLeft",
                table: "Pots",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "PotAmount",
                table: "Pots",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
