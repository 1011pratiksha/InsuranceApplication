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
    public class AgentPolicyService : IAgentPolicyService
    {

        AppDbContext data;
        PolicyClient policyClient;
        CustomerClient customerClient;

        public AgentPolicyService(AppDbContext data, PolicyClient policyClient, CustomerClient customerClient)
        {
            this.data = data;
            this.policyClient = policyClient;   
            this.customerClient = customerClient;
        }

        public async Task CreateAgentPolicy(int agentId, CreateAgentPolicyDto dto)
        {
            
            var agent = await data.Agents.FirstOrDefaultAsync(a => a.AgentId == agentId);

            if (agent == null)
            {
                throw new Exception("Agent not found.");
            }

            
            var customer = await customerClient.GetCustomerById(dto.CustomerId);


            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }


            var policy = await policyClient.GetPolicyById(dto.PolicyId);


            if (policy == null)
            {
                throw new Exception("Policy not found.");
            }

          
            var agentPolicy = new AgentPolicy
            {
                AgentId = agentId,
                PolicyId = dto.PolicyId,
                CustomerId = dto.CustomerId,
                AssignedDate = DateTime.Now,
                SaleDate = dto.SaleDate,
                Status = dto.SaleDate.HasValue ? "Sold" : "Assigned",
                CreatedAt = DateTime.Now
            };


            await data.AgentPolicies.AddAsync(agentPolicy);
            await data.SaveChangesAsync();
        }



        public async Task<List<AgentPolicyResponseDto>> GetAgentPolicies(int agentId)
        {
            var agent = await data.Agents.AnyAsync(a => a.AgentId == agentId);

            if (!agent)
            {
                throw new Exception("Agent not found.");
            }

            var policies = await data.AgentPolicies.Where(ap => ap.AgentId == agentId)
                .Select(ap => new AgentPolicyResponseDto
                {
                    AgentPolicyId = ap.AgentPolicyId,
                    AgentId = ap.AgentId,
                    PolicyId = ap.PolicyId,
                    CustomerId = ap.CustomerId,
                    AssignedDate = ap.AssignedDate,
                    SaleDate = ap.SaleDate,
                    Status = ap.Status,
                    CreatedAt = ap.CreatedAt
                }).ToListAsync();

            return policies;
        }



        public async Task<AgentPolicyDetailsDto> GetAgentPolicyById(
            int agentId, int policyId)
        {
            var agent = await data.Agents.AnyAsync(a => a.AgentId == agentId);

            if (!agent)
            {
                throw new Exception("Agent not found.");
            }

            var policy = await data.AgentPolicies.Where(ap => ap.AgentId == agentId && ap.PolicyId == policyId)
                .Select(ap => new AgentPolicyDetailsDto
                {
                    AgentPolicyId = ap.AgentPolicyId,
                    AgentId = ap.AgentId,
                    PolicyId = ap.PolicyId,
                    CustomerId = ap.CustomerId,
                    AssignedDate = ap.AssignedDate,
                    SaleDate = ap.SaleDate,
                    Status = ap.Status,
                    CreatedAt = ap.CreatedAt
                }).FirstOrDefaultAsync();

            if (policy == null)
            {
                throw new Exception("Agent policy not found.");
            }

            return policy;
        }
    }
}
