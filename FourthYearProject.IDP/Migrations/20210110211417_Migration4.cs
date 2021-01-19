using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.IDP.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFile",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoFile",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
