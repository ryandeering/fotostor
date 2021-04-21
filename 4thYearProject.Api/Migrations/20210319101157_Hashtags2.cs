using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class Hashtags2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Hashtags_Posts_PostId",
                "Hashtags");

            migrationBuilder.DropIndex(
                "IX_Hashtags_PostId",
                "Hashtags");

            migrationBuilder.DropColumn(
                "PostId",
                "Hashtags");

            migrationBuilder.CreateTable(
                "HashTagPost",
                table => new
                {
                    HashTagsId = table.Column<int>("int", nullable: false),
                    HashTagsPostId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagPost", x => new {x.HashTagsId, x.HashTagsPostId});
                    table.ForeignKey(
                        "FK_HashTagPost_Hashtags_HashTagsId",
                        x => x.HashTagsId,
                        "Hashtags",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_HashTagPost_Posts_HashTagsPostId",
                        x => x.HashTagsPostId,
                        "Posts",
                        "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_HashTagPost_HashTagsPostId",
                "HashTagPost",
                "HashTagsPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HashTagPost");

            migrationBuilder.AddColumn<int>(
                "PostId",
                "Hashtags",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Hashtags_PostId",
                "Hashtags",
                "PostId");

            migrationBuilder.AddForeignKey(
                "FK_Hashtags_Posts_PostId",
                "Hashtags",
                "PostId",
                "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}