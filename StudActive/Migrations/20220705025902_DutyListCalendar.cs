using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class DutyListCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DutyListCalendarId",
                table: "DutyLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DutyListCalendars",
                columns: table => new
                {
                    DutyListCalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorFio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyListCalendars", x => x.DutyListCalendarId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DutyListCalendars");

            migrationBuilder.DropColumn(
                name: "DutyListCalendarId",
                table: "DutyLists");
        }
    }
}
