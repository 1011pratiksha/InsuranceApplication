using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimManagement.Infrastructure.Repository
{
    public class InvestigationRepository : IInvestigationRepository
    {
        AppDbContext db;
        IMapper mapper;

        public InvestigationRepository(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<InvestigationResponseDto> AssignInvestigatorAsync(int claimId, CreateInvestigationDto dto)
        {
            var investigation = mapper.Map<Investigation>(dto);
            investigation.claim_id = claimId;
            investigation.CreatedDate = DateTime.UtcNow;
            investigation.investigation_status = "Pending";

            if (dto.InvestigatorId != null)
                investigation.AssignedDate = DateTime.UtcNow;

            await db.InvestigationCases.AddAsync(investigation);
            await db.SaveChangesAsync();

            return mapper.Map<InvestigationResponseDto>(investigation);
        }

        public async Task<InvestigationResponseDto?> SubmitInvestigationReportAsync(int id, InvestigationReportDto dto)
        {
            var investigation = await db.InvestigationCases.FirstOrDefaultAsync(i => i.investigation_id == id);
            if (investigation == null)
                return null;

            mapper.Map(dto, investigation);
            investigation.investigation_status = "Completed";
            investigation.CompletedDate = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return mapper.Map<InvestigationResponseDto>(investigation);
        }
    }
}