using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class AddressChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Address_Users_UserDataId",
                "Address");

            migrationBuilder.DropIndex(
                "IX_Address_UserDataId",
                "Address");

            migrationBuilder.DropColumn(
                "UserDataId",
                "Address");

            migrationBuilder.AddColumn<int>(
                "AddressId",
                "Users",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Users_AddressId",
                "Users",
                "AddressId");

            migrationBuilder.AddForeignKey(
                "FK_Users_Address_AddressId",
                "Users",
                "AddressId",
                "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Users_Address_AddressId",
                "Users");

            migrationBuilder.DropIndex(
                "IX_Users_AddressId",
                "Users");

            migrationBuilder.DropColumn(
                "AddressId",
                "Users");

            migrationBuilder.AddColumn<string>(
                "UserDataId",
                "Address",
                "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Address_UserDataId",
                "Address",
                "UserDataId");

            migrationBuilder.AddForeignKey(
                "FK_Address_Users_UserDataId",
                "Address",
                "UserDataId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}