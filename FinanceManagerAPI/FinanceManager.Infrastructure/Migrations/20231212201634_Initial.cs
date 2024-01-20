using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    ConfigName = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonzoClientId = table.Column<string>(type: "text", nullable: false),
                    MonzoAccountId = table.Column<string>(type: "text", nullable: false),
                    MonzoClientSecret = table.Column<string>(type: "text", nullable: false),
                    MonzoRefreshToken = table.Column<string>(type: "text", nullable: false),
                    MonzoAccessToken = table.Column<string>(type: "text", nullable: false),
                    HFUsername = table.Column<string>(type: "text", nullable: false),
                    HFPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.ConfigName);
                });

            migrationBuilder.CreateTable(
                name: "Pots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PotName = table.Column<string>(type: "text", nullable: false),
                    PotAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PotAmountLeft = table.Column<decimal>(type: "numeric", nullable: false),
                    IsSavingsPot = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Processed = table.Column<bool>(type: "boolean", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MerchantName = table.Column<string>(type: "text", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PotId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Pots_PotId",
                        column: x => x.PotId,
                        principalTable: "Pots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PotId",
                table: "Transactions",
                column: "PotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Pots");
        }
    }
}
