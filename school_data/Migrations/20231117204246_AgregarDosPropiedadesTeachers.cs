using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDosPropiedadesTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_at",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_at",
                table: "Teachers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f430a508-a6ef-4058-8764-d6af030e0c15", "AQAAAAIAAYagAAAAEGgcUgp9Ahd9noKC19bl91p22Nsk+LYkcuzWCAIPssEbMxz1D249ZUpMU6teYgrpsQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_at",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "update_at",
                table: "Teachers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2d52f609-b6ff-48b7-8dd2-dbf9d6254337", "AQAAAAIAAYagAAAAEMF9XMbgp+gRuPP5VQSGwoDKgr3QU7d8niv5wZhwl2n9TfGtqHGycXfjzDvj9Mk+6w==" });
        }
    }
}
