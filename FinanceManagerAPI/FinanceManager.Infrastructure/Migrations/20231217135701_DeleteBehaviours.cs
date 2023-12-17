using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomaticTransactions_Pots_PotId",
                table: "AutomaticTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticTransactions_Pots_PotId",
                table: "AutomaticTransactions",
                column: "PotId",
                principalTable: "Pots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomaticTransactions_Pots_PotId",
                table: "AutomaticTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticTransactions_Pots_PotId",
                table: "AutomaticTransactions",
                column: "PotId",
                principalTable: "Pots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
