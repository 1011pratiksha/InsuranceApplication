using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsAndDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnderwritingCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "underwriting_case",
                columns: table => new
                {
                    UnderwritingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnderwritingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerId1 = table.Column<long>(type: "bigint", nullable: false),
                    ProductId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_case", x => x.UnderwritingId);
                    table.ForeignKey(
                        name: "FK_underwriting_case_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_underwriting_case_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_underwriting_case_InsuranceProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_underwriting_case_InsuranceProducts_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "InsuranceProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claim_master_claim_number",
                table: "claim_master",
                column: "claim_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_CustomerId",
                table: "underwriting_case",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_CustomerId1",
                table: "underwriting_case",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_ProductId",
                table: "underwriting_case",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_ProductId1",
                table: "underwriting_case",
                column: "ProductId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "underwriting_case");

            migrationBuilder.DropIndex(
                name: "IX_claim_master_claim_number",
                table: "claim_master");
        }
    }
}
