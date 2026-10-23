
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class UnderwritingDecisionService : IUnderwritingDecisionService
    {
        AppDbContext db;
        IMapper mapper;

        public UnderwritingDecisionService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task ApproveCase(
            int id, createUnderwritingDecisionDTO dto)
        {
            await SaveDecision(id, dto, "Approved");
        }

        public async Task RejectCase(
            int id, createUnderwritingDecisionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.rejection_reason))
                throw new Exception("Rejection reason is required.");

            await SaveDecision(id, dto, "Rejected");
        }

        public async Task ApplyLoading(
            int id, createUnderwritingDecisionDTO dto)
        {
            if (dto.loading_percentage < 0 ||
                dto.loading_percentage > 100)
                throw new Exception("Loading percentage must be between 0 and 100.");

            await SaveDecision(id, dto, "Approved with Loading");
        }

        private async Task SaveDecision(
            int id,
            createUnderwritingDecisionDTO dto,
            string decision)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            if (underwriting.status == "Approved" ||
                underwriting.status == "Rejected")
                throw new Exception("A final decision already exists for this case.");

            if (decision == "Approved with Loading" &&
                dto.loading_percentage <= 0)
                throw new Exception("Loading percentage must be greater than zero.");

            if (decision == "Approved with Exclusions" &&
                string.IsNullOrWhiteSpace(dto.exclusion_details))
                throw new Exception("Exclusion details are required.");

            var existing = await db.underwriting_decision
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            var d = existing ?? new underwriting_decision();

            d.underwriting_id = id;
            d.decision = decision;
            d.loading_percentage =
                decision == "Approved with Loading"
                    ? dto.loading_percentage : 0;
            d.exclusion_details =
                decision == "Approved with Exclusions"
                    ? dto.exclusion_details : null;
            d.rejection_reason =
                decision == "Rejected"
                    ? dto.rejection_reason : null;
            d.approved_by = dto.approved_by;
            d.decision_date = DateTime.Now;

            if (existing == null)
                await db.underwriting_decision.AddAsync(d);

            underwriting.status = decision == "Rejected"
                ? "Rejected"
                : "Approved";

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = decision,
                    performed_by = dto.approved_by,
                    remarks = dto.rejection_reason
                              ?? dto.exclusion_details
                              ?? $"Loading: {d.loading_percentage}%",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }

        public async Task<underwriting_decisionDTO?> GetDecision(int id)
        {
            var d = await db.underwriting_decision
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                return null;

            return mapper.Map<underwriting_decisionDTO>(d);
        }
    }
}
