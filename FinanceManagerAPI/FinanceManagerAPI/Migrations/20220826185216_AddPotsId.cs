using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class AddPotsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pots",
                table: "Pots");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pots",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pots",
                table: "Pots",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pots",
                table: "Pots");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pots",
                table: "Pots",
                column: "potName");
        }
    }
}
