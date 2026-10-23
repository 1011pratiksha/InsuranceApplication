using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Infrastructure.Data;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimEligibilityRepository : IClaimEligibilityRepository
    {
        AppDbContext context;

        public ClaimEligibilityRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<string> CheckEligibilityAsync(CreateClaimDto dto)
        {
            // Incident date validation
            if (dto.IncidentDate == DateTime.MinValue || dto.IncidentDate.Date > DateTime.UtcNow.Date)
                return "Invalid incident date.";

            return await Task.FromResult("");
        }
    }
}