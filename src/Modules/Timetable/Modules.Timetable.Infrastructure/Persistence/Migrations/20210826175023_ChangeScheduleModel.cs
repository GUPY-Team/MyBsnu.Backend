using Microsoft.EntityFrameworkCore.Migrations;

namespace Modules.Timetable.Infrastructure.Persistence.Migrations
{
    public partial class ChangeScheduleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Schedules_ScheduleId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TimeTableId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "HalfYear",
                schema: "Schedule",
                table: "Schedules",
                newName: "Semester");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                schema: "Schedule",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Schedules_ScheduleId",
                schema: "Schedule",
                table: "Classes",
                column: "ScheduleId",
                principalSchema: "Schedule",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Schedules_ScheduleId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "Semester",
                schema: "Schedule",
                table: "Schedules",
                newName: "HalfYear");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                schema: "Schedule",
                table: "Classes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TimeTableId",
                schema: "Schedule",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Schedules_ScheduleId",
                schema: "Schedule",
                table: "Classes",
                column: "ScheduleId",
                principalSchema: "Schedule",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
