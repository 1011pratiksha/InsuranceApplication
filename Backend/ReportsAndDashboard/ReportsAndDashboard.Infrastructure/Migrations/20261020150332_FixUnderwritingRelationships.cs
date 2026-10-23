using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsAndDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUnderwritingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_underwriting_case_Customers_CustomerId1",
                table: "underwriting_case");

            migrationBuilder.DropForeignKey(
                name: "FK_underwriting_case_InsuranceProducts_ProductId1",
                table: "underwriting_case");

            migrationBuilder.DropIndex(
                name: "IX_underwriting_case_CustomerId1",
                table: "underwriting_case");

            migrationBuilder.DropIndex(
                name: "IX_underwriting_case_ProductId1",
                table: "underwriting_case");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "underwriting_case");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "underwriting_case");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId1",
                table: "underwriting_case",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "underwriting_case",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_CustomerId1",
                table: "underwriting_case",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_ProductId1",
                table: "underwriting_case",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_underwriting_case_Customers_CustomerId1",
                table: "underwriting_case",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_underwriting_case_InsuranceProducts_ProductId1",
                table: "underwriting_case",
                column: "ProductId1",
                principalTable: "InsuranceProducts",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
