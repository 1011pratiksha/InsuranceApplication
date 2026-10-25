using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface ICommissionRuleService
    {
        Task  CreateCommissionRule(CreateCommissionRuleDto dto);

        Task<List<CommissionRuleResponseDto>> GetAllCommissionRules();

        Task<CommissionRuleResponseDto> GetCommissionRuleByProductId(int productId);

        Task UpdateCommissionRule(int ruleId, UpdateCommissionRuleDto dto);
        Task<List<ProductDTO>> GetAllProducts();
    }
}
