using AutoMapper;
using Reports.Application.DTO;
using Reports.Application.Interface;
using Reports.Infrastructure.Config;
using Reports.Infrastructure.Data;

namespace Reports.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        private readonly ReportsDataClient client;

        public ReportService( AppDbContext db,IMapper mapper,ReportsDataClient client)
        {
            this.db = db;
            this.mapper = mapper;
            this.client = client;
        }

        public async Task<List<CustomerReportDto>> GetCustomersAsync( ReportFilterDto filter)
        {
            var data = await client.GetCustomers(filter);
            return mapper.Map<List<CustomerReportDto>>(data);
        }

        public async Task<List<PolicyReportDto>> GetPoliciesAsync(ReportFilterDto filter)
        {
            var data = await client.GetPolicies(filter);
            return mapper.Map<List<PolicyReportDto>>(data);
        }

        public async Task<List<PremiumReportDto>> GetPremiumsAsync( ReportFilterDto filter)
        {
            var data = await client.GetPremiums(filter);
            return mapper.Map<List<PremiumReportDto>>(data);
        }

        public async Task<List<ClaimReportDto>> GetClaimsAsync( ReportFilterDto filter)
        {
            var data = await client.GetClaims(filter);
            return mapper.Map<List<ClaimReportDto>>(data);
        }

        public async Task<List<UnderwritingReportDto>> GetUnderwritingAsync(ReportFilterDto filter)
        {
            var data = await client.GetUnderwriting(filter);
            return mapper.Map<List<UnderwritingReportDto>>(data);
        }

        public async Task<List<AgentReportDto>> GetAgentsAsync( ReportFilterDto filter)
        {
            var data = await client.GetAgents(filter);
            return mapper.Map<List<AgentReportDto>>(data);
        }

        public async Task<List<FinancialReportDto>> GetFinancialAsync( ReportFilterDto filter)
        {
            var data = await client.GetFinancial(filter);
            return mapper.Map<List<FinancialReportDto>>(data);
        }
    }
}