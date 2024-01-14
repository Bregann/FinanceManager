using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pots",
                columns: new[] { "Id", "Deleted", "IsSavingsPot", "PotAmount", "PotAmountLeft", "PotAmountSpent", "PotName" },
                values: new object[] { 1, false, false, 0L, 0L, 0L, "Spare Money" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pots",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
