
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class CoverageService : ICoverageService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public CoverageService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddCoverage(int productId, coverage_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Coverage data is required.");

            var product = await db.insurance_product.FindAsync(productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            ValidateCoverage(dto);

            var coverage = mapper.Map<coverage_master>(dto);

            coverage.coverage_id = 0;
            coverage.product_id = productId;
            coverage.coverage_name = dto.coverage_name.Trim();

            await db.coverage_master.AddAsync(coverage);
            await db.SaveChangesAsync();
        }

        public async Task<List<coverage_masterDTO>> FetchCoveragesByProductId(
            int productId)
        {
            bool productExists = await db.insurance_product
                .AnyAsync(x => x.product_id == productId);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");

            var coverages = await db.coverage_master
                .AsNoTracking()
                .Where(x => x.product_id == productId)
                .OrderBy(x => x.coverage_id)
                .ToListAsync();

            return mapper.Map<List<coverage_masterDTO>>(coverages);
        }

        public async Task UpdateCoverage(int coverageId, coverage_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Coverage data is required.");

            var coverage = await db.coverage_master.FindAsync(coverageId);

            if (coverage == null)
                throw new KeyNotFoundException("Coverage not found.");

            ValidateCoverage(dto);

            coverage.coverage_name = dto.coverage_name.Trim();
            coverage.minimum_coverage = dto.minimum_coverage;
            coverage.maximum_coverage = dto.maximum_coverage;
            coverage.waiting_period_days = dto.waiting_period_days;
            coverage.deductible_amount = dto.deductible_amount;

            await db.SaveChangesAsync();
        }

        public async Task DeleteCoverage(int coverageId)
        {
            var coverage = await db.coverage_master.FindAsync(coverageId);

            if (coverage == null)
                throw new KeyNotFoundException("Coverage not found.");

            db.coverage_master.Remove(coverage);

            await db.SaveChangesAsync();
        }

        private static void ValidateCoverage(coverage_masterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.coverage_name))
                throw new ArgumentException("Coverage name is required.");

            if (dto.coverage_name.Trim().Length > 150)
                throw new ArgumentException(
                    "Coverage name cannot exceed 150 characters.");

            if (dto.minimum_coverage < 0)
                throw new ArgumentException(
                    "Minimum coverage cannot be negative.");

            if (dto.maximum_coverage < 0)
                throw new ArgumentException(
                    "Maximum coverage cannot be negative.");

            if (dto.minimum_coverage.HasValue &&
                dto.maximum_coverage.HasValue &&
                dto.minimum_coverage.Value > dto.maximum_coverage.Value)
            {
                throw new ArgumentException(
                    "Minimum coverage cannot exceed maximum coverage.");
            }

            if (dto.waiting_period_days < 0)
                throw new ArgumentException(
                    "Waiting period cannot be negative.");

            if (dto.deductible_amount < 0)
                throw new ArgumentException(
                    "Deductible amount cannot be negative.");
        }
    }
}
