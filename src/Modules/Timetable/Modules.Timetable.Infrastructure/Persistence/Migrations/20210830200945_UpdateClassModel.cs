using Microsoft.EntityFrameworkCore.Migrations;

namespace Modules.Timetable.Infrastructure.Persistence.Migrations
{
    public partial class UpdateClassModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Audiences_AudienceId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_AudienceId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TeacherId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "AudienceId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                schema: "Schedule",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "AudienceClass",
                schema: "Schedule",
                columns: table => new
                {
                    AudiencesId = table.Column<int>(type: "integer", nullable: false),
                    ClassesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceClass", x => new { x.AudiencesId, x.ClassesId });
                    table.ForeignKey(
                        name: "FK_AudienceClass_Audiences_AudiencesId",
                        column: x => x.AudiencesId,
                        principalSchema: "Schedule",
                        principalTable: "Audiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudienceClass_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalSchema: "Schedule",
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTeacher",
                schema: "Schedule",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "integer", nullable: false),
                    TeachersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTeacher", x => new { x.ClassesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_ClassTeacher_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalSchema: "Schedule",
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalSchema: "Schedule",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudienceClass_ClassesId",
                schema: "Schedule",
                table: "AudienceClass",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeacher_TeachersId",
                schema: "Schedule",
                table: "ClassTeacher",
                column: "TeachersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudienceClass",
                schema: "Schedule");

            migrationBuilder.DropTable(
                name: "ClassTeacher",
                schema: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "AudienceId",
                schema: "Schedule",
                table: "Classes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                schema: "Schedule",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AudienceId",
                schema: "Schedule",
                table: "Classes",
                column: "AudienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                schema: "Schedule",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Audiences_AudienceId",
                schema: "Schedule",
                table: "Classes",
                column: "AudienceId",
                principalSchema: "Schedule",
                principalTable: "Audiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                schema: "Schedule",
                table: "Classes",
                column: "TeacherId",
                principalSchema: "Schedule",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
