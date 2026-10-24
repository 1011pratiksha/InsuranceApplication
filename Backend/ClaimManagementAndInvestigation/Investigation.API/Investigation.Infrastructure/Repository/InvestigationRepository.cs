using Investigation.Application.Interface;
using Investigation.Domain.Entities;
using Investigation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Investigation.Infrastructure.Repository
{
    public class InvestigationRepository : IInvestigationRepository
    {
        private readonly AppDbContext db;

        public InvestigationRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<InvestigationCase> AddAsync(InvestigationCase investigation)
        {
            db.InvestigationCases.Add(investigation);
            await db.SaveChangesAsync();
            return investigation;
        }

        public async Task<InvestigationCase?> GetByIdAsync(int investigationId)
        {
            return await db.InvestigationCases
                .FirstOrDefaultAsync(i => i.InvestigationId == investigationId);
        }

        public async Task UpdateAsync(InvestigationCase investigation)
        {
            db.InvestigationCases.Update(investigation);
            await db.SaveChangesAsync();
        }
    }
}