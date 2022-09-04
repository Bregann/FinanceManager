using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class amountleftthismonth_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "amountLeftThisMonth",
                table: "Pots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amountLeftThisMonth",
                table: "Pots");
        }
    }
}
