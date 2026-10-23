using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claim_master",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedLoss = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    ClaimStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_master", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CustomerStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "insurance_product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumAge = table.Column<int>(type: "int", nullable: true),
                    MaximumAge = table.Column<int>(type: "int", nullable: true),
                    MinimumSumInsured = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    MaximumSumInsured = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_insurance_product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "claim_assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    AssessedAmount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Deductible = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Depreciation = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    NonPayable = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    NetPayable = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_assessment", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_claim_assessment_claim_master_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_claim_assessment_claim_master_ClaimId1",
                        column: x => x.ClaimId1,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId");
                });

            migrationBuilder.CreateTable(
                name: "claim_documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_claim_documents_claim_master_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_claim_documents_claim_master_ClaimId1",
                        column: x => x.ClaimId1,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId");
                });

            migrationBuilder.CreateTable(
                name: "claim_history",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ClaimId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_history", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_claim_history_claim_master_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_claim_history_claim_master_ClaimId1",
                        column: x => x.ClaimId1,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId");
                });

            migrationBuilder.CreateTable(
                name: "survey_assignment",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    SurveyorId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SurveyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClaimId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey_assignment", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_survey_assignment_claim_master_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_survey_assignment_claim_master_ClaimId1",
                        column: x => x.ClaimId1,
                        principalTable: "claim_master",
                        principalColumn: "ClaimId");
                });

            migrationBuilder.CreateTable(
                name: "policy",
                columns: table => new
                {
                    PolicyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QuoteId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_policy_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_policy_insurance_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "insurance_product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "policy_coverage",
                columns: table => new
                {
                    CoverageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<long>(type: "bigint", nullable: false),
                    CoverageName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SumInsured = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CoverageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy_coverage", x => x.CoverageId);
                    table.ForeignKey(
                        name: "FK_policy_coverage_policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "policy",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "premium_schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<long>(type: "bigint", nullable: false),
                    InstallmentNo = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_premium_schedule_policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "policy",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "premium_payment",
                columns: table => new
                {
                    PaymentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    PolicyId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransactionReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_premium_payment_policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "policy",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_premium_payment_premium_schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "premium_schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claim_assessment_ClaimId",
                table: "claim_assessment",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_claim_assessment_ClaimId1",
                table: "claim_assessment",
                column: "ClaimId1");

            migrationBuilder.CreateIndex(
                name: "IX_claim_documents_ClaimId",
                table: "claim_documents",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_claim_documents_ClaimId1",
                table: "claim_documents",
                column: "ClaimId1");

            migrationBuilder.CreateIndex(
                name: "IX_claim_history_ClaimId",
                table: "claim_history",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_claim_history_ClaimId1",
                table: "claim_history",
                column: "ClaimId1");

            migrationBuilder.CreateIndex(
                name: "IX_claim_master_ClaimNumber",
                table: "claim_master",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_CustomerCode",
                table: "customer",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_Email",
                table: "customer",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_customer_MobileNumber",
                table: "customer",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_insurance_product_ProductCode",
                table: "insurance_product",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_policy_CustomerId",
                table: "policy",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_policy_PolicyNumber",
                table: "policy",
                column: "PolicyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_policy_ProductId",
                table: "policy",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_policy_coverage_PolicyId",
                table: "policy_coverage",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_premium_payment_PolicyId",
                table: "premium_payment",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_premium_payment_ScheduleId",
                table: "premium_payment",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_premium_schedule_PolicyId",
                table: "premium_schedule",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_survey_assignment_ClaimId",
                table: "survey_assignment",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_survey_assignment_ClaimId1",
                table: "survey_assignment",
                column: "ClaimId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claim_assessment");

            migrationBuilder.DropTable(
                name: "claim_documents");

            migrationBuilder.DropTable(
                name: "claim_history");

            migrationBuilder.DropTable(
                name: "policy_coverage");

            migrationBuilder.DropTable(
                name: "premium_payment");

            migrationBuilder.DropTable(
                name: "survey_assignment");

            migrationBuilder.DropTable(
                name: "premium_schedule");

            migrationBuilder.DropTable(
                name: "claim_master");

            migrationBuilder.DropTable(
                name: "policy");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "insurance_product");
        }
    }
}
