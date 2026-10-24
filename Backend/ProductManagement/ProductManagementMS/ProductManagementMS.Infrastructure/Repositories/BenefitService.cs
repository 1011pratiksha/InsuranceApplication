
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class BenefitService : IBenefitService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public BenefitService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddBenefit(int productId, benefit_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Benefit data is required.");

            var product = await db.insurance_product.FindAsync(productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            ValidateBenefit(dto);

            var benefit = mapper.Map<benefit_master>(dto);

            benefit.benefit_id = 0;
            benefit.product_id = productId;
            benefit.benefit_name = dto.benefit_name.Trim();

            await db.benefit_master.AddAsync(benefit);
            await db.SaveChangesAsync();
        }

        public async Task<List<benefit_masterDTO>> FetchBenefitsByProductId(
            int productId)
        {
            bool productExists = await db.insurance_product
                .AnyAsync(x => x.product_id == productId);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");

            var benefits = await db.benefit_master
                .AsNoTracking()
                .Where(x => x.product_id == productId)
                .OrderBy(x => x.benefit_id)
                .ToListAsync();

            return mapper.Map<List<benefit_masterDTO>>(benefits);
        }

        public async Task UpdateBenefit(int benefitId, benefit_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Benefit data is required.");

            var benefit = await db.benefit_master.FindAsync(benefitId);

            if (benefit == null)
                throw new KeyNotFoundException("Benefit not found.");

            ValidateBenefit(dto);

            benefit.benefit_name = dto.benefit_name.Trim();
            benefit.benefit_description = dto.benefit_description;
            benefit.benefit_limit = dto.benefit_limit;

            await db.SaveChangesAsync();
        }

        public async Task DeleteBenefit(int benefitId)
        {
            var benefit = await db.benefit_master.FindAsync(benefitId);

            if (benefit == null)
                throw new KeyNotFoundException("Benefit not found.");

            db.benefit_master.Remove(benefit);
            await db.SaveChangesAsync();
        }

        private static void ValidateBenefit(benefit_masterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.benefit_name))
                throw new ArgumentException("Benefit name is required.");

            if (dto.benefit_name.Trim().Length > 150)
                throw new ArgumentException(
                    "Benefit name cannot exceed 150 characters.");

            if (dto.benefit_limit.HasValue && dto.benefit_limit.Value < 0)
                throw new ArgumentException(
                    "Benefit limit cannot be negative.");
        }
    }
}
