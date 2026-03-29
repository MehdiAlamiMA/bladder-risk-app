using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BladderRiskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalScore",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OccupationalScore",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonalScore",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SmokingScore",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalScore",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "OccupationalScore",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "PersonalScore",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "SmokingScore",
                table: "RiskChecks");
        }
    }
}
