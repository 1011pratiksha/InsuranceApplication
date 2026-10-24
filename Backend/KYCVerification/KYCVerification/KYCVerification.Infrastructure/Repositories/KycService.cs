
using AutoMapper;
using KycVerification.Application.DTO;
using KycVerification.Application.Interface;
using KycVerification.Domain.Entities;
using KycVerification.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace KycVerification.Infrastructure.Repositories
{
    public class KycService : IKycService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;

        public KycService(
            AppDbContext db,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            this.db = db;
            this.mapper = mapper;
            this.env = env;
        }

        // Create a new KYC request.
        public async Task<CustomerKycDto> CreateKycAsync(
            CreateCustomerKycDto dto)
        {
            bool existing = await db.CustomerKycs.AnyAsync(x =>
                x.CustomerId == dto.CustomerId &&
                (x.VerificationStatus == "Pending" ||
                 x.VerificationStatus == "Re-Submitted"));

            if (existing)
                throw new InvalidOperationException(
                    "This customer already has an active KYC request.");

            var kyc = mapper.Map<CustomerKyc>(dto);

            kyc.KycId = 0;
            kyc.KycNumber = $"KYC-{Guid.NewGuid():N}";
            kyc.KycDate = DateTime.UtcNow;
            kyc.VerificationStatus = "Pending";
            kyc.VerifiedBy = null;
            kyc.VerifiedDate = null;

            db.CustomerKycs.Add(kyc);
            await db.SaveChangesAsync();

            await AddLogAsync(
                kyc.KycId, "KYC Created", null, "KYC request created.");

            return mapper.Map<CustomerKycDto>(kyc);
        }

        // Get one KYC request.
        public async Task<CustomerKycDto?> GetKycByIdAsync(int kycId)
        {
            var kyc = await db.CustomerKycs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            return kyc == null ? null : mapper.Map<CustomerKycDto>(kyc);
        }

        // Get all KYC requests.
        public async Task<List<CustomerKycDto>> GetAllKycAsync()
        {
            var records = await db.CustomerKycs
                .AsNoTracking()
                .OrderByDescending(x => x.KycDate)
                .ToListAsync();

            return mapper.Map<List<CustomerKycDto>>(records);
        }

        // Update editable KYC fields without changing verification details.
        public async Task<CustomerKycDto?> UpdateKycAsync(
            int kycId, UpdateCustomerKycDto dto)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            if (kyc == null)
                return null;

            // Do not change the customer after verification has started.
            if (kyc.VerificationStatus == "Verified" ||
                kyc.VerificationStatus == "Rejected")
            {
                throw new InvalidOperationException(
                    "A verified or rejected KYC request cannot be updated.");
            }

            // Check for another active request for the selected customer.
            bool duplicate = await db.CustomerKycs.AnyAsync(x =>
                x.CustomerId == dto.CustomerId &&
                x.KycId != kycId &&
                (x.VerificationStatus == "Pending" ||
                 x.VerificationStatus == "Re-Submitted"));

            if (duplicate)
                throw new InvalidOperationException(
                    "This customer already has another active KYC request.");

            kyc.CustomerId = dto.CustomerId;
            kyc.Remarks = dto.Remarks;

            await db.SaveChangesAsync();

            await AddLogAsync(
                kycId, "KYC Updated", null, "KYC request details updated.");

            return mapper.Map<CustomerKycDto>(kyc);
        }

        // Delete a KYC request and its related records and stored files.
        public async Task<bool> DeleteKycAsync(int kycId)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            if (kyc == null)
                return false;

            // Avoid deleting a completed verification record.
            if (kyc.VerificationStatus == "Verified")
                throw new InvalidOperationException(
                    "A verified KYC request cannot be deleted.");

            var documents = await db.KycDocuments
                .Where(x => x.KycId == kycId)
                .ToListAsync();

            var logs = await db.KycVerificationLogs
                .Where(x => x.KycId == kycId)
                .ToListAsync();

            // Delete stored files belonging to this KYC request.
            foreach (var document in documents)
            {
                if (string.IsNullOrWhiteSpace(document.FilePath))
                    continue;

                // Only allow deletion from the SecureDocuments folder.
                string folder = Path.GetFullPath(
                    Path.Combine(env.ContentRootPath, "SecureDocuments"));

                string fullPath = Path.GetFullPath(
                    Path.Combine(env.ContentRootPath, document.FilePath));

                if (fullPath.StartsWith(
                    folder + Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase) &&
                    File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            // Delete dependent database records before deleting the KYC.
            db.KycDocuments.RemoveRange(documents);
            db.KycVerificationLogs.RemoveRange(logs);
            db.CustomerKycs.Remove(kyc);

            await db.SaveChangesAsync();
            return true;
        }

        // Upload a document for a KYC request.
        public async Task<KycDocumentDto> UploadDocumentAsync(
            UploadKycDocumentDto dto)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == dto.KycId);

            if (kyc == null)
                throw new KeyNotFoundException("KYC record not found.");

            if (kyc.VerificationStatus == "Verified")
                throw new InvalidOperationException(
                    "Documents cannot be uploaded to an already verified KYC.");

            var file = dto.File;

            if (file == null || file.Length == 0)
                throw new ArgumentException("Please select a document.");

            const long maxFileSize = 5L * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new ArgumentException(
                    "Maximum file size allowed is 5 MB.");

            string[] allowedExtensions =
                { ".pdf", ".jpg", ".jpeg", ".png" };

            string extension =
                Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException(
                    "Only PDF, JPG, JPEG and PNG files are allowed.");

            string folder = Path.Combine(
                env.ContentRootPath, "SecureDocuments");

            Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid():N}{extension}";
            string fullPath = Path.Combine(folder, fileName);

            try
            {
                await using (var stream = new FileStream(
                    fullPath, FileMode.CreateNew, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                var document = new KycDocument
                {
                    KycId = dto.KycId,
                    DocumentType = dto.DocumentType,
                    DocumentNumber = dto.DocumentNumber,
                    FilePath = Path.Combine("SecureDocuments", fileName),
                    UploadDate = DateTime.UtcNow,
                    VerificationStatus = "Pending"
                };

                db.KycDocuments.Add(document);
                await db.SaveChangesAsync();

                await AddLogAsync(
                    dto.KycId,
                    "Document Uploaded",
                    null,
                    $"Document uploaded: {dto.DocumentType}");

                return mapper.Map<KycDocumentDto>(document);
            }
            catch
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                throw;
            }
        }

        // List documents belonging to a KYC request.
        public async Task<List<KycDocumentDto>> GetDocumentsAsync(int kycId)
        {
            var documents = await db.KycDocuments
                .AsNoTracking()
                .Where(x => x.KycId == kycId)
                .OrderByDescending(x => x.UploadDate)
                .ToListAsync();

            return mapper.Map<List<KycDocumentDto>>(documents);
        }

        // Approve KYC only when all required documents are verified.
        public async Task<bool> ApproveKycAsync(
            int kycId, ReviewKycDto dto)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            if (kyc == null)
                return false;

            var documents = await db.KycDocuments
                .Where(x => x.KycId == kycId)
                .ToListAsync();

            var mandatoryTypes = await db.DocumentTypeMasters
                .Where(x => x.MandatoryFlag && x.Status == "Active")
                .Select(x => x.DocumentName)
                .ToListAsync();

            foreach (var documentType in mandatoryTypes)
            {
                bool verified = documents.Any(x =>
                    x.DocumentType == documentType &&
                    x.VerificationStatus == "Verified");

                if (!verified)
                    throw new InvalidOperationException(
                        $"{documentType} is mandatory and must be uploaded and verified.");
            }

            if (documents.Any(x => x.VerificationStatus != "Verified"))
                throw new InvalidOperationException(
                    "All uploaded documents must be verified before KYC approval.");

            kyc.VerificationStatus = "Verified";
            kyc.VerifiedDate = DateTime.UtcNow;
            kyc.Remarks = dto.Remarks;

            await db.SaveChangesAsync();

            await AddLogAsync(
                kycId, "KYC Approved", null,
                dto.Remarks ?? "KYC approved successfully.");

            return true;
        }

        // Reject KYC and record the reason.
        public async Task<bool> RejectKycAsync(
            int kycId, ReviewKycDto dto)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            if (kyc == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.Remarks))
                throw new ArgumentException(
                    "Remarks are required when rejecting KYC.");

            kyc.VerificationStatus = "Rejected";
            kyc.Remarks = dto.Remarks;
            kyc.VerifiedDate = DateTime.UtcNow;

            await db.SaveChangesAsync();

            await AddLogAsync(
                kycId, "KYC Rejected", null, dto.Remarks);

            return true;
        }

        // Verify a document.
        public async Task<bool> VerifyDocumentAsync(
            int documentId, ReviewKycDto dto)
        {
            var document = await db.KycDocuments
                .FirstOrDefaultAsync(x => x.DocumentId == documentId);

            if (document == null)
                return false;

            document.VerificationStatus = "Verified";
            await db.SaveChangesAsync();

            await AddLogAsync(
                document.KycId,
                "Document Verified",
                null,
                dto.Remarks ?? "Document verified successfully.");

            return true;
        }

        // Reject a document and require a reason.
        public async Task<bool> RejectDocumentAsync(
            int documentId, ReviewKycDto dto)
        {
            var document = await db.KycDocuments
                .FirstOrDefaultAsync(x => x.DocumentId == documentId);

            if (document == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.Remarks))
                throw new ArgumentException(
                    "Remarks are required when rejecting a document.");

            document.VerificationStatus = "Rejected";
            await db.SaveChangesAsync();

            await AddLogAsync(
                document.KycId, "Document Rejected", null, dto.Remarks);

            return true;
        }

        // Re-upload a rejected document.
        public async Task<KycDocumentDto> ReuploadDocumentAsync(
            int documentId, UploadKycDocumentDto dto)
        {
            var document = await db.KycDocuments
                .FirstOrDefaultAsync(x => x.DocumentId == documentId);

            if (document == null)
                throw new KeyNotFoundException("Document not found.");

            if (document.VerificationStatus != "Rejected")
                throw new InvalidOperationException(
                    "Only rejected documents can be re-uploaded.");

            if (dto.KycId != document.KycId)
                throw new ArgumentException(
                    "The KYC ID does not match the original document.");

            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == document.KycId);

            if (kyc == null)
                throw new KeyNotFoundException("KYC record not found.");

            if (kyc.VerificationStatus == "Verified")
                throw new InvalidOperationException(
                    "Documents cannot be re-uploaded after KYC approval.");

            var file = dto.File;

            if (file == null || file.Length == 0)
                throw new ArgumentException("Please select a document.");

            const long maxFileSize = 5L * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new ArgumentException(
                    "Maximum file size allowed is 5 MB.");

            string[] allowedExtensions =
                { ".pdf", ".jpg", ".jpeg", ".png" };

            string extension =
                Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException(
                    "Only PDF, JPG, JPEG and PNG files are allowed.");

            string folder = Path.Combine(
                env.ContentRootPath, "SecureDocuments");

            Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid():N}{extension}";
            string fullPath = Path.Combine(folder, fileName);

            // Preserve the old file until the database update succeeds.
            string oldRelativePath = document.FilePath;
            string oldFullPath = Path.GetFullPath(
                Path.Combine(env.ContentRootPath, oldRelativePath));

            try
            {
                await using (var stream = new FileStream(
                    fullPath, FileMode.CreateNew, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                document.DocumentType = dto.DocumentType;
                document.DocumentNumber = dto.DocumentNumber;
                document.FilePath = Path.Combine("SecureDocuments", fileName);
                document.UploadDate = DateTime.UtcNow;
                document.VerificationStatus = "Pending";

                await db.SaveChangesAsync();

                await AddLogAsync(
                    document.KycId,
                    "Document Re-uploaded",
                    null,
                    $"Re-uploaded document: {document.DocumentType}");

                // Remove the previous stored file after the DB update.
                string secureFolder = Path.GetFullPath(folder);

                if (oldFullPath.StartsWith(
                    secureFolder + Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase) &&
                    File.Exists(oldFullPath))
                {
                    File.Delete(oldFullPath);
                }

                return mapper.Map<KycDocumentDto>(document);
            }
            catch
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                throw;
            }
        }

        // Return summary counts for the dashboard.
        public async Task<KycDashboardDto> GetDashboardAsync()
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            return new KycDashboardDto
            {
                TotalRequests = await db.CustomerKycs.CountAsync(),

                PendingRequests = await db.CustomerKycs.CountAsync(x =>
                    x.VerificationStatus == "Pending" ||
                    x.VerificationStatus == "Re-Submitted"),

                VerifiedRequests = await db.CustomerKycs.CountAsync(x =>
                    x.VerificationStatus == "Verified"),

                RejectedRequests = await db.CustomerKycs.CountAsync(x =>
                    x.VerificationStatus == "Rejected"),

                CkycSuccessful = await db.CkycDetails.CountAsync(x =>
                    x.CkycStatus == "Verified"),

                CkycFailed = await db.CkycDetails.CountAsync(x =>
                    x.CkycStatus == "Failed"),

                RequestsCreatedToday = await db.CustomerKycs.CountAsync(x =>
                    x.KycDate >= today && x.KycDate < tomorrow)
            };
        }

        // Allow rejected KYC requests to be submitted for review again.
        public async Task<bool> ResubmitKycAsync(int kycId)
        {
            var kyc = await db.CustomerKycs
                .FirstOrDefaultAsync(x => x.KycId == kycId);

            if (kyc == null)
                return false;

            if (kyc.VerificationStatus != "Rejected")
                throw new InvalidOperationException(
                    "Only rejected KYC requests can be resubmitted.");

            bool rejectedDocuments = await db.KycDocuments.AnyAsync(x =>
                x.KycId == kycId &&
                x.VerificationStatus == "Rejected");

            if (rejectedDocuments)
                throw new InvalidOperationException(
                    "Re-upload all rejected documents before resubmitting KYC.");

            kyc.VerificationStatus = "Re-Submitted";
            kyc.VerifiedDate = null;

            await db.SaveChangesAsync();

            await AddLogAsync(
                kycId,
                "KYC Resubmitted",
                null,
                "KYC request resubmitted for review.");

            return true;
        }

        // Store an audit-log entry.
        private async Task AddLogAsync(
            int kycId, string action, int? performedBy, string? remarks)
        {
            var log = new KycVerificationLog
            {
                KycId = kycId,
                Action = action,
                PerformedBy = performedBy,
                ActionDate = DateTime.UtcNow,
                Remarks = remarks
            };

            db.KycVerificationLogs.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
