using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace _4thYearProject.Api.Data.Migrations
{
    public partial class Post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
               name: "Posts",
               columns: table => new
               {
                   PostId = table.Column<int>(nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   URL = table.Column<string>(nullable: true),
                   ThumbnailURL = table.Column<string>(nullable: true),
                   UploadDate = table.Column<DateTime>(nullable: false),
                   Caption = table.Column<string>(nullable: true),
                   Likes = table.Column<int>(nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Posts", x => x.PostId);
               });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
