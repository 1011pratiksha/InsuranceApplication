using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
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
    public class CommissionPaymentService : ICommissionPaymentService
    {

        AppDbContext data;

        public CommissionPaymentService(AppDbContext data)
        {
            this.data = data;
        }


        public async Task<List<CommissionPaymentResponseDto>> GetAgentPayoutHistory(int agentId)
        {
            var payments = await data.AgentCommissionPayments.Where(p => p.AgentId == agentId).ToListAsync();

            return payments.Select(p => new CommissionPaymentResponseDto
            {
                CommissionPaymentId = p.CommissionPaymentId,
                AgentId = p.AgentId,
                PaymentReference = p.PaymentReference,
                TotalAmount = p.TotalAmount,
                PaymentDate = p.PaymentDate,
                PaymentMode = p.PaymentMode,
                TransactionReference = p.TransactionReference,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();
        }


        public async Task<List<CommissionPaymentResponseDto>> GetPayoutHistory(string? paymentMode, string? status)
        {
            var payments = await data.AgentCommissionPayments.ToListAsync();

            if (!string.IsNullOrEmpty(paymentMode))
            {
                payments = payments.Where(p => p.PaymentMode == paymentMode).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                payments = payments.Where(p => p.Status == status).ToList();
            }

            return payments.Select(p => new CommissionPaymentResponseDto
            {
                CommissionPaymentId = p.CommissionPaymentId,
                AgentId = p.AgentId,
                PaymentReference = p.PaymentReference,
                TotalAmount = p.TotalAmount,
                PaymentDate = p.PaymentDate,
                PaymentMode = p.PaymentMode,
                TransactionReference = p.TransactionReference,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();
        }


        public async Task ProcessPayout(CommissionPaymentRequestDto dto)
        {
            var commissions = await data.AgentCommissions.Where(c => c.AgentId == dto.AgentId && c.Status == "Approved").ToListAsync();

            if (commissions.Count == 0)
            {
                throw new Exception("No approved commissions found.");
            }

            var payment = new AgentCommissionPayment();

            payment.AgentId = dto.AgentId;
            payment.PaymentReference = Guid.NewGuid().ToString();
            payment.TotalAmount = commissions.Sum(c => c.CommissionAmount);
            payment.PaymentMode = dto.PaymentMode;
            payment.Status = "Pending";
            payment.CreatedAt = DateTime.Now;

            data.AgentCommissionPayments.Add(payment);

            await data.SaveChangesAsync();
        }




        public async Task ReconcilePayment(int paymentId, CommissionPaymentReconcileDto dto)
        {
            var payment = await data.AgentCommissionPayments.FirstOrDefaultAsync(p => p.CommissionPaymentId == paymentId);

            if (payment == null)
            {
                throw new Exception("Payment not found.");
            }

            payment.TransactionReference = dto.TransactionReference;
            payment.Status = "Success";
            payment.PaymentDate = DateTime.Now;

            await data.SaveChangesAsync();
        }
    }
}
