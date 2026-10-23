
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class PremiumFrequencyService : IPremiumFrequencyService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        private static readonly string[] ValidFrequencies =
        {
            "Monthly",
            "Quarterly",
            "Half-Yearly",
            "Yearly",
            "Single Premium"
        };

        public PremiumFrequencyService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddFrequency(premium_frequencyDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Premium frequency data is required.");

            string name = NormalizeFrequencyName(dto.frequency_name);

            if (!ValidFrequencies.Contains(name))
                throw new ArgumentException("Invalid premium frequency.");

            if (dto.installment_count <= 0)
                throw new ArgumentException(
                    "Installment count must be greater than zero.");

            if (name == "Single Premium" && dto.installment_count != 1)
                throw new ArgumentException(
                    "Single Premium must have an installment count of 1.");

            bool exists = await db.premium_frequency
                .AnyAsync(x => x.frequency_name.ToLower() == name.ToLower());

            if (exists)
                throw new ArgumentException(
                    "Premium frequency already exists.");

            var frequency = mapper.Map<premium_frequency>(dto);

            frequency.frequency_id = 0;
            frequency.frequency_name = name;
            frequency.status = "Active";

            await db.premium_frequency.AddAsync(frequency);
            await db.SaveChangesAsync();
        }

        public async Task<List<premium_frequencyDTO>> FetchAllFrequencies()
        {
            var frequencies = await db.premium_frequency
                .AsNoTracking()
                .OrderBy(x => x.installment_count)
                .ToListAsync();

            return mapper.Map<List<premium_frequencyDTO>>(frequencies);
        }

        public async Task<premium_frequencyDTO?> FindFrequencyById(int id)
        {
            var frequency = await db.premium_frequency
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.frequency_id == id);

            return frequency == null
                ? null
                : mapper.Map<premium_frequencyDTO>(frequency);
        }

        public async Task UpdateFrequency(int id, premium_frequencyDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Premium frequency data is required.");

            var frequency = await db.premium_frequency.FindAsync(id);

            if (frequency == null)
                throw new KeyNotFoundException(
                    "Premium frequency not found.");

            string name = NormalizeFrequencyName(dto.frequency_name);

            if (!ValidFrequencies.Contains(name))
                throw new ArgumentException("Invalid premium frequency.");

            if (dto.installment_count <= 0)
                throw new ArgumentException(
                    "Installment count must be greater than zero.");

            if (name == "Single Premium" && dto.installment_count != 1)
                throw new ArgumentException(
                    "Single Premium must have an installment count of 1.");

            bool exists = await db.premium_frequency.AnyAsync(
                x => x.frequency_id != id &&
                     x.frequency_name.ToLower() == name.ToLower());

            if (exists)
                throw new ArgumentException(
                    "Premium frequency already exists.");

            if (dto.status != "Active" && dto.status != "Inactive")
                throw new ArgumentException(
                    "Status must be Active or Inactive.");

            frequency.frequency_name = name;
            frequency.installment_count = dto.installment_count;
            frequency.status = dto.status;

            await db.SaveChangesAsync();
        }

        public async Task DeactivateFrequency(int id)
        {
            var frequency = await db.premium_frequency.FindAsync(id);

            if (frequency == null)
                throw new KeyNotFoundException(
                    "Premium frequency not found.");

            frequency.status = "Inactive";

            await db.SaveChangesAsync();
        }

        private static string NormalizeFrequencyName(string? name)
        {
            return name?.Trim() ?? string.Empty;
        }
    }
}
