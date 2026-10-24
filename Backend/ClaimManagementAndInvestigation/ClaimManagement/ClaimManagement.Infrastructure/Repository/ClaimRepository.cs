using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext db;

        public ClaimRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Claim> AddAsync(Claim claim)
        {
            await db.Claims.AddAsync(claim);
            await db.SaveChangesAsync();

            return claim;
        }

        public async Task<List<Claim>> GetAllAsync()
        {
            return await db.Claims
                .AsNoTracking()
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(long claimId)
        {
            return await db.Claims
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClaimId == claimId);
        }

        public async Task<bool> ExistsAsync(long claimId)
        {
            return await db.Claims
                .AnyAsync(c => c.ClaimId == claimId);
        }

        public async Task UpdateAsync(Claim claim)
        {
            db.Claims.Update(claim);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long claimId)
        {
            var claim = await db.Claims.FindAsync((int)claimId);

            if (claim == null)
                return false;

            db.Claims.Remove(claim);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
