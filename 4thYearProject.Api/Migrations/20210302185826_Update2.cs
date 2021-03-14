namespace _4thYearProject.Api.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "PostDeleted",
                "Posts",
                "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "PostDeleted",
                "Posts");
        }
    }
}
