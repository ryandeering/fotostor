using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class aaaaashite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProfilePic",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePic",
                table: "Users");

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
    }
}
