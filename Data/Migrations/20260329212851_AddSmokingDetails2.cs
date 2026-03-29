using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BladderRiskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSmokingDetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CigarettesPerDay",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SmokingType",
                table: "RiskChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearsSinceQuit",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsSmoked",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CigarettesPerDay",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "SmokingType",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "YearsSinceQuit",
                table: "RiskChecks");

            migrationBuilder.DropColumn(
                name: "YearsSmoked",
                table: "RiskChecks");
        }
    }
}
