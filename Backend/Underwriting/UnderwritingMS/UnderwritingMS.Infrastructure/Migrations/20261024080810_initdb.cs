using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnderwritingMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "underwriting_case",
                columns: table => new
                {
                    underwriting_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quote_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    underwriting_date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_case", x => x.underwriting_id);
                });

            migrationBuilder.CreateTable(
                name: "financial_underwriting",
                columns: table => new
                {
                    financial_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<int>(type: "int", nullable: false),
                    annual_income = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    monthly_income = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    employer_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    existing_insurance = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    loan_liability = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_underwriting", x => x.financial_id);
                    table.ForeignKey(
                        name: "FK_financial_underwriting_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medical_underwriting",
                columns: table => new
                {
                    medical_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    bmi = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    blood_pressure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    diabetes = table.Column<bool>(type: "bit", nullable: false),
                    heart_disease = table.Column<bool>(type: "bit", nullable: false),
                    cancer_history = table.Column<bool>(type: "bit", nullable: false),
                    smoking = table.Column<bool>(type: "bit", nullable: false),
                    alcohol = table.Column<bool>(type: "bit", nullable: false),
                    medical_history = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medical_report_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_underwriting", x => x.medical_id);
                    table.ForeignKey(
                        name: "FK_medical_underwriting_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "risk_score",
                columns: table => new
                {
                    risk_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    age_score = table.Column<int>(type: "int", nullable: false),
                    underwriting_id = table.Column<int>(type: "int", nullable: false),
                    medical_score = table.Column<int>(type: "int", nullable: false),
                    occupation_score = table.Column<int>(type: "int", nullable: false),
                    lifestyle_score = table.Column<int>(type: "int", nullable: false),
                    financial_score = table.Column<int>(type: "int", nullable: false),
                    total_score = table.Column<int>(type: "int", nullable: false),
                    risk_category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk_score", x => x.risk_id);
                    table.ForeignKey(
                        name: "FK_risk_score_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "underwriting_audit_log",
                columns: table => new
                {
                    audit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    performed_by = table.Column<int>(type: "int", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    action_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_audit_log", x => x.audit_id);
                    table.ForeignKey(
                        name: "FK_underwriting_audit_log_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "underwriting_decision",
                columns: table => new
                {
                    decision_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<int>(type: "int", nullable: false),
                    decision = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    loading_percentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    exclusion_details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rejection_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    approved_by = table.Column<int>(type: "int", nullable: true),
                    decision_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_decision", x => x.decision_id);
                    table.ForeignKey(
                        name: "FK_underwriting_decision_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_financial_underwriting_underwriting_id",
                table: "financial_underwriting",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_underwriting_underwriting_id",
                table: "medical_underwriting",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_score_underwriting_id",
                table: "risk_score",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_audit_log_underwriting_id",
                table: "underwriting_audit_log",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_quote_id",
                table: "underwriting_case",
                column: "quote_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_decision_underwriting_id",
                table: "underwriting_decision",
                column: "underwriting_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_underwriting");

            migrationBuilder.DropTable(
                name: "medical_underwriting");

            migrationBuilder.DropTable(
                name: "risk_score");

            migrationBuilder.DropTable(
                name: "underwriting_audit_log");

            migrationBuilder.DropTable(
                name: "underwriting_decision");

            migrationBuilder.DropTable(
                name: "underwriting_case");
        }
    }
}
