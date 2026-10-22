using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyServicing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    approved_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCancellations", x => x.cancellation_id);
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
                });

            //migrationBuilder.CreateTable(
            //    name: "PolicyEndorsements",
            //    columns: table => new
            //    {
            //        endorsement_id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        request_id = table.Column<long>(type: "bigint", nullable: false),
            //        endorsement_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        endorsement_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        previous_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        updated_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        approval_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PolicyEndorsements", x => x.endorsement_id);
            //        table.CheckConstraint("CK_PolicyEndorsement_ApprovalStatus", "[approval_status] IN ('Pending', 'Approved', 'Rejected')");
            //        table.ForeignKey(
            //            name: "FK_PolicyEndorsements_ServiceRequests_request_id",
            //            column: x => x.request_id,
            //            principalTable: "ServiceRequests",
            //            principalColumn: "request_id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

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

            //migrationBuilder.CreateIndex(
            //    name: "IX_PolicyEndorsements_endorsement_number",
            //    table: "PolicyEndorsements",
            //    column: "endorsement_number",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PolicyEndorsements_request_id",
            //    table: "PolicyEndorsements",
            //    column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestHistories_request_id",
                table: "ServiceRequestHistories",
                column: "request_id");

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
                name: "NomineeDetails");

            migrationBuilder.DropTable(
                name: "PolicyCancellations");

            //migrationBuilder.DropTable(
            //    name: "PolicyEndorsements");

            migrationBuilder.DropTable(
                name: "PolicyRenewals");

            migrationBuilder.DropTable(
                name: "ServiceRequestHistories");

            migrationBuilder.DropTable(
                name: "ServiceRequests");
        }
    }
}
