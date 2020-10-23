using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreAuthenticatAuthorizeProjectExample.Migrations
{
    public partial class Usersadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "LoginModels",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "LoginModels",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "RememeberLogin",
                table: "LoginModels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FavColor = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    GoogleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "RememeberLogin",
                table: "LoginModels");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "LoginModels",
                newName: "Username");

            migrationBuilder.AlterColumn<bool>(
                name: "Password",
                table: "LoginModels",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
