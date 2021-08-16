using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class hashtag3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HashTagPost_Posts_HashTagsPostId",
                "HashTagPost");

            migrationBuilder.RenameColumn(
                "HashTagsPostId",
                "HashTagPost",
                "PostsPostId");

            migrationBuilder.RenameIndex(
                "IX_HashTagPost_HashTagsPostId",
                table: "HashTagPost",
                newName: "IX_HashTagPost_PostsPostId");

            migrationBuilder.AddForeignKey(
                "FK_HashTagPost_Posts_PostsPostId",
                "HashTagPost",
                "PostsPostId",
                "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HashTagPost_Posts_PostsPostId",
                "HashTagPost");

            migrationBuilder.RenameColumn(
                "PostsPostId",
                "HashTagPost",
                "HashTagsPostId");

            migrationBuilder.RenameIndex(
                "IX_HashTagPost_PostsPostId",
                table: "HashTagPost",
                newName: "IX_HashTagPost_HashTagsPostId");

            migrationBuilder.AddForeignKey(
                "FK_HashTagPost_Posts_HashTagsPostId",
                "HashTagPost",
                "HashTagsPostId",
                "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}