using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSurveyRemarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "survey_assignment",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "survey_assignment");
        }
    }
}
