using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BladderRiskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeAndOtherFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SmokingStatus",
                table: "RiskChecks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "RiskChecks");

            migrationBuilder.AlterColumn<int>(
                name: "SmokingStatus",
                table: "RiskChecks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
