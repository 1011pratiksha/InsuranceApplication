using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumPayment.Infratructure.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "premium_payment",
                schema: "dbo",
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
                schema: "dbo",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<int>(type: "int", nullable: false),
                    premium_frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "payment_receipt",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "premium_payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "premium_refund",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "premium_payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PremiumReminders",
                schema: "dbo",
                columns: table => new
                {
                    reminder_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule_id = table.Column<int>(type: "int", nullable: false),
                    reminder_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reminder_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    delivery_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumReminders", x => x.reminder_id);
                    table.ForeignKey(
                        name: "FK_PremiumReminders_premium_schedule_schedule_id",
                        column: x => x.schedule_id,
                        principalSchema: "dbo",
                        principalTable: "premium_schedule",
                        principalColumn: "schedule_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payment_receipt_payment_id",
                schema: "dbo",
                table: "payment_receipt",
                column: "payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_payment_receipt_receipt_number",
                schema: "dbo",
                table: "payment_receipt",
                column: "receipt_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_premium_payment_policy_id",
                schema: "dbo",
                table: "premium_payment",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "idx_premium_payment_schedule_id",
                schema: "dbo",
                table: "premium_payment",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_premium_refund_payment_id",
                schema: "dbo",
                table: "premium_refund",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_premium_schedule_policy_id_installment_no",
                schema: "dbo",
                table: "premium_schedule",
                columns: new[] { "policy_id", "installment_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PremiumReminders_schedule_id",
                schema: "dbo",
                table: "PremiumReminders",
                column: "schedule_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_receipt",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "premium_refund",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PremiumReminders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "premium_payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "premium_schedule",
                schema: "dbo");
        }
    }
}
