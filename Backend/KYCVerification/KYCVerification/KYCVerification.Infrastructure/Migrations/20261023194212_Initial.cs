
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KYCVerification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create document type master table.
            migrationBuilder.CreateTable(
                name: "DocumentTypeMasters",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    DocumentName = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false),

                    MandatoryFlag = table.Column<bool>(
                        type: "bit",
                        nullable: false),

                    Status = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_DocumentTypeMasters",
                        x => x.DocumentTypeId);
                });

            // Create CKYC details table.
            // CustomerId references the existing shared Customers table.
            migrationBuilder.CreateTable(
                name: "CkycDetails",
                columns: table => new
                {
                    CkycId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    CustomerId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    CkycNumber = table.Column<string>(
                        type: "nvarchar(30)",
                        maxLength: 30,
                        nullable: true),

                    VerificationDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    CkycStatus = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_CkycDetails",
                        x => x.CkycId);

                    table.ForeignKey(
                        name: "FK_CkycDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create KYC requests table.
            migrationBuilder.CreateTable(
                name: "CustomerKycs",
                columns: table => new
                {
                    KycId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    CustomerId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    KycNumber = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    KycDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    VerificationStatus = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false),

                    Remarks = table.Column<string>(
                        type: "nvarchar(500)",
                        maxLength: 500,
                        nullable: true),

                    VerifiedBy = table.Column<int>(
                        type: "int",
                        nullable: true),

                    VerifiedDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_CustomerKycs",
                        x => x.KycId);

                    table.ForeignKey(
                        name: "FK_CustomerKycs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create uploaded KYC documents table.
            migrationBuilder.CreateTable(
                name: "KycDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    KycId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    DocumentType = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    DocumentNumber = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false),

                    FilePath = table.Column<string>(
                        type: "nvarchar(500)",
                        maxLength: 500,
                        nullable: false),

                    UploadDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    VerificationStatus = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_KycDocuments",
                        x => x.DocumentId);

                    table.ForeignKey(
                        name: "FK_KycDocuments_CustomerKycs_KycId",
                        column: x => x.KycId,
                        principalTable: "CustomerKycs",
                        principalColumn: "KycId",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create KYC audit log table.
            migrationBuilder.CreateTable(
                name: "KycVerificationLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    KycId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    Action = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false),

                    PerformedBy = table.Column<int>(
                        type: "int",
                        nullable: true),

                    ActionDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    Remarks = table.Column<string>(
                        type: "nvarchar(500)",
                        maxLength: 500,
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_KycVerificationLogs",
                        x => x.LogId);

                    table.ForeignKey(
                        name: "FK_KycVerificationLogs_CustomerKycs_KycId",
                        column: x => x.KycId,
                        principalTable: "CustomerKycs",
                        principalColumn: "KycId",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create indexes for the KYC tables.
            migrationBuilder.CreateIndex(
                name: "IX_CkycDetails_CustomerId",
                table: "CkycDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerKycs_CustomerId",
                table: "CustomerKycs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerKycs_KycNumber",
                table: "CustomerKycs",
                column: "KycNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KycDocuments_KycId",
                table: "KycDocuments",
                column: "KycId");

            migrationBuilder.CreateIndex(
                name: "IX_KycVerificationLogs_KycId",
                table: "KycVerificationLogs",
                column: "KycId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop only KYC-specific tables.
            // Never drop the shared Customers table.
            migrationBuilder.DropTable(
                name: "CkycDetails");

            migrationBuilder.DropTable(
                name: "DocumentTypeMasters");

            migrationBuilder.DropTable(
                name: "KycDocuments");

            migrationBuilder.DropTable(
                name: "KycVerificationLogs");

            migrationBuilder.DropTable(
                name: "CustomerKycs");
        }
    }
}
