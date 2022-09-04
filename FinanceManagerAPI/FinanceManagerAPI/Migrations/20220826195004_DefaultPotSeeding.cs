using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class DefaultPotSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pots",
                columns: new[] { "Id", "amountAllocated", "amountLeftThisMonth", "potName" },
                values: new object[] { 1, 0L, 0L, "Unallocated" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pots",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
