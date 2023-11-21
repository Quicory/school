using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIDNumberUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "314a276d-841a-4175-afc2-7b5bf0527937", "AQAAAAIAAYagAAAAEDP2vpkjfdWh3W/Ld6Jhd1fJlPgraZ86/nHdO5riPZSpZTLw5eEZu6FlR0UjcKbztg==" });

            migrationBuilder.CreateIndex(
                name: "Unique_IDNumber",
                table: "Students",
                column: "IDNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Unique_IDNumber",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f9ae802-af5e-49cd-b767-6b12edd839b8", "AQAAAAIAAYagAAAAEDQuj4/RXz5c/MYHTC/Prddp+NWR+nZ6q2A2WRQNvzhZbP9ENRbGyoTXbI8kZ+nlSQ==" });
        }
    }
}
