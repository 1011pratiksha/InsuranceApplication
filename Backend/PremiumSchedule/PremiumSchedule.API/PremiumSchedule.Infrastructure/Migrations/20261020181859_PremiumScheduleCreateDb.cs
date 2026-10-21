using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PremiumScheduleCreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PremiumSchedulers",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<int>(type: "int", nullable: false),
                    premium_frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    installment_no = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    premium_amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    late_fee = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumSchedulers", x => x.schedule_id);
                });

            migrationBuilder.CreateTable(
                name: "PremiumReminders",
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
                        name: "FK_PremiumReminders_PremiumSchedulers_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "PremiumSchedulers",
                        principalColumn: "schedule_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PremiumReminders_schedule_id",
                table: "PremiumReminders",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumSchedulers_policy_id_installment_no",
                table: "PremiumSchedulers",
                columns: new[] { "policy_id", "installment_no" },
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
