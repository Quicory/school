using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Data.Migrations
{
    /// <inheritdoc />
    public partial class TablaAsignatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "CompleteName", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "Administrator", "5a106a82-c0db-4f34-8e3d-3045f96ccd4e", "AQAAAAIAAYagAAAAEMwmLDz+iWMeW5TGydzoGoCxCaXFhndy01L7MpJm0jZwp53DbnmGE6RfAjVR5fxD+w==" });
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
