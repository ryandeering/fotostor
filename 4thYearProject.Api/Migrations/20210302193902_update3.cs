namespace _4thYearProject.Api.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Email",
                "Users",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Email",
                "Users");
        }
    }
}
