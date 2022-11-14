using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Lab_4.Migrations
{
    public partial class database_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsCourses_Courses_courseCrs_Id",
                table: "studentsCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentsCourses_courseCrs_Id",
                table: "studentsCourses");

            migrationBuilder.DropColumn(
                name: "courseCrs_Id",
                table: "studentsCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_studentsCourses_Courses_Crs_Id",
                table: "studentsCourses",
                column: "Crs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsCourses_Courses_Crs_Id",
                table: "studentsCourses");

            migrationBuilder.AddColumn<int>(
                name: "courseCrs_Id",
                table: "studentsCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_studentsCourses_courseCrs_Id",
                table: "studentsCourses",
                column: "courseCrs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_studentsCourses_Courses_courseCrs_Id",
                table: "studentsCourses",
                column: "courseCrs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
