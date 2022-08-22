using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class DeleteGroupIdStudActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "StudentStudActives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "StudentStudActives",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
