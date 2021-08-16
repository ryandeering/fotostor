namespace FourthYearProject.Api.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ShirtSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Size",
                "LineItems",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Size",
                "LineItems");
        }
    }
}
