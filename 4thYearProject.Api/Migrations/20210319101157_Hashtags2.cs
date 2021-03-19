using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class Hashtags2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_Posts_PostId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_PostId",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Hashtags");

            migrationBuilder.CreateTable(
                name: "HashTagPost",
                columns: table => new
                {
                    HashTagsId = table.Column<int>(type: "int", nullable: false),
                    HashTagsPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagPost", x => new { x.HashTagsId, x.HashTagsPostId });
                    table.ForeignKey(
                        name: "FK_HashTagPost_Hashtags_HashTagsId",
                        column: x => x.HashTagsId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashTagPost_Posts_HashTagsPostId",
                        column: x => x.HashTagsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashTagPost_HashTagsPostId",
                table: "HashTagPost",
                column: "HashTagsPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashTagPost");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Hashtags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_PostId",
                table: "Hashtags",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_Posts_PostId",
                table: "Hashtags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
