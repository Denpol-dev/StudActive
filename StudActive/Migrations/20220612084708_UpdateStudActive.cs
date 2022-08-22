using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class UpdateStudActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VkLink",
                table: "StudentStudActives",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VkLink",
                table: "StudentStudActives");
        }
    }
}
