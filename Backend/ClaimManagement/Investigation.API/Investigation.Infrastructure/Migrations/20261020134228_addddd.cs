using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "investigation",
                columns: table => new
                {
                    InvestigationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    InvestigatorId = table.Column<int>(type: "int", nullable: true),
                    InvestigationReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReportPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvestigationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investigation", x => x.InvestigationId);
                });

            migrationBuilder.CreateTable(
                name: "investigation_history",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestigationId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investigation_history", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_investigation_history_investigation_InvestigationId",
                        column: x => x.InvestigationId,
                        principalTable: "investigation",
                        principalColumn: "InvestigationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_investigation_history_InvestigationId",
                table: "investigation_history",
                column: "InvestigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "investigation_history");

            migrationBuilder.DropTable(
                name: "investigation");
        }
    }
}
