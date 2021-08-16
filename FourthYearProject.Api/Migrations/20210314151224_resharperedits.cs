using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class resharperedits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Carts",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Carts", x => x.Id); });

            migrationBuilder.CreateTable(
                "Countries",
                table => new
                {
                    CountryId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Countries", x => x.CountryId); });

            migrationBuilder.CreateTable(
                "Followers",
                table => new
                {
                    ID = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Follower_ID = table.Column<string>("nvarchar(max)", nullable: false),
                    Followed_ID = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Followers", x => x.ID); });

            migrationBuilder.CreateTable(
                "JobCategories",
                table => new
                {
                    JobCategoryId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_JobCategories", x => x.JobCategoryId); });

            migrationBuilder.CreateTable(
                "Likes",
                table => new
                {
                    ID = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>("nvarchar(max)", nullable: false),
                    Post_ID = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Likes", x => x.ID); });

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    OrderId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true),
                    DatePlaced = table.Column<DateTime>("datetime2", nullable: true),
                    UserName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserAddress = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCity = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCountry = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Orders", x => x.OrderId); });

            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    PostId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true),
                    PhotoFile = table.Column<string>("nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>("nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>("nvarchar(max)", nullable: true),
                    LicenseEnabled = table.Column<bool>("bit", nullable: false),
                    LicensePrice = table.Column<double>("float", nullable: false),
                    PrintsEnabled = table.Column<bool>("bit", nullable: false),
                    ShirtsEnabled = table.Column<bool>("bit", nullable: false),
                    PostDeleted = table.Column<bool>("bit", nullable: false),
                    Caption = table.Column<string>("nvarchar(150)", maxLength: 150, nullable: false),
                    UploadDate = table.Column<DateTime>("datetime2", nullable: false),
                    Likes = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Posts", x => x.PostId); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>("nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>("nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>("nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>("nvarchar(max)", nullable: true),
                    Email = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "Employees",
                table => new
                {
                    EmployeeId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>("datetime2", nullable: false),
                    Email = table.Column<string>("nvarchar(max)", nullable: false),
                    Street = table.Column<string>("nvarchar(max)", nullable: true),
                    Zip = table.Column<string>("nvarchar(max)", nullable: true),
                    City = table.Column<string>("nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>("int", nullable: false),
                    PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    Smoker = table.Column<bool>("bit", nullable: false),
                    MaritalStatus = table.Column<int>("int", nullable: false),
                    Gender = table.Column<int>("int", nullable: false),
                    Comment = table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                    JoinedDate = table.Column<DateTime>("datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>("datetime2", nullable: true),
                    JobCategoryId = table.Column<int>("int", nullable: false),
                    Latitude = table.Column<double>("float", nullable: false),
                    Longitude = table.Column<double>("float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        "FK_Employees_Countries_CountryId",
                        x => x.CountryId,
                        "Countries",
                        "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Employees_JobCategories_JobCategoryId",
                        x => x.JobCategoryId,
                        "JobCategories",
                        "JobCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>("int", nullable: false),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true),
                    Body = table.Column<string>("nvarchar(max)", nullable: true),
                    Username = table.Column<string>("nvarchar(max)", nullable: true),
                    SubmittedOn = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_Posts_PostId",
                        x => x.PostId,
                        "Posts",
                        "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "LineItems",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>("int", nullable: false),
                    Type = table.Column<string>("nvarchar(max)", nullable: true),
                    Size = table.Column<string>("nvarchar(max)", nullable: true),
                    Price = table.Column<double>("float", nullable: false),
                    Quantity = table.Column<int>("int", nullable: false),
                    OrderId = table.Column<int>("int", nullable: true),
                    ShoppingCartId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                    table.ForeignKey(
                        "FK_LineItems_Carts_ShoppingCartId",
                        x => x.ShoppingCartId,
                        "Carts",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_LineItems_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_LineItems_Posts_PostId",
                        x => x.PostId,
                        "Posts",
                        "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Address",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserAddress = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCity = table.Column<string>("nvarchar(max)", nullable: true),
                    UserCountry = table.Column<string>("nvarchar(max)", nullable: true),
                    UserDataId = table.Column<string>("nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        "FK_Address_Users_UserDataId",
                        x => x.UserDataId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Address_UserDataId",
                "Address",
                "UserDataId");

            migrationBuilder.CreateIndex(
                "IX_Comments_PostId",
                "Comments",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_Employees_CountryId",
                "Employees",
                "CountryId");

            migrationBuilder.CreateIndex(
                "IX_Employees_JobCategoryId",
                "Employees",
                "JobCategoryId");

            migrationBuilder.CreateIndex(
                "IX_LineItems_OrderId",
                "LineItems",
                "OrderId");

            migrationBuilder.CreateIndex(
                "IX_LineItems_PostId",
                "LineItems",
                "PostId");

            migrationBuilder.CreateIndex(
                "IX_LineItems_ShoppingCartId",
                "LineItems",
                "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Address");

            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "Employees");

            migrationBuilder.DropTable(
                "Followers");

            migrationBuilder.DropTable(
                "Likes");

            migrationBuilder.DropTable(
                "LineItems");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Countries");

            migrationBuilder.DropTable(
                "JobCategories");

            migrationBuilder.DropTable(
                "Carts");

            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.DropTable(
                "Posts");
        }
    }
}