using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "id", "imgUrl", "merchantName", "potId", "processed", "transactionAmount", "transactionDate" },
                values: new object[,]
                {
                    { "abc123", "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", "Morrisons", 0L, false, 5.44m, new DateTime(2022, 9, 1, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { "abc1234", "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", "Greggs", 0L, false, 2.44m, new DateTime(2022, 8, 31, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { "abc1235", "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", "Sainsburys", 0L, false, 44.44m, new DateTime(2022, 8, 30, 23, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "id",
                keyValue: "abc123");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "id",
                keyValue: "abc1234");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "id",
                keyValue: "abc1235");
        }
    }
}
