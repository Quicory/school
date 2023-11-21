using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f79c0dd-0e5b-4217-b842-cff5b571c9b7", "AQAAAAIAAYagAAAAEMDEw21La6C8C7eYyBumkggK+6pP+OoGMpm42qRButcn4oT4TkONjTO4K633ekzfVw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "314a276d-841a-4175-afc2-7b5bf0527937", "AQAAAAIAAYagAAAAEDP2vpkjfdWh3W/Ld6Jhd1fJlPgraZ86/nHdO5riPZSpZTLw5eEZu6FlR0UjcKbztg==" });
        }
    }
}
