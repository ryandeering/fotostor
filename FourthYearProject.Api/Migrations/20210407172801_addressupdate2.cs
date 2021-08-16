using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class addressupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "UserName",
                "Address",
                "UserPostcode");

            migrationBuilder.AddColumn<string>(
                "UserAddress2",
                "Address",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "UserFName",
                "Address",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "UserLName",
                "Address",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UserAddress2",
                "Address");

            migrationBuilder.DropColumn(
                "UserFName",
                "Address");

            migrationBuilder.DropColumn(
                "UserLName",
                "Address");

            migrationBuilder.RenameColumn(
                "UserPostcode",
                "Address",
                "UserName");
        }
    }
}