using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class RepiteMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ffee603e-2bf8-48e2-89bc-59658e645f61", "AQAAAAIAAYagAAAAEIvEJaxeitfkEk3AP+pFnlBsfcLt5myS2tYyDpqsrlBvzZ6HEzZWPMlMQbuHIlNYhg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74817f35-d6c4-4abd-9198-ce54772d50d8", "AQAAAAIAAYagAAAAEGs8zV3IvL4vju3EuoewefF0A+HNEaPwSPAUOaaRVxzzZtRbILiO0wxWFPrfLlGD5Q==" });
        }
    }
}
