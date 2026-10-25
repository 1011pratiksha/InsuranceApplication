using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClaimMgmtProcessDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimMasters",
                columns: table => new
                {
                    claim_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    incident_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    claim_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estimated_loss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    claim_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimMasters", x => x.claim_id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimAssessments",
                columns: table => new
                {
                    assessment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    assessed_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    deductible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    depreciation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    non_payable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    net_payable = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimAssessments", x => x.assessment_id);
                    table.ForeignKey(
                        name: "FK_ClaimAssessments_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimDocuments",
                columns: table => new
                {
                    document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    document_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    verification_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimDocuments", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_ClaimDocuments_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimHistories",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    performed_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    action_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimHistories", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_ClaimHistories_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimSettlements",
                columns: table => new
                {
                    settlement_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    approved_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    settlement_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    settlement_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimSettlements", x => x.settlement_id);
                    table.ForeignKey(
                        name: "FK_ClaimSettlements_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestigationCases",
                columns: table => new
                {
                    investigation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    investigator_id = table.Column<int>(type: "int", nullable: true),
                    investigation_reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    report_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    investigation_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationCases", x => x.investigation_id);
                    table.ForeignKey(
                        name: "FK_InvestigationCases_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAssignments",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_id = table.Column<long>(type: "bigint", nullable: false),
                    surveyor_id = table.Column<int>(type: "int", nullable: false),
                    assigned_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    inspection_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    report_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    survey_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAssignments", x => x.survey_id);
                    table.ForeignKey(
                        name: "FK_SurveyAssignments_ClaimMasters_claim_id",
                        column: x => x.claim_id,
                        principalTable: "ClaimMasters",
                        principalColumn: "claim_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimAssessments_claim_id",
                table: "ClaimAssessments",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDocuments_claim_id",
                table: "ClaimDocuments",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimHistories_claim_id",
                table: "ClaimHistories",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimSettlements_claim_id",
                table: "ClaimSettlements",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationCases_claim_id",
                table: "InvestigationCases",
                column: "claim_id");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAssignments_claim_id",
                table: "SurveyAssignments",
                column: "claim_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimAssessments");

            migrationBuilder.DropTable(
                name: "ClaimDocuments");

            migrationBuilder.DropTable(
                name: "ClaimHistories");

            migrationBuilder.DropTable(
                name: "ClaimSettlements");

            migrationBuilder.DropTable(
                name: "InvestigationCases");

            migrationBuilder.DropTable(
                name: "SurveyAssignments");

            migrationBuilder.DropTable(
                name: "ClaimMasters");
        }
    }
}
