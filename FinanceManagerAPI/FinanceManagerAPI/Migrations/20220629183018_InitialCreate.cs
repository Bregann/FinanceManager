using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pots",
                columns: table => new
                {
                    potName = table.Column<string>(type: "text", nullable: false),
                    amountAllocated = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pots", x => x.potName);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    processed = table.Column<bool>(type: "boolean", nullable: false),
                    imgUrl = table.Column<string>(type: "text", nullable: false),
                    transactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    merchantName = table.Column<string>(type: "text", nullable: false),
                    transactionAmount = table.Column<long>(type: "bigint", nullable: false),
                    potType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pots");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
