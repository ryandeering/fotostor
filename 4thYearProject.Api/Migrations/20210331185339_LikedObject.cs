using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class LikedObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Liked",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Liked",
                table: "Posts");
        }
    }
}
