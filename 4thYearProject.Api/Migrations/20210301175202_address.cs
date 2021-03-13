using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Address",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserAddress = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCity = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCountry = table.Column<string>("nvarchar(max)", nullable: true),
                    UserDataId = table.Column<string>("nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        "FK_Address_Users_UserDataId",
                        x => x.UserDataId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Address_UserDataId",
                "Address",
                "UserDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Address");
        }
    }
}