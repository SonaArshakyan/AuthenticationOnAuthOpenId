using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreAuthenticatAuthorizeProjectExample.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FavColor", "GoogleId", "Password", "Role", "UserName" },
                values: new object[] { 2, "yellow", "104018143982611409017", "Sonajan_21", "Admin", "Sona" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "GoogleId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
