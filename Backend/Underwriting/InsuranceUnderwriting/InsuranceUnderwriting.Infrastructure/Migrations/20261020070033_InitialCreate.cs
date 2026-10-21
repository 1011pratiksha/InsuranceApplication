using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceUnderwriting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employee_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "Insurance_Products",
                columns: table => new
                {
                    product_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance_Products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    quote_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quote_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    premium_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.quote_id);
                    table.ForeignKey(
                        name: "FK_Quotes_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Insurance_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Insurance_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "underwriting_case",
                columns: table => new
                {
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quote_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    underwriting_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_case", x => x.underwriting_id);
                    table.ForeignKey(
                        name: "FK_underwriting_case_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_underwriting_case_Insurance_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Insurance_Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_underwriting_case_Quotes_quote_id",
                        column: x => x.quote_id,
                        principalTable: "Quotes",
                        principalColumn: "quote_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "financial_underwriting",
                columns: table => new
                {
                    financial_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false),
                    annual_income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    employer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    existing_insurance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loan_liability = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_underwriting", x => x.financial_id);
                    table.ForeignKey(
                        name: "FK_financial_underwriting_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_underwriting",
                columns: table => new
                {
                    medical_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false),
                    height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bmi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    smoking = table.Column<bool>(type: "bit", nullable: false),
                    alcohol = table.Column<bool>(type: "bit", nullable: false),
                    medical_history = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medical_report_path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_underwriting", x => x.medical_id);
                    table.ForeignKey(
                        name: "FK_medical_underwriting_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "risk_score",
                columns: table => new
                {
                    risk_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false),
                    medical_score = table.Column<int>(type: "int", nullable: false),
                    occupation_score = table.Column<int>(type: "int", nullable: false),
                    lifestyle_score = table.Column<int>(type: "int", nullable: false),
                    financial_score = table.Column<int>(type: "int", nullable: false),
                    total_score = table.Column<int>(type: "int", nullable: false),
                    risk_category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_risk_score", x => x.risk_id);
                    table.ForeignKey(
                        name: "FK_risk_score_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "underwriting_audit_log",
                columns: table => new
                {
                    audit_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    performed_by = table.Column<long>(type: "bigint", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    action_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_audit_log", x => x.audit_id);
                    table.ForeignKey(
                        name: "FK_underwriting_audit_log_Employees_performed_by",
                        column: x => x.performed_by,
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_underwriting_audit_log_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "underwriting_decision",
                columns: table => new
                {
                    decision_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    underwriting_id = table.Column<long>(type: "bigint", nullable: false),
                    decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loading_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    exclusion_details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rejection_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    approved_by = table.Column<long>(type: "bigint", nullable: false),
                    decision_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_underwriting_decision", x => x.decision_id);
                    table.ForeignKey(
                        name: "FK_underwriting_decision_Employees_approved_by",
                        column: x => x.approved_by,
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_underwriting_decision_underwriting_case_underwriting_id",
                        column: x => x.underwriting_id,
                        principalTable: "underwriting_case",
                        principalColumn: "underwriting_id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Quotes_customer_id",
                table: "Quotes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_product_id",
                table: "Quotes",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_risk_score_underwriting_id",
                table: "risk_score",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_audit_log_performed_by",
                table: "underwriting_audit_log",
                column: "performed_by");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_audit_log_underwriting_id",
                table: "underwriting_audit_log",
                column: "underwriting_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_customer_id",
                table: "underwriting_case",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_product_id",
                table: "underwriting_case",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_case_quote_id",
                table: "underwriting_case",
                column: "quote_id");

            migrationBuilder.CreateIndex(
                name: "IX_underwriting_decision_approved_by",
                table: "underwriting_decision",
                column: "approved_by");

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
                name: "Employees");

            migrationBuilder.DropTable(
                name: "underwriting_case");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Insurance_Products");
        }
    }
}
