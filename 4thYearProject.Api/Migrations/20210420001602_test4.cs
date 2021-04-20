using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_FeedData_ProfileDataId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ProfileDataId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ProfileDataId",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileDataId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProfileDataId",
                table: "Posts",
                column: "ProfileDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_FeedData_ProfileDataId",
                table: "Posts",
                column: "ProfileDataId",
                principalTable: "FeedData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
