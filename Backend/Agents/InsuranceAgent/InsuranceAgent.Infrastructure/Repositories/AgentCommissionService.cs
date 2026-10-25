using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Application.Mapper;
using InsuranceAgent.Domain.Entities;
using InsuranceAgent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Infrastructure.Repositories
{
    public class AgentCommissionService : IAgentCommissionService
    {

         AppDbContext data;
         PolicyClient policyClient;
        PremiumCalculationClient premiumCalculationClient;


        public AgentCommissionService(AppDbContext data, PolicyClient policyClient, PremiumCalculationClient premiumCalculationClient)
        {
            this.data = data;
            this.policyClient = policyClient;
            this.premiumCalculationClient = premiumCalculationClient;
        }


        public async Task CalculateCommission(CalculateCommissionDto dto)
        {
          
            var policy = await policyClient.GetPolicyById(dto.PolicyId);

            if (policy == null)
            {
                throw new Exception("Policy not found.");
            }

            
            var premiumDetails = await premiumCalculationClient.GetPremiumByQuoteId(policy.QuoteId);


            if (premiumDetails == null)
            {
                throw new Exception("Premium calculation not found.");
            }

            decimal premium = premiumDetails.final_premium;

         
            var rule = await data.CommissionRules.FirstOrDefaultAsync(r =>r.ProductId == policy.ProductId && r.IsActive);

            if (rule == null)
            {
                throw new Exception("Commission rule not found.");
            }

            
            decimal amount = premium * rule.CommissionPercentage / 100;

            
            var commission = new AgentCommission
            {
                AgentId = dto.AgentId,
                PolicyId = dto.PolicyId,
                PremiumAmount = premium,
                CommissionPercentage = rule.CommissionPercentage,
                CommissionAmount = amount,
                Status = "Pending",
                EarnedDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            data.AgentCommissions.Add(commission);

            await data.SaveChangesAsync();
            
        }





        public async Task<List<CommissionResponseDto>> GetAllCommissions(string status, int? agentId, DateTime? fromDate, DateTime? toDate)
        {
            var commissions = await data.AgentCommissions
              .Where(c => string.IsNullOrEmpty(status) || c.Status == status)
              .Where(c => agentId == null || c.AgentId == agentId)
              .Where(c => fromDate == null || c.EarnedDate >= fromDate)
              .Where(c => toDate == null || c.EarnedDate <= toDate)
              .ToListAsync();

            return commissions.Select(c => new CommissionResponseDto
            {
                CommissionId = c.CommissionId,
                AgentId = c.AgentId,
                PolicyId = c.PolicyId,
                PremiumAmount = c.PremiumAmount,
                CommissionPercentage = c.CommissionPercentage,
                CommissionAmount = c.CommissionAmount,
                Status = c.Status,
                EarnedDate = c.EarnedDate,
                CreatedAt = c.CreatedAt
            }).ToList();
        }




        public async Task<List<CommissionResponseDto>> GetCommissionsByAgentId(int agentId)
        {
            var commissions = await data.AgentCommissions.Where(c => c.AgentId == agentId).ToListAsync();

            return commissions.Select(c => new CommissionResponseDto
            {
                CommissionId = c.CommissionId,
                AgentId = c.AgentId,
                PolicyId = c.PolicyId,
                PremiumAmount = c.PremiumAmount,
                CommissionPercentage = c.CommissionPercentage,
                CommissionAmount = c.CommissionAmount,
                Status = c.Status,
                EarnedDate = c.EarnedDate,
                CreatedAt = c.CreatedAt
            }).ToList();
        }




        public async Task UpdateCommissionStatus(int commissionId, CommissionStatusUpdateDto dto)
        {
            var commission = await data.AgentCommissions.FirstOrDefaultAsync(c => c.CommissionId == commissionId);

            if (commission == null)
            {
                throw new Exception("Commission not found.");
            }


            if (dto.Status != "Approved" && dto.Status != "Reversed")
            {
                throw new Exception("Invalid status.");
            }


            commission.Status = dto.Status;

            await data.SaveChangesAsync();
        }


        public async Task<List<PolicyDTO>> GetAllPolicies()
        {
            return await policyClient.GetAllPolicies();
        }
    }
}
