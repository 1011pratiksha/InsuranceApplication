
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class UnderwritingService : IUnderwritingService
    {
        AppDbContext db;
        IMapper mapper;

        public UnderwritingService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateCase(createUnderwritingDTO dto)
        {
            var duplicate = await db.underwriting_case.AnyAsync(
                x => x.quote_id == dto.quote_id);

            if (duplicate)
                throw new Exception("An underwriting case already exists for this quote.");

            var d = mapper.Map<underwriting_case>(dto);

            d.underwriting_id = 0;
            d.underwriting_date = DateTime.Today;
            d.status = "Pending";

            await db.underwriting_case.AddAsync(d);
            await db.SaveChangesAsync();

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = d.underwriting_id,
                    action = "Case Created",
                    performed_by = null,
                    remarks = "Underwriting case created.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }

        public async Task<List<underwriting_caseDTO>> FetchAllCases()
        {
            var d = await db.underwriting_case.ToListAsync();

            var data = mapper.Map<List<underwriting_caseDTO>>(d);

            return data;
        }

        public async Task<underwriting_caseDTO?> FindCaseById(int id)
        {
            var d = await db.underwriting_case.FindAsync(id);

            if (d == null)
                return null;

            var data = mapper.Map<underwriting_caseDTO>(d);

            return data;
        }

        public async Task UpdateCase(int id, createUnderwritingDTO dto)
        {
            var d = await db.underwriting_case.FindAsync(id);

            if (d == null)
                throw new Exception("Underwriting case not found.");

            if (d.status != "Pending")
                throw new Exception("Only Pending cases can be updated.");

            var duplicate = await db.underwriting_case.AnyAsync(
                x => x.quote_id == dto.quote_id &&
                     x.underwriting_id != id);

            if (duplicate)
                throw new Exception("An underwriting case already exists for this quote.");

            d.quote_id = dto.quote_id;
            d.customer_id = dto.customer_id;
            d.product_id = dto.product_id;

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = d.underwriting_id,
                    action = "Case Updated",
                    performed_by = null,
                    remarks = "Underwriting case details updated.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }
    }
}
