using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class AddressRestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UserAddress",
                "Orders");

            migrationBuilder.DropColumn(
                "UserCity",
                "Orders");

            migrationBuilder.DropColumn(
                "UserCountry",
                "Orders");

            migrationBuilder.AddColumn<int>(
                "OrderAddressId",
                "Orders",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Orders_OrderAddressId",
                "Orders",
                "OrderAddressId");

            migrationBuilder.AddForeignKey(
                "FK_Orders_Address_OrderAddressId",
                "Orders",
                "OrderAddressId",
                "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Orders_Address_OrderAddressId",
                "Orders");

            migrationBuilder.DropIndex(
                "IX_Orders_OrderAddressId",
                "Orders");

            migrationBuilder.DropColumn(
                "OrderAddressId",
                "Orders");

            migrationBuilder.AddColumn<string>(
                "UserAddress",
                "Orders",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "UserCity",
                "Orders",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "UserCountry",
                "Orders",
                "nvarchar(max)",
                nullable: true);
        }
    }
}