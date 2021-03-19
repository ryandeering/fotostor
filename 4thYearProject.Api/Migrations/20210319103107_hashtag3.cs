using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class hashtag3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTagPost_Posts_HashTagsPostId",
                table: "HashTagPost");

            migrationBuilder.RenameColumn(
                name: "HashTagsPostId",
                table: "HashTagPost",
                newName: "PostsPostId");

            migrationBuilder.RenameIndex(
                name: "IX_HashTagPost_HashTagsPostId",
                table: "HashTagPost",
                newName: "IX_HashTagPost_PostsPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTagPost_Posts_PostsPostId",
                table: "HashTagPost",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTagPost_Posts_PostsPostId",
                table: "HashTagPost");

            migrationBuilder.RenameColumn(
                name: "PostsPostId",
                table: "HashTagPost",
                newName: "HashTagsPostId");

            migrationBuilder.RenameIndex(
                name: "IX_HashTagPost_PostsPostId",
                table: "HashTagPost",
                newName: "IX_HashTagPost_HashTagsPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTagPost_Posts_HashTagsPostId",
                table: "HashTagPost",
                column: "HashTagsPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
