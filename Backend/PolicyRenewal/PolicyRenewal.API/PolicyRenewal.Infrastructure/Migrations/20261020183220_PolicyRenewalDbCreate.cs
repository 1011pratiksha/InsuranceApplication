using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyRenewal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PolicyRenewalDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "policy_renewal",
                columns: table => new
                {
                    renewal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<int>(type: "int", nullable: false),
                    renewal_date = table.Column<DateTime>(type: "date", nullable: false),
                    new_expiry_date = table.Column<DateTime>(type: "date", nullable: false),
                    premium_amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    renewal_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy_renewal", x => x.renewal_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_policy_renewal_policy_id_renewal_date",
                table: "policy_renewal",
                columns: new[] { "policy_id", "renewal_date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "policy_renewal");
        }
    }
}
