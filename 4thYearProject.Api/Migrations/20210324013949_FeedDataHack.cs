using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class FeedDataHack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileDataId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedData", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_FeedData_ProfileDataId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "FeedData");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ProfileDataId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ProfileDataId",
                table: "Posts");
        }
    }
}
