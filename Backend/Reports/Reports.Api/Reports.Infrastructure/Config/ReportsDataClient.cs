using Reports.Application.DTO;
using Reports.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Infrastructure.Config
{
    public class ReportsDataClient : IReportDataClient
    {
            CustomerClient customerClient;
            PolicyClient policyClient;
            PremiumClient premiumClient;
            ClaimsClient claimsClient;
            UnderwritingClient underwritingClient;
            AgentClient agentClient;
            FinanceClient financeClient;

        public ReportsDataClient(
            CustomerClient customerClient,
            PolicyClient policyClient,
            PremiumClient premiumClient,
            ClaimsClient claimsClient,
            UnderwritingClient underwritingClient,
            AgentClient agentClient,
            FinanceClient financeClient)
        {
            this.customerClient = customerClient;
            this.policyClient = policyClient;
            this.premiumClient = premiumClient;
            this.claimsClient = claimsClient;
            this.underwritingClient = underwritingClient;
            this.agentClient = agentClient;
            this.financeClient = financeClient;
        }

        public Task<List<CustomerReportDto>> GetCustomers(
            ReportFilterDto filter) =>
            customerClient.GetCustomers(filter);

        public Task<List<PolicyReportDto>> GetPolicies(
            ReportFilterDto filter) =>
            policyClient.GetPolicies(filter);

        public Task<List<PremiumReportDto>> GetPremiums(
            ReportFilterDto filter) =>
            premiumClient.GetPremiums(filter);

        public Task<List<ClaimReportDto>> GetClaims(
            ReportFilterDto filter) =>
            claimsClient.GetClaims(filter);

        public Task<List<UnderwritingReportDto>> GetUnderwriting(
            ReportFilterDto filter) =>
            underwritingClient.GetUnderwriting(filter);

        public Task<List<AgentReportDto>> GetAgents(
            ReportFilterDto filter) =>
            agentClient.GetAgents(filter);

        public Task<List<FinancialReportDto>> GetFinancial(
            ReportFilterDto filter) =>
            financeClient.GetFinancial(filter);
    }
}

