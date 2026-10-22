using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class ClaimAssessmentRepository : IClaimAssessmentRepository
    {
        AppDbContext context;

        public ClaimAssessmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<AssessmentResponseDto> AddAssessmentAsync(long claimId, CreateAssessmentDto dto)
        {
            var assessment = new ClaimAssessment
            {
                claim_id = claimId,
                assessed_amount = dto.AssessedAmount,
                deductible = dto.Deductible,
                depreciation = dto.Depreciation,
                non_payable = dto.NonPayable,
                net_payable = dto.NetPayable
            };

            await context.ClaimAssessments.AddAsync(assessment);
            await context.SaveChangesAsync();

            return ConvertToDto(assessment);
        }

        public async Task<AssessmentResponseDto?> GetAssessmentByClaimIdAsync(long claimId)
        {
            var assessment = await context.ClaimAssessments
                .FirstOrDefaultAsync(a => a.claim_id == claimId);

            if (assessment == null)
                return null;

            return ConvertToDto(assessment);
        }

        private static AssessmentResponseDto ConvertToDto(ClaimAssessment assessment)
        {
            return new AssessmentResponseDto
            {
                AssessmentId = assessment.assessment_id,
                ClaimId = assessment.claim_id,
                AssessedAmount = assessment.assessed_amount,
                Deductible = assessment.deductible,
                Depreciation = assessment.depreciation,
                NonPayable = assessment.non_payable,
                NetPayable = assessment.net_payable
            };
        }
    }
}
