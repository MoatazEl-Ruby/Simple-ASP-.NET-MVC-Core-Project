using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Lab_4.Migrations
{
    public partial class DockerChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentsCourses_Courses_Crs_Id",
                table: "studentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentsCourses_Students_Std_Id",
                table: "studentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentsCourses",
                table: "studentsCourses");

            migrationBuilder.RenameTable(
                name: "studentsCourses",
                newName: "StudentsCourses");

            migrationBuilder.RenameIndex(
                name: "IX_studentsCourses_Std_Id",
                table: "StudentsCourses",
                newName: "IX_StudentsCourses_Std_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses",
                columns: new[] { "Crs_Id", "Std_Id" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Instructor" },
                    { 3, "Student" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name", "Password", "UserName" },
                values: new object[] { 1, 27, null, "admin", "123456", "admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[] { 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Courses_Crs_Id",
                table: "StudentsCourses",
                column: "Crs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Students_Std_Id",
                table: "StudentsCourses",
                column: "Std_Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Courses_Crs_Id",
                table: "StudentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Students_Std_Id",
                table: "StudentsCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsCourses",
                table: "StudentsCourses");

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "StudentsCourses",
                newName: "studentsCourses");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsCourses_Std_Id",
                table: "studentsCourses",
                newName: "IX_studentsCourses_Std_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentsCourses",
                table: "studentsCourses",
                columns: new[] { "Crs_Id", "Std_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_studentsCourses_Courses_Crs_Id",
                table: "studentsCourses",
                column: "Crs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_studentsCourses_Students_Std_Id",
                table: "studentsCourses",
                column: "Std_Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
