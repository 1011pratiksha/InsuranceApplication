using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        AppDbContext context;
        IMapper mapper;

        public ClaimRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClaimResponseDto> AddClaimAsync(CreateClaimDto dto)
        {
            var claim = mapper.Map<ClaimMaster>(dto);
            claim.claim_number = "CLM-" + Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
            claim.claim_date = DateTime.UtcNow;
            claim.claim_status = "Registered";

            await context.ClaimMasters.AddAsync(claim);
            await context.SaveChangesAsync();

            return mapper.Map<ClaimResponseDto>(claim);
        }

        public async Task<List<ClaimResponseDto>> GetAllClaimsAsync()
        {
            var data = await context.ClaimMasters.OrderByDescending(c => c.claim_date).ToListAsync();
            return mapper.Map<List<ClaimResponseDto>>(data);
        }

        // Returns null if the claim is not found
        public async Task<ClaimResponseDto?> GetClaimByIdAsync(long claimId)
        {
            var claim = await context.ClaimMasters.FirstOrDefaultAsync(c => c.claim_id == claimId);
            if (claim == null)
                return null;

            return mapper.Map<ClaimResponseDto>(claim);
        }

        // Returns "" when updated, otherwise the error message
        public async Task<string> UpdateClaimAsync(long claimId, UpdateClaimDto dto)
        {
            var claim = await context.ClaimMasters.FirstOrDefaultAsync(c => c.claim_id == claimId);
            if (claim == null)
                return "Claim not found.";

            if (claim.claim_status != "Registered")
                return "Only registered claims can be updated.";

            if (dto.IncidentDate == DateTime.MinValue || dto.IncidentDate.Date > DateTime.UtcNow.Date)
                return "Invalid incident date.";

            mapper.Map(dto, claim);
            await context.SaveChangesAsync();

            return "";
        }

        // Returns "" when deleted, otherwise the error message
        public async Task<string> DeleteClaimAsync(long claimId)
        {
            var claim = await context.ClaimMasters.FirstOrDefaultAsync(c => c.claim_id == claimId);
            if (claim == null)
                return "Claim not found.";

            if (claim.claim_status != "Registered")
                return "Only registered claims can be deleted.";

            context.ClaimMasters.Remove(claim);
            await context.SaveChangesAsync();

            return "";
        }
    }
}