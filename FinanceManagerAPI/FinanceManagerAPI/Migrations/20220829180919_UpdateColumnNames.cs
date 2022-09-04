using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class UpdateColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PotId",
                table: "Transactions",
                newName: "potId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pots",
                newName: "id");

            migrationBuilder.AlterColumn<long>(
                name: "potId",
                table: "Transactions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "potId",
                table: "Transactions",
                newName: "PotId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pots",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PotId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
