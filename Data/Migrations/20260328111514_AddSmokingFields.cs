using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BladderRiskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSmokingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExposedToSecondhandSmoke",
                table: "RiskChecks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SmokingStatus",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExposedToSecondhandSmoke",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "SmokingStatus",
                table: "RiskChecks");
        }
    }
}
