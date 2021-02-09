using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class Migration32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfilePic",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PicId",
                table: "Users",
                column: "PicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfilePic_PicId",
                table: "Users",
                column: "PicId",
                principalTable: "ProfilePic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfilePic_PicId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProfilePic");

            migrationBuilder.DropIndex(
                name: "IX_Users_PicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PicId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoFile",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
