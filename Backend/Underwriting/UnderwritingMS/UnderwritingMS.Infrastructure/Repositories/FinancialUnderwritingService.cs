
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class FinancialUnderwritingService : IFinancialUnderwritingService
    {
        AppDbContext db;
        IMapper mapper;

        public FinancialUnderwritingService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddFinancialDetails(
            int id, financial_underwritingDTO dto)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            var existing = await db.financial_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (existing != null)
                throw new Exception("Financial details already exist.");

            if (dto.annual_income < 0 ||
                dto.monthly_income < 0 ||
                dto.existing_insurance < 0 ||
                dto.loan_liability < 0)
                throw new Exception("Financial values cannot be negative.");

            var d = mapper.Map<financial_underwriting>(dto);

            d.financial_id = 0;
            d.underwriting_id = id;

            await db.financial_underwriting.AddAsync(d);

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = "Financial Details Added",
                    remarks = "Financial assessment details recorded.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }

        public async Task<financial_underwritingDTO?> GetFinancialDetails(int id)
        {
            var d = await db.financial_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                return null;

            return mapper.Map<financial_underwritingDTO>(d);
        }

        public async Task UpdateFinancialDetails(
            int id, financial_underwritingDTO dto)
        {
            var d = await db.financial_underwriting
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                throw new Exception("Financial details not found.");

            if (dto.annual_income < 0 ||
                dto.monthly_income < 0 ||
                dto.existing_insurance < 0 ||
                dto.loan_liability < 0)
                throw new Exception("Financial values cannot be negative.");

            d.annual_income = dto.annual_income;
            d.monthly_income = dto.monthly_income;
            d.employer_name = dto.employer_name;
            d.occupation = dto.occupation;
            d.existing_insurance = dto.existing_insurance;
            d.loan_liability = dto.loan_liability;

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = "Financial Details Updated",
                    remarks = "Financial assessment details updated.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }
    }
}
