using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class EditTableUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "CompleteName", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "Administrator", "74817f35-d6c4-4abd-9198-ce54772d50d8", "AQAAAAIAAYagAAAAEGs8zV3IvL4vju3EuoewefF0A+HNEaPwSPAUOaaRVxzzZtRbILiO0wxWFPrfLlGD5Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "CompleteName", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "admin", "59f7574e-3684-477a-bab7-4deec9332aed", "AQAAAAIAAYagAAAAEIM29uLZK19LVgOpRpiMyKsMP8ynKZJ7/1Dj5IADtSFfaCZuSdQ6pVw3LSDnFt3dRA==" });
        }
    }
}
