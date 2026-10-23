
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class ProductCommissionService : IProductCommissionService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public ProductCommissionService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddCommission(int productId, product_commissionDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Commission data is required.");

            if (productId <= 0)
                throw new ArgumentException("Product ID must be greater than zero.");

            ValidateCommission(dto);

            var product = await db.insurance_product
                .FindAsync(productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            var exists = await db.product_commission
                .AnyAsync(x => x.product_id == productId);

            if (exists)
                throw new ArgumentException(
                    "Commission already exists for this product.");

            var commission = mapper.Map<product_commission>(dto);

            commission.commission_id = 0;
            commission.product_id = productId;

            await db.product_commission.AddAsync(commission);
            await db.SaveChangesAsync();
        }

        public async Task<product_commissionDTO?> FindCommissionByProductId(
            int productId)
        {
            if (productId <= 0)
                throw new ArgumentException(
                    "Product ID must be greater than zero.");

            var commission = await db.product_commission
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.product_id == productId);

            if (commission == null)
                return null;

            return mapper.Map<product_commissionDTO>(commission);
        }

        public async Task UpdateCommission(
            int commissionId, product_commissionDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Commission data is required.");

            if (commissionId <= 0)
                throw new ArgumentException(
                    "Commission ID must be greater than zero.");

            ValidateCommission(dto);

            var commission = await db.product_commission
                .FindAsync(commissionId);

            if (commission == null)
                throw new KeyNotFoundException("Commission not found.");

            commission.first_year_commission = dto.first_year_commission;
            commission.renewal_commission = dto.renewal_commission;
            commission.branch_commission = dto.branch_commission;

            await db.SaveChangesAsync();
        }

        public async Task DeleteCommission(int commissionId)
        {
            if (commissionId <= 0)
                throw new ArgumentException(
                    "Commission ID must be greater than zero.");

            var commission = await db.product_commission
                .FindAsync(commissionId);

            if (commission == null)
                throw new KeyNotFoundException("Commission not found.");

            db.product_commission.Remove(commission);

            await db.SaveChangesAsync();
        }

        private static void ValidateCommission(product_commissionDTO dto)
        {
            if (dto.first_year_commission < 0 ||
                dto.first_year_commission > 100)
            {
                throw new ArgumentException(
                    "First-year commission must be between 0 and 100.");
            }

            if (dto.renewal_commission < 0 ||
                dto.renewal_commission > 100)
            {
                throw new ArgumentException(
                    "Renewal commission must be between 0 and 100.");
            }

            if (dto.branch_commission < 0 ||
                dto.branch_commission > 100)
            {
                throw new ArgumentException(
                    "Branch commission must be between 0 and 100.");
            }
        }
    }
}
