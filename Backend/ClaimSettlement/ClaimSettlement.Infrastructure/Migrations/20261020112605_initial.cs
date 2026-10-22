using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimSettlement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claim_assessment",
                columns: table => new
                {
                    assessment_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    assessed_amount = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    deductible = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    depreciation = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    non_payable = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    net_payable = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_assessment", x => x.assessment_id);
                });

            migrationBuilder.CreateTable(
                name: "claim_master",
                columns: table => new
                {
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    incident_date = table.Column<DateTime>(type: "date", nullable: false),
                    claim_date = table.Column<DateTime>(type: "date", nullable: false),
                    estimated_loss = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    claim_status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_master", x => x.claim_id);
                });

            migrationBuilder.CreateTable(
                name: "claim_settlement",
                columns: table => new
                {
                    settlement_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    approved_amount = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    payment_mode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    transaction_reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    settlement_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    settlement_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_settlement", x => x.settlement_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    marital_status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    mobile_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    annual_income = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    customer_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "policy",
                columns: table => new
                {
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    policy_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    policy_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    sum_insured = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    policy_expiry_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy", x => x.policy_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claim_assessment_claim_id",
                table: "claim_assessment",
                column: "claim_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_claim_master_claim_number",
                table: "claim_master",
                column: "claim_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_claim_settlement_claim_id",
                table: "claim_settlement",
                column: "claim_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_policy_policy_number",
                table: "policy",
                column: "policy_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claim_assessment");

            migrationBuilder.DropTable(
                name: "claim_master");

            migrationBuilder.DropTable(
                name: "claim_settlement");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "policy");
        }
    }
}
