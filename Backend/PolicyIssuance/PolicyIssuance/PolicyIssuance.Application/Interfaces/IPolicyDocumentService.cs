using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyIssuance.Application.DTO;

namespace PolicyIssuance.Application.Interfaces
{
    public interface IPolicyDocumentService
    {
        Task<PolicyDocumentDto?> GetPolicyDocumentAsync(int policyId);

        Task<PolicyDocumentDto> GeneratePolicyDocumentAsync(int policyId);

        Task<PolicyDocumentDto> RegeneratePolicyDocumentAsync(int policyId);
    }
}
