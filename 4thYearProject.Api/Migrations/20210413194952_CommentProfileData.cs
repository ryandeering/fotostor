using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class CommentProfileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ProfileDataId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfileDataId",
                table: "Comments",
                column: "ProfileDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FeedData_ProfileDataId",
                table: "Comments",
                column: "ProfileDataId",
                principalTable: "FeedData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FeedData_ProfileDataId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProfileDataId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProfileDataId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
