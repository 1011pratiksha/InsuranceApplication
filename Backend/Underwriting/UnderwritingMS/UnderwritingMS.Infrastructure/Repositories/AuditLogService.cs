
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        AppDbContext db;
        IMapper mapper;

        public AuditLogService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<underwriting_audit_logDTO>> GetAuditLogs(int id)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            var d = await db.underwriting_audit_log
                .Where(x => x.underwriting_id == id)
                .OrderByDescending(x => x.action_date)
                .ToListAsync();

            return mapper.Map<List<underwriting_audit_logDTO>>(d);
        }
    }
}
