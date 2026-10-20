using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PremiumScheduleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PremiumSchedulers",
                columns: table => new
                {
                    scheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policyId = table.Column<int>(type: "int", nullable: false),
                    premiumFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    installmentNo = table.Column<int>(type: "int", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    premiumAmount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    lateFee = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumSchedulers", x => x.scheduleId);
                });

            migrationBuilder.CreateTable(
                name: "PremiumReminders",
                columns: table => new
                {
                    reminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    reminderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reminderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumReminders", x => x.reminderId);
                    table.ForeignKey(
                        name: "FK_PremiumReminders_PremiumSchedulers_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "PremiumSchedulers",
                        principalColumn: "scheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PremiumReminders_scheduleId",
                table: "PremiumReminders",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumSchedulers_policyId_installmentNo",
                table: "PremiumSchedulers",
                columns: new[] { "policyId", "installmentNo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PremiumReminders");

            migrationBuilder.DropTable(
                name: "PremiumSchedulers");
        }
    }
}
