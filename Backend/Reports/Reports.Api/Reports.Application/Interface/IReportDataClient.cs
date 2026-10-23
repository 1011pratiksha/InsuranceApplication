using Reports.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Interface
{
    public interface IReportDataClient
    {
        Task<List<CustomerReportDto>> GetCustomers(
          ReportFilterDto filter);

        Task<List<PolicyReportDto>> GetPolicies(
            ReportFilterDto filter);

        Task<List<PremiumReportDto>> GetPremiums(
            ReportFilterDto filter);

        Task<List<ClaimReportDto>> GetClaims(
            ReportFilterDto filter);

        Task<List<UnderwritingReportDto>> GetUnderwriting(
            ReportFilterDto filter);

        Task<List<AgentReportDto>> GetAgents(
            ReportFilterDto filter);

        Task<List<FinancialReportDto>> GetFinancial(
            ReportFilterDto filter);

    }
}
