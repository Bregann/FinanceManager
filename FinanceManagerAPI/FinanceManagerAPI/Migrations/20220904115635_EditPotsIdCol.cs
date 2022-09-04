using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class EditPotsIdCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pots",
                newName: "potId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "potId",
                table: "Pots",
                newName: "id");
        }
    }
}
