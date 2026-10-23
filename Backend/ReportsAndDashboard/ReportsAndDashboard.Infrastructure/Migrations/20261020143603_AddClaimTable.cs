using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsAndDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claim_master",
                columns: table => new
                {
                    claim_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    claim_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    incident_date = table.Column<DateTime>(type: "date", nullable: false),
                    claim_date = table.Column<DateTime>(type: "date", nullable: false),
                    estimated_loss = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    claim_status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claim_master", x => x.claim_id);
                    table.ForeignKey(
                        name: "FK_claim_master_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_claim_master_Policies_policy_id",
                        column: x => x.policy_id,
                        principalTable: "Policies",
                        principalColumn: "PolicyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_claim_master_customer_id",
                table: "claim_master",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_claim_master_policy_id",
                table: "claim_master",
                column: "policy_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claim_master");
        }
    }
}
