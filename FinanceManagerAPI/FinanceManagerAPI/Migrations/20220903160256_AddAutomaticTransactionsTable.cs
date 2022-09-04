using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class AddAutomaticTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutomaticTransactions",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    potId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomaticTransactions", x => x.name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutomaticTransactions");
        }
    }
}
