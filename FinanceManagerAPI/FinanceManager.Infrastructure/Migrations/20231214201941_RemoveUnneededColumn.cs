using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnneededColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Pots_PotId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PotName",
                table: "HistoricData");

            migrationBuilder.AlterColumn<int>(
                name: "PotId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Pots_PotId",
                table: "Transactions",
                column: "PotId",
                principalTable: "Pots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Pots_PotId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "PotId",
                table: "Transactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "PotName",
                table: "HistoricData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Pots_PotId",
                table: "Transactions",
                column: "PotId",
                principalTable: "Pots",
                principalColumn: "Id");
        }
    }
}
