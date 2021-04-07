using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class addressupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Address",
                newName: "UserPostcode");

            migrationBuilder.AddColumn<string>(
                name: "UserAddress2",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAddress2",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserFName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserLName",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "UserPostcode",
                table: "Address",
                newName: "UserName");
        }
    }
}
