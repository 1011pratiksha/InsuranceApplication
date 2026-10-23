using PremiumPayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Interfaces
{
    public interface IPremiumRefundRepository
    {
        Task<premium_refund?> GetById(long refundId);

        Task Add(premium_refund refund);

        Task Update(premium_refund refund);
    }
}
