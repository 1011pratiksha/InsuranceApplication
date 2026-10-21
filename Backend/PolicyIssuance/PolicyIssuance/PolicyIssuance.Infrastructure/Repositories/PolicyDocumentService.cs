using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Application.Interfaces;
using PolicyIssuance.Domain.Entities;
using PolicyIssuance.Infrastructure.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyIssuance.Infrastructure.Repositories
{
    public class PolicyDocumentService : IPolicyDocumentService
    {
        AppDbContext db;
        IMapper mapper;

        public PolicyDocumentService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<PolicyDocumentDto?> GetPolicyDocumentAsync(int policyId)
        {
            var document = await db.PolicyDocuments
                .Where(d => d.PolicyId == policyId)
                .OrderByDescending(d => d.VersionNo)
                .FirstOrDefaultAsync();

            if (document == null)
            {
                return null;
            }

            var dto = mapper.Map<PolicyDocumentDto>(document);

            if (File.Exists(document.FilePath))
            {
                dto.FileBytes = await File.ReadAllBytesAsync(document.FilePath);
            }
            else
            {
                var fallbackContent = GenerateDocumentContent(policyId, document.VersionNo);
                dto.FileBytes = Encoding.UTF8.GetBytes(fallbackContent);
            }

            return dto;
        }

        public async Task<PolicyDocumentDto> GeneratePolicyDocumentAsync(int policyId)
        {
            var policy = await db.Policies
                .Include(p => p.PolicyCoverages)
                .Include(p => p.PolicyRiders)
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);

            if (policy == null)
            {
                throw new Exception("Policy not found");
            }

            var existingDoc = await db.PolicyDocuments
                .Where(d => d.PolicyId == policyId)
                .OrderByDescending(d => d.VersionNo)
                .FirstOrDefaultAsync();

            if (existingDoc != null)
            {
                return (await GetPolicyDocumentAsync(policyId))!;
            }

            return await CreateAndSaveDocumentAsync(policy, 1, "DOCUMENT_GENERATED");
        }

        public async Task<PolicyDocumentDto> RegeneratePolicyDocumentAsync(int policyId)
        {
            var policy = await db.Policies
                .Include(p => p.PolicyCoverages)
                .Include(p => p.PolicyRiders)
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);

            if (policy == null)
            {
                throw new Exception("Policy not found");
            }

            var latestDoc = await db.PolicyDocuments
                .Where(d => d.PolicyId == policyId)
                .OrderByDescending(d => d.VersionNo)
                .FirstOrDefaultAsync();

            int nextVersion = (latestDoc?.VersionNo ?? 0) + 1;
            return await CreateAndSaveDocumentAsync(policy, nextVersion, "DOCUMENT_REGENERATED");
        }

        private async Task<PolicyDocumentDto> CreateAndSaveDocumentAsync(Policy policy, int versionNo, string action)
        {
            var documentsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage", "Documents");
            if (!Directory.Exists(documentsDir))
            {
                Directory.CreateDirectory(documentsDir);
            }

            var fileName = $"{policy.PolicyNumber}_v{versionNo}.txt";
            var filePath = Path.Combine(documentsDir, fileName);

            var content = BuildDocumentContent(policy, versionNo);
            var fileBytes = Encoding.UTF8.GetBytes(content);
            await File.WriteAllBytesAsync(filePath, fileBytes);

            var docRecord = new PolicyDocument
            {
                PolicyId = policy.PolicyId,
                DocumentName = fileName,
                FilePath = filePath,
                VersionNo = versionNo,
                GeneratedDate = DateTime.UtcNow
            };

            db.PolicyDocuments.Add(docRecord);

            db.PolicyHistories.Add(new PolicyHistory
            {
                PolicyId = policy.PolicyId,
                Action = action,
                ActionDate = DateTime.UtcNow,
                Remarks = $"Policy schedule document version {versionNo} generated ({fileName})"
            });

            await db.SaveChangesAsync();

            var dto = mapper.Map<PolicyDocumentDto>(docRecord);
            dto.FileBytes = fileBytes;
            return dto;
        }

        private string BuildDocumentContent(Policy policy, int versionNo)
        {
            return $"""
                Policy Number: {policy.PolicyNumber}
                Version: {versionNo}
                Status: {policy.PolicyStatus}
                Customer ID: {policy.CustomerId}
                Effective Date: {policy.EffectiveDate:yyyy-MM-dd}
                Expiry Date: {policy.ExpiryDate:yyyy-MM-dd}
                Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC
                """;
        }

        private string GenerateDocumentContent(int policyId, int versionNo)
        {
            return $"Policy Schedule for Policy ID: {policyId}, Version: v{versionNo}, Timestamp: {DateTime.UtcNow:O}";
        }
    }
}
