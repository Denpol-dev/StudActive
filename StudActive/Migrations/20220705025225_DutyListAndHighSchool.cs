using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class DutyListAndHighSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DutyLists",
                columns: table => new
                {
                    DutyListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDuty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerification = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyLists", x => x.DutyListId);
                });

            migrationBuilder.CreateTable(
                name: "HigherSchools",
                columns: table => new
                {
                    HigherSchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HigherSchools", x => x.HigherSchoolId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DutyLists");

            migrationBuilder.DropTable(
                name: "HigherSchools");
        }
    }
}
