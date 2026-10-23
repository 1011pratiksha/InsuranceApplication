using AutoMapper;
using Investigation.Application.DTO;
using Investigation.Application.Interface;
using Investigation.Domain.Entities;
using Investigation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Investigation.Infrastructure.Repository
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
            var investigation = mapper.Map<InvestigationCase>(dto);
            investigation.ClaimId = claimId;
            investigation.CreatedDate = DateTime.UtcNow;
            investigation.InvestigationStatus = "Pending";

            if (dto.InvestigatorId != null)
                investigation.AssignedDate = DateTime.UtcNow;

            await db.InvestigationCases.AddAsync(investigation);
            await db.SaveChangesAsync();

            return mapper.Map<InvestigationResponseDto>(investigation);
        }

        public async Task<InvestigationResponseDto?> SubmitInvestigationReportAsync(int id, InvestigationReportDto dto)
        {
            var investigation = await db.InvestigationCases.FirstOrDefaultAsync(i => i.InvestigationId == id);
            if (investigation == null)
                return null;

            mapper.Map(dto, investigation);
            investigation.InvestigationStatus = "Completed";
            investigation.CompletedDate = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return mapper.Map<InvestigationResponseDto>(investigation);
        }
    }
}