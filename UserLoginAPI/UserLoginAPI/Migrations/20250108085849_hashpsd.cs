using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserLoginAPI.Migrations
{
    /// <inheritdoc />
    public partial class hashpsd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$LfaOOb/ycH3Sx2btH2emAuWbbbAIaCWM7KYNs.fB3Mmf/akaMbYLa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "admin123");
        }
    }
}
