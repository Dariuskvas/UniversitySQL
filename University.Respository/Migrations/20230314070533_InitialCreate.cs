using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Respository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departaments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departaments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lectures",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lectures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departamentsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_departaments_departamentsid",
                        column: x => x.departamentsid,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartamentLecture",
                columns: table => new
                {
                    departamentsid = table.Column<int>(type: "int", nullable: false),
                    lecturesid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentLecture", x => new { x.departamentsid, x.lecturesid });
                    table.ForeignKey(
                        name: "FK_DepartamentLecture_departaments_departamentsid",
                        column: x => x.departamentsid,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartamentLecture_lectures_lecturesid",
                        column: x => x.lecturesid,
                        principalTable: "lectures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentLecture_lecturesid",
                table: "DepartamentLecture",
                column: "lecturesid");

            migrationBuilder.CreateIndex(
                name: "IX_students_departamentsid",
                table: "students",
                column: "departamentsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartamentLecture");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "lectures");

            migrationBuilder.DropTable(
                name: "departaments");
        }
    }
}
