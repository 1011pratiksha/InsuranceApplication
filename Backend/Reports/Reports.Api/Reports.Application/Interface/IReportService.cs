using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Interface
{
    public interface IReportService
    {
        Task<List<CustomerReportDto>> GetCustomersAsync(
           ReportFilterDto filter);

        Task<List<PolicyReportDto>> GetPoliciesAsync(
            ReportFilterDto filter);

        Task<List<PremiumReportDto>> GetPremiumsAsync(
            ReportFilterDto filter);

        Task<List<ClaimReportDto>> GetClaimsAsync(
            ReportFilterDto filter);

        Task<List<UnderwritingReportDto>> GetUnderwritingAsync(
            ReportFilterDto filter);

        Task<List<AgentReportDto>> GetAgentsAsync(
            ReportFilterDto filter);

        Task<List<FinancialReportDto>> GetFinancialAsync(
            ReportFilterDto filter);
    }
}
