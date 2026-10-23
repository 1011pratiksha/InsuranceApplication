using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsAndDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPremiumTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_premium_schedule_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
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
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TransactionReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_premium_payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_premium_payment_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_premium_payment_premium_schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "premium_schedule",
                        principalColumn: "ScheduleId");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "premium_payment");

            migrationBuilder.DropTable(
                name: "premium_schedule");
        }
    }
}
