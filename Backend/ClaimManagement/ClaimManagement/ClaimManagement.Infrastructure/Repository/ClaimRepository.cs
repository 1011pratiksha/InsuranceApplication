using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext context;

        public ClaimRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Claim> AddAsync(Claim claim)
        {
            await context.Claims.AddAsync(claim);
            await context.SaveChangesAsync();

            return claim;
        }

        public async Task<List<Claim>> GetAllAsync()
        {
            return await context.Claims
                .AsNoTracking()
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(long claimId)
        {
            return await context.Claims
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClaimId == claimId);
        }

        public async Task<bool> ExistsAsync(long claimId)
        {
            return await context.Claims
                .AnyAsync(c => c.ClaimId == claimId);
        }

        public async Task UpdateAsync(Claim claim)
        {
            context.Claims.Update(claim);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(long claimId)
        {
            var claim = await context.Claims.FindAsync((int)claimId);

            if (claim == null)
                return false;

            context.Claims.Remove(claim);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
