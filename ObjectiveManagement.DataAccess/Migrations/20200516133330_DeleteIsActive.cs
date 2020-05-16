using Microsoft.EntityFrameworkCore.Migrations;

namespace ObjectiveManagement.DataAccess.Migrations
{
    public partial class DeleteIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Objectives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Objectives",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
