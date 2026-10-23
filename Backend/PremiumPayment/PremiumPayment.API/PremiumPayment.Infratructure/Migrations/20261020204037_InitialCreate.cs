using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumPayment.Infratructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "policy",
                columns: table => new
                {
                    policy_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    quote_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    effective_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    policy_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy", x => x.policy_id);
                });

            migrationBuilder.CreateTable(
                name: "policy_cancellation",
                columns: table => new
                {
                    cancellation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    cancellation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cancellation_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refund_amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    approved_by = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy_cancellation", x => x.cancellation_id);
                });

            migrationBuilder.CreateTable(
                name: "premium_payment",
                columns: table => new
                {
                    payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule_id = table.Column<long>(type: "bigint", nullable: false),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount_paid = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    payment_mode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transaction_reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    payment_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_payment", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "premium_schedule",
                columns: table => new
                {
                    schedule_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    installment_no = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    premium_amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    late_fee = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValue: 0m),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_schedule", x => x.schedule_id);
                });

            migrationBuilder.CreateTable(
                name: "role_master",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_master", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "user_master",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    branch_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_master", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_receipt",
                columns: table => new
                {
                    receipt_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    receipt_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    receipt_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_receipt", x => x.receipt_id);
                    table.ForeignKey(
                        name: "FK_payment_receipt_premium_payment_payment_id",
                        column: x => x.payment_id,
                        principalTable: "premium_payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "premium_refund",
                columns: table => new
                {
                    refund_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    refund_amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    refund_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    refund_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    approved_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_refund", x => x.refund_id);
                    table.ForeignKey(
                        name: "FK_premium_refund_premium_payment_payment_id",
                        column: x => x.payment_id,
                        principalTable: "premium_payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payment_receipt_payment_id",
                table: "payment_receipt",
                column: "payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_payment_receipt_receipt_number",
                table: "payment_receipt",
                column: "receipt_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_policy_policy_number",
                table: "policy",
                column: "policy_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_premium_payment_policy_id",
                table: "premium_payment",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "idx_premium_payment_schedule_id",
                table: "premium_payment",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_premium_refund_payment_id",
                table: "premium_refund",
                column: "payment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_receipt");

            migrationBuilder.DropTable(
                name: "policy");

            migrationBuilder.DropTable(
                name: "policy_cancellation");

            migrationBuilder.DropTable(
                name: "premium_refund");

            migrationBuilder.DropTable(
                name: "premium_schedule");

            migrationBuilder.DropTable(
                name: "role_master");

            migrationBuilder.DropTable(
                name: "user_master");

            migrationBuilder.DropTable(
                name: "premium_payment");
        }
    }
}
