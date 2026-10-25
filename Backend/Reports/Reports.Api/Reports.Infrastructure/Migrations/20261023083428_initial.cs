using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "report_master",
                columns: table => new
                {
                    report_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    report_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_master", x => x.report_id);
                });

            migrationBuilder.CreateTable(
                name: "report_execution",
                columns: table => new
                {
                    execution_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_id = table.Column<long>(type: "bigint", nullable: false),
                    executed_by = table.Column<long>(type: "bigint", nullable: false),
                    execution_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    execution_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_execution", x => x.execution_id);
                    table.ForeignKey(
                        name: "FK_report_execution_report_master_report_id",
                        column: x => x.report_id,
                        principalTable: "report_master",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "report_schedule",
                columns: table => new
                {
                    schedule_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_id = table.Column<long>(type: "bigint", nullable: false),
                    schedule_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    next_run_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    email_to = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_schedule", x => x.schedule_id);
                    table.ForeignKey(
                        name: "FK_report_schedule_report_master_report_id",
                        column: x => x.report_id,
                        principalTable: "report_master",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_report_execution_report_id",
                table: "report_execution",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_schedule_report_id",
                table: "report_schedule",
                column: "report_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report_execution");

            migrationBuilder.DropTable(
                name: "report_schedule");

            migrationBuilder.DropTable(
                name: "report_master");
        }
    }
}
