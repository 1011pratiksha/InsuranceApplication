using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimDocumentRepository : IClaimDocumentRepository
    {
        private readonly AppDbContext db;

        public ClaimDocumentRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<ClaimDocument> AddAsync(ClaimDocument document)
        {
            db.ClaimDocuments.Add(document);
            await db.SaveChangesAsync();
            return document;
        }

        public async Task<List<ClaimDocument>> GetByClaimIdAsync(int claimId)
        {
            return await db.ClaimDocuments
                .AsNoTracking()
                .Where(d => d.ClaimId == claimId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();
        }

        public async Task<ClaimDocument?> GetByIdAsync(int documentId)
        {
            return await db.ClaimDocuments
                .FirstOrDefaultAsync(d => d.DocumentId == documentId);
        }

        public async Task UpdateAsync(ClaimDocument document)
        {
            db.ClaimDocuments.Update(document);
            await db.SaveChangesAsync();
        }
    }
}