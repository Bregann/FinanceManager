using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    public partial class ConfigTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    configName = table.Column<string>(type: "text", nullable: false),
                    clientID = table.Column<string>(type: "text", nullable: false),
                    clientSecret = table.Column<string>(type: "text", nullable: false),
                    refreshToken = table.Column<string>(type: "text", nullable: false),
                    accessToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.configName);
                });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "configName", "accessToken", "clientID", "clientSecret", "refreshToken" },
                values: new object[] { "Main", "", "", "", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");
        }
    }
}
