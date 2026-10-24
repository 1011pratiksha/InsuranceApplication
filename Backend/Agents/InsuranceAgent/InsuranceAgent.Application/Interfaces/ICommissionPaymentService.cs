using InsuranceAgent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Application.Interfaces
{
    public interface ICommissionPaymentService
    {
        Task ProcessPayout(CommissionPaymentRequestDto dto);

        Task<List<CommissionPaymentResponseDto>> GetPayoutHistory(string? paymentMode, string? status);

        Task<List<CommissionPaymentResponseDto>> GetAgentPayoutHistory(int agentId);

       
        Task  ReconcilePayment(int paymentId, CommissionPaymentReconcileDto dto);
    }
}
