using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class PonerIDAulaUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachersClassrooms_ClassroomId",
                table: "TeachersClassrooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "acdeaf4d-29d9-4b18-906a-ca1591e15b54", "AQAAAAIAAYagAAAAEFr+UN5iflWo99vBwJu3NW4lfzGoJdwnZA+GueJdlgv4ibUn92ptMsJ+j2/7fDCz1A==" });

            migrationBuilder.CreateIndex(
                name: "Unique_ClassroomId",
                table: "TeachersClassrooms",
                column: "ClassroomId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Unique_ClassroomId",
                table: "TeachersClassrooms");

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
    }
}
