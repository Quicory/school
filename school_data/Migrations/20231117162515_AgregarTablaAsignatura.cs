using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaAsignatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2daa96d2-4d3a-4353-8ab0-8a5b450999eb", "AQAAAAIAAYagAAAAEIAfAsnHgVvvHIlOCa1B/kJbQQgvx5IKD5at5N9N+65acKAQvhNAHxKiAIRxaHscsw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74817f35-d6c4-4abd-9198-ce54772d50d8", "AQAAAAIAAYagAAAAEGs8zV3IvL4vju3EuoewefF0A+HNEaPwSPAUOaaRVxzzZtRbILiO0wxWFPrfLlGD5Q==" });
        }
    }
}
