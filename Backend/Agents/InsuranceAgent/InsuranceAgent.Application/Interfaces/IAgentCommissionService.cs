using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface IAgentCommissionService
    {
        Task  CalculateCommission(CalculateCommissionDto dto);
        
        Task<List<CommissionResponseDto>> GetAllCommissions(string status, int? agentId,  DateTime? fromDate, DateTime? toDate);
     
        Task<List<CommissionResponseDto>> GetCommissionsByAgentId(int agentId);
      
        Task UpdateCommissionStatus(int commissionId, CommissionStatusUpdateDto dto);
    }
}
