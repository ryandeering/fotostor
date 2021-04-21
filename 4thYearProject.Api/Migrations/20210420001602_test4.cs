using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Posts_FeedData_ProfileDataId",
                "Posts");

            migrationBuilder.DropIndex(
                "IX_Posts_ProfileDataId",
                "Posts");

            migrationBuilder.DropColumn(
                "ProfileDataId",
                "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "ProfileDataId",
                "Posts",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Posts_ProfileDataId",
                "Posts",
                "ProfileDataId");

            migrationBuilder.AddForeignKey(
                "FK_Posts_FeedData_ProfileDataId",
                "Posts",
                "ProfileDataId",
                "FeedData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}