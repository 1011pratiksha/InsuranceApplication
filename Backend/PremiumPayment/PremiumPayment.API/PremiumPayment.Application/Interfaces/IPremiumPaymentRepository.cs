using PremiumPayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Interfaces
{
    public interface IPremiumPaymentRepository
    {
        Task<premium_payment?> GetById(long paymentId);

        Task<IEnumerable<premium_payment>> GetByPolicyId(long policyId);

        Task Add(premium_payment payment);

        Task Update(premium_payment payment);

        Task<bool> ExistsByScheduleId(long scheduleId);
    }
}
