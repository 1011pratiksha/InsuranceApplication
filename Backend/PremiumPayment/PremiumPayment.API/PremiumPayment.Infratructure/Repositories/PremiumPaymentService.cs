    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using PremiumPayment.Domain.Entities;
    using PremiumPayment.Infrastructure.Data;
    using PremiumPayment.Application.Interfaces;




        namespace PremiumPayment.Infrastructure.Repositories
        {
            public class PremiumPaymentService : IPremiumPaymentRepository
            {
                private readonly ApplicationDbContext _context;

                public PremiumPaymentService(ApplicationDbContext context)
                {
                    _context = context;
                }

                public async Task<premium_payment?> GetById(long paymentId)
                {
                    return await _context.premium_payment
                        .Include(x => x.payment_receipt)
                        .Include(x => x.premium_refunds)
                        .FirstOrDefaultAsync(x => x.payment_id == paymentId);
                }

                public async Task<IEnumerable<premium_payment>> GetByPolicyId(long policyId)
                {
                    return await _context.premium_payment
                        .Where(x => x.policy_id == policyId)
                        .OrderByDescending(x => x.payment_date)
                        .ToListAsync();
                }

                public async Task Add(premium_payment payment)
                {
                    await _context.premium_payment.AddAsync(payment);
                    await _context.SaveChangesAsync();
                }

                public async Task Update(premium_payment payment)
                {
                    _context.premium_payment.Update(payment);
                    await _context.SaveChangesAsync();
                }

                public async Task<bool> ExistsByScheduleId(long scheduleId)
                {
                    return await _context.premium_payment.AnyAsync(x =>
                        x.schedule_id == scheduleId &&
                        (x.payment_status == "Success" ||
                         x.payment_status == "Initiated" ||
                         x.payment_status == "Pending"));
                }
            }
        }


