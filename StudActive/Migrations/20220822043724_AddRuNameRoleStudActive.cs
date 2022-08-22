using Microsoft.EntityFrameworkCore.Migrations;

namespace StudActive.Migrations
{
    public partial class AddRuNameRoleStudActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameRU",
                table: "RoleStudActives",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameRU",
                table: "RoleStudActives");
        }
    }
}
