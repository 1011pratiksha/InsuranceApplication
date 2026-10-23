using PremiumPayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Interfaces
{
    public interface IPaymentReceiptRepository
    {
        Task<payment_receipt?> GetByPaymentId(long paymentId);
        Task Add(payment_receipt receipt);
        Task Update(payment_receipt receipt);
    }
}
