using Microsoft.EntityFrameworkCore.Migrations;

namespace FourthYearProject.Api.Migrations
{
    public partial class FeedDataHack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "ProfileDataId",
                "Posts",
                "int",
                nullable: true);

            migrationBuilder.CreateTable(
                "FeedData",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>("nvarchar(max)", nullable: true),
                    ProfilePicURL = table.Column<string>("nvarchar(max)", nullable: true),
                    FName = table.Column<string>("nvarchar(max)", nullable: true),
                    LName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_FeedData", x => x.Id); });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Posts_FeedData_ProfileDataId",
                "Posts");

            migrationBuilder.DropTable(
                "FeedData");

            migrationBuilder.DropIndex(
                "IX_Posts_ProfileDataId",
                "Posts");

            migrationBuilder.DropColumn(
                "ProfileDataId",
                "Posts");
        }
    }
}