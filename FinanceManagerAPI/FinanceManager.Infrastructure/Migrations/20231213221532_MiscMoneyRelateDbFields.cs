using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MiscMoneyRelateDbFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Pots",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PotAmountSpent",
                table: "Pots",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "HistoricData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PotName = table.Column<string>(type: "text", nullable: false),
                    MonthlyIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountSaved = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountSpent = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricData");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Pots");

            migrationBuilder.DropColumn(
                name: "PotAmountSpent",
                table: "Pots");
        }
    }
}
