using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyServicing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_Customer_CustomerId1",
                table: "CustomerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContact_Customer_CustomerId",
                table: "CustomerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDocument_Customer_CustomerId",
                table: "CustomerDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominee_Customer_CustomerId",
                table: "Nominee");

            migrationBuilder.DropForeignKey(
                name: "FK_NomineeDetails_Policy_policy_id",
                table: "NomineeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCancellations_Policy_policy_id",
                table: "PolicyCancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyRenewals_Policy_policy_id",
                table: "PolicyRenewals");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Customer_customer_id",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Policy_policy_id",
                table: "ServiceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policy",
                table: "Policy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Policy",
                newName: "policies");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_policies",
                table: "policies",
                column: "policy_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_customers_CustomerId1",
                table: "CustomerAddress",
                column: "CustomerId1",
                principalTable: "customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContact_customers_CustomerId",
                table: "CustomerContact",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDocument_customers_CustomerId",
                table: "CustomerDocument",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominee_customers_CustomerId",
                table: "Nominee",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NomineeDetails_policies_policy_id",
                table: "NomineeDetails",
                column: "policy_id",
                principalTable: "policies",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCancellations_policies_policy_id",
                table: "PolicyCancellations",
                column: "policy_id",
                principalTable: "policies",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyRenewals_policies_policy_id",
                table: "PolicyRenewals",
                column: "policy_id",
                principalTable: "policies",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_customers_customer_id",
                table: "ServiceRequests",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_policies_policy_id",
                table: "ServiceRequests",
                column: "policy_id",
                principalTable: "policies",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_customers_CustomerId1",
                table: "CustomerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContact_customers_CustomerId",
                table: "CustomerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDocument_customers_CustomerId",
                table: "CustomerDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominee_customers_CustomerId",
                table: "Nominee");

            migrationBuilder.DropForeignKey(
                name: "FK_NomineeDetails_policies_policy_id",
                table: "NomineeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyCancellations_policies_policy_id",
                table: "PolicyCancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyRenewals_policies_policy_id",
                table: "PolicyRenewals");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_customers_customer_id",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_policies_policy_id",
                table: "ServiceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_policies",
                table: "policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "policies",
                newName: "Policy");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policy",
                table: "Policy",
                column: "policy_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_Customer_CustomerId1",
                table: "CustomerAddress",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContact_Customer_CustomerId",
                table: "CustomerContact",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDocument_Customer_CustomerId",
                table: "CustomerDocument",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominee_Customer_CustomerId",
                table: "Nominee",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NomineeDetails_Policy_policy_id",
                table: "NomineeDetails",
                column: "policy_id",
                principalTable: "Policy",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyCancellations_Policy_policy_id",
                table: "PolicyCancellations",
                column: "policy_id",
                principalTable: "Policy",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyRenewals_Policy_policy_id",
                table: "PolicyRenewals",
                column: "policy_id",
                principalTable: "Policy",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Customer_customer_id",
                table: "ServiceRequests",
                column: "customer_id",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Policy_policy_id",
                table: "ServiceRequests",
                column: "policy_id",
                principalTable: "Policy",
                principalColumn: "policy_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
