using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class AddressChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_UserDataId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserDataId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserDataId",
                table: "Address",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserDataId",
                table: "Address",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_UserDataId",
                table: "Address",
                column: "UserDataId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
