using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _4thYearProject.Api.Migrations
{
    public partial class RemovingDefaultClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Employees");

            migrationBuilder.DropTable(
                "Countries");

            migrationBuilder.DropTable(
                "JobCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                "JobCategories",
                table => new
                {
                    JobCategoryId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_JobCategories", x => x.JobCategoryId); });

            migrationBuilder.CreateTable(
                "Employees",
                table => new
                {
                    EmployeeId = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>("datetime2", nullable: false),
                    City = table.Column<string>("nvarchar(max)", nullable: true),
                    Comment = table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                    CountryId = table.Column<int>("int", nullable: false),
                    Email = table.Column<string>("nvarchar(max)", nullable: false),
                    ExitDate = table.Column<DateTime>("datetime2", nullable: true),
                    FirstName = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>("int", nullable: false),
                    JobCategoryId = table.Column<int>("int", nullable: false),
                    JoinedDate = table.Column<DateTime>("datetime2", nullable: true),
                    LastName = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<double>("float", nullable: false),
                    Longitude = table.Column<double>("float", nullable: false),
                    MaritalStatus = table.Column<int>("int", nullable: false),
                    PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    Smoker = table.Column<bool>("bit", nullable: false),
                    Street = table.Column<string>("nvarchar(max)", nullable: true),
                    Zip = table.Column<string>("nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                "IX_Employees_CountryId",
                "Employees",
                "CountryId");

            migrationBuilder.CreateIndex(
                "IX_Employees_JobCategoryId",
                "Employees",
                "JobCategoryId");
        }
    }
}