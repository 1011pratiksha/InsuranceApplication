using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface IAgentPolicyService
    {

        Task CreateAgentPolicy(int agentId, CreateAgentPolicyDto dto);

        Task<List<AgentPolicyResponseDto>> GetAgentPolicies(int agentId);

        Task<AgentPolicyDetailsDto> GetAgentPolicyById(int agentId, int policyId);

        Task<List<PolicyDTO>> GetAllPolicies();

        Task<List<CustomerDTO>> GetAllCustomers();

    }
}
