using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PremiumPayment.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Data;
using PremiumPayment.Application.Interfaces;

namespace PremiumPayment.Infrastructure.Repositories
{
    public class PaymentReceiptService : IPaymentReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentReceiptService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<payment_receipt?> GetByPaymentId(long paymentId)
        {
            return await _context.payment_receipt
                .FirstOrDefaultAsync(x => x.payment_id == paymentId);
        }

        public async Task Add(payment_receipt receipt)
        {
            await _context.payment_receipt.AddAsync(receipt);
            await _context.SaveChangesAsync();
        }

        public async Task Update(payment_receipt receipt)
        {
            _context.payment_receipt.Update(receipt);
            await _context.SaveChangesAsync();
        }
    }
}