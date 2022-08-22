using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class StudentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCouncils",
                columns: table => new
                {
                    StudentCouncilId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCouncils", x => x.StudentCouncilId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCouncils");
        }
    }
}
