using Microsoft.EntityFrameworkCore.Migrations;

namespace Modules.Timetable.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCourseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "Schedule",
                table: "Courses",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "Schedule",
                table: "Courses");
        }
    }
}
