using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAgent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newone1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Agents",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Agents",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
