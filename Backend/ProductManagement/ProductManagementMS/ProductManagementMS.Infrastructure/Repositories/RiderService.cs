
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class RiderService : IRiderService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public RiderService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddRider(int productId, rider_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Rider data is required.");

            bool productExists = await db.insurance_product
                .AnyAsync(x => x.product_id == productId);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");

            ValidateRider(dto, validateStatus: false);

            var rider = mapper.Map<rider_master>(dto);

            rider.rider_id = 0;
            rider.product_id = productId;
            rider.rider_name = dto.rider_name.Trim();
            rider.status = "Active";

            await db.rider_master.AddAsync(rider);
            await db.SaveChangesAsync();
        }

        public async Task<List<rider_masterDTO>> FetchRidersByProductId(
            int productId)
        {
            bool productExists = await db.insurance_product
                .AnyAsync(x => x.product_id == productId);

            if (!productExists)
                throw new KeyNotFoundException("Product not found.");

            var riders = await db.rider_master
                .AsNoTracking()
                .Where(x => x.product_id == productId)
                .OrderBy(x => x.rider_id)
                .ToListAsync();

            return mapper.Map<List<rider_masterDTO>>(riders);
        }

        public async Task UpdateRider(int riderId, rider_masterDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Rider data is required.");

            var rider = await db.rider_master.FindAsync(riderId);

            if (rider == null)
                throw new KeyNotFoundException("Rider not found.");

            ValidateRider(dto, validateStatus: true);

            rider.rider_name = dto.rider_name.Trim();
            rider.rider_description = dto.rider_description;
            rider.rider_premium = dto.rider_premium;
            rider.status = dto.status;

            await db.SaveChangesAsync();
        }

        public async Task DeleteRider(int riderId)
        {
            var rider = await db.rider_master.FindAsync(riderId);

            if (rider == null)
                throw new KeyNotFoundException("Rider not found.");

            db.rider_master.Remove(rider);
            await db.SaveChangesAsync();
        }

        private static void ValidateRider(
            rider_masterDTO dto, bool validateStatus)
        {
            if (string.IsNullOrWhiteSpace(dto.rider_name))
                throw new ArgumentException("Rider name is required.");

            if (dto.rider_name.Trim().Length > 150)
                throw new ArgumentException(
                    "Rider name cannot exceed 150 characters.");

            if (dto.rider_premium < 0)
                throw new ArgumentException(
                    "Rider premium cannot be negative.");

            if (dto.rider_description?.Length > 4000)
                throw new ArgumentException(
                    "Rider description cannot exceed 4000 characters.");

            if (validateStatus &&
                dto.status != "Active" &&
                dto.status != "Inactive")
            {
                throw new ArgumentException(
                    "Status must be Active or Inactive.");
            }
        }
    }
}
