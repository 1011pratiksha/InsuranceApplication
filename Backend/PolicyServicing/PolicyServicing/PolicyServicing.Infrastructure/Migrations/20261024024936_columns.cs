using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyServicing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "details",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "details",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "ServiceRequests");
        }
    }
}
