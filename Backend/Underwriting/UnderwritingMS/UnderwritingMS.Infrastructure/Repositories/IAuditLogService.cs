
using UnderwritingMS.Application.DTO;

namespace UnderwritingMS.Application.Interface
{
    public interface IAuditLogService
    {
        Task<List<underwriting_audit_logDTO>> GetAuditLogs(int id);
    }
}
