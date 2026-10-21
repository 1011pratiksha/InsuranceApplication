using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyServicing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    policy_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quote_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effective_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    policy_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.policy_id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    AddressType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AlternateMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredContactMode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocument",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocument", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_CustomerDocument_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nominee",
                columns: table => new
                {
                    NomineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    NomineeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominee", x => x.NomineeId);
                    table.ForeignKey(
                        name: "FK_Nominee_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomineeDetails",
                columns: table => new
                {
                    nominee_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    nominee_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    share_percentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    guardian_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomineeDetails", x => x.nominee_id);
                    table.CheckConstraint("CK_NomineeDetails_SharePercentage", "[share_percentage] > 0 AND [share_percentage] <= 100");
                    table.ForeignKey(
                        name: "FK_NomineeDetails_Policy_policy_id",
                        column: x => x.policy_id,
                        principalTable: "Policy",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyCancellations",
                columns: table => new
                {
                    cancellation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    cancellation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cancellation_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refund_amount = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    approved_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCancellations", x => x.cancellation_id);
                    table.ForeignKey(
                        name: "FK_PolicyCancellations_Policy_policy_id",
                        column: x => x.policy_id,
                        principalTable: "Policy",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRenewals",
                columns: table => new
                {
                    renewal_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    renewal_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    new_expiry_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    premium_amount = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    renewal_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRenewals", x => x.renewal_id);
                    table.CheckConstraint("CK_PolicyRenewal_Status", "[renewal_status] IN ('Pending', 'Paid', 'Renewed', 'Expired')");
                    table.ForeignKey(
                        name: "FK_PolicyRenewals_Policy_policy_id",
                        column: x => x.policy_id,
                        principalTable: "Policy",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    request_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    request_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    request_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.request_id);
                    table.CheckConstraint("CK_ServiceRequest_Status", "[status] IN ('Submitted', 'Under Review', 'Approved', 'Rejected', 'Completed', 'Closed')");
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Policy_policy_id",
                        column: x => x.policy_id,
                        principalTable: "Policy",
                        principalColumn: "policy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyEndorsements",
                columns: table => new
                {
                    endorsement_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<long>(type: "bigint", nullable: false),
                    endorsement_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    endorsement_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    previous_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    approval_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyEndorsements", x => x.endorsement_id);
                    table.CheckConstraint("CK_PolicyEndorsement_ApprovalStatus", "[approval_status] IN ('Pending', 'Approved', 'Rejected')");
                    table.ForeignKey(
                        name: "FK_PolicyEndorsements_ServiceRequests_request_id",
                        column: x => x.request_id,
                        principalTable: "ServiceRequests",
                        principalColumn: "request_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequestHistories",
                columns: table => new
                {
                    history_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action_by = table.Column<long>(type: "bigint", nullable: false),
                    action_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestHistories", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestHistories_ServiceRequests_request_id",
                        column: x => x.request_id,
                        principalTable: "ServiceRequests",
                        principalColumn: "request_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId1",
                table: "CustomerAddress",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                table: "CustomerContact",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocument_CustomerId",
                table: "CustomerDocument",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominee_CustomerId",
                table: "Nominee",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NomineeDetails_policy_id",
                table: "NomineeDetails",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCancellations_policy_id",
                table: "PolicyCancellations",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyEndorsements_endorsement_number",
                table: "PolicyEndorsements",
                column: "endorsement_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolicyEndorsements_request_id",
                table: "PolicyEndorsements",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRenewals_policy_id",
                table: "PolicyRenewals",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestHistories_request_id",
                table: "ServiceRequestHistories",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_customer_id",
                table: "ServiceRequests",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_policy_id",
                table: "ServiceRequests",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_request_number",
                table: "ServiceRequests",
                column: "request_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerContact");

            migrationBuilder.DropTable(
                name: "CustomerDocument");

            migrationBuilder.DropTable(
                name: "Nominee");

            migrationBuilder.DropTable(
                name: "NomineeDetails");

            migrationBuilder.DropTable(
                name: "PolicyCancellations");

            migrationBuilder.DropTable(
                name: "PolicyEndorsements");

            migrationBuilder.DropTable(
                name: "PolicyRenewals");

            migrationBuilder.DropTable(
                name: "ServiceRequestHistories");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Policy");
        }
    }
}
