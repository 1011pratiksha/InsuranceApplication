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
        public class PremiumRefundService : IPremiumRefundRepository
        {
            private readonly ApplicationDbContext _context;

            public PremiumRefundService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<premium_refund?> GetById(long refundId)
            {
                return await _context.premium_refund
                    .Include(x => x.premium_payment)
                    .FirstOrDefaultAsync(x => x.refund_id == refundId);
            }

            public async Task Add(premium_refund refund)
            {
                await _context.premium_refund.AddAsync(refund);
                await _context.SaveChangesAsync();
            }

            public async Task Update(premium_refund refund)
            {
                _context.premium_refund.Update(refund);
                await _context.SaveChangesAsync();
            }
        }
    }



