using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaEstadiantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "601873e5-eda6-468d-beb1-27c9426d98a9", "AQAAAAIAAYagAAAAELwHP3N4S9o9HG41S/OF5mA9SqLk/4huuhPCeOJGVAQ6XEsjmUTyT6vr3xo6RdNeWw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e47d6d3f-44af-4b46-b60e-195bd6c8db4d", "AQAAAAIAAYagAAAAEFfMrs5CrSFxH0ur+4czkK1noHwBWYR3j1f5xlbFQRcTRQTM4f446cWdyAqvqnZvMw==" });
        }
    }
}
