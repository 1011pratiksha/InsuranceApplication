using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimDocumentRepository : IClaimDocumentRepository
    {
        private readonly AppDbContext context;

        public ClaimDocumentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ClaimDocument> AddAsync(ClaimDocument document)
        {
            context.ClaimDocuments.Add(document);
            await context.SaveChangesAsync();
            return document;
        }

        public async Task<List<ClaimDocument>> GetByClaimIdAsync(int claimId)
        {
            return await context.ClaimDocuments
                .AsNoTracking()
                .Where(d => d.ClaimId == claimId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();
        }

        public async Task<ClaimDocument?> GetByIdAsync(int documentId)
        {
            return await context.ClaimDocuments.FirstOrDefaultAsync(d => d.DocumentId == documentId);
        }

        public async Task UpdateAsync(ClaimDocument document)
        {
            context.ClaimDocuments.Update(document);
            await context.SaveChangesAsync();
        }
    }
}