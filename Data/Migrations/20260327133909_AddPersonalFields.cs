using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BladderRiskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "RiskChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasFamilyHistory",
                table: "RiskChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "HasFamilyHistory",
                table: "RiskChecks");
        }
    }
}
