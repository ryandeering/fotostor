using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class CommentProfileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Username",
                "Comments");

            migrationBuilder.AddColumn<int>(
                "ProfileDataId",
                "Comments",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Comments_ProfileDataId",
                "Comments",
                "ProfileDataId");

            migrationBuilder.AddForeignKey(
                "FK_Comments_FeedData_ProfileDataId",
                "Comments",
                "ProfileDataId",
                "FeedData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_FeedData_ProfileDataId",
                "Comments");

            migrationBuilder.DropIndex(
                "IX_Comments_ProfileDataId",
                "Comments");

            migrationBuilder.DropColumn(
                "ProfileDataId",
                "Comments");

            migrationBuilder.AddColumn<string>(
                "Username",
                "Comments",
                "nvarchar(max)",
                nullable: true);
        }
    }
}