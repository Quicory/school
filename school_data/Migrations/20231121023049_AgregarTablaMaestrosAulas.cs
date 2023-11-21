using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaMaestrosAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeachersClassrooms",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersClassrooms", x => new { x.TeacherId, x.ClassroomId });
                    table.ForeignKey(
                        name: "FK_TeachersClassrooms_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersClassrooms_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d05e448d-e498-47eb-9466-d1cabe0181b0", "AQAAAAIAAYagAAAAEL+NDT20PIJYjxc8ETaQi8NLpoHExddtwmXY/i+Ao2S2WSGYm2zR9g2kFGwMnLgSYg==" });

            migrationBuilder.CreateIndex(
                name: "IX_TeachersClassrooms_ClassroomId",
                table: "TeachersClassrooms",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachersClassrooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f79c0dd-0e5b-4217-b842-cff5b571c9b7", "AQAAAAIAAYagAAAAEMDEw21La6C8C7eYyBumkggK+6pP+OoGMpm42qRButcn4oT4TkONjTO4K633ekzfVw==" });
        }
    }
}
