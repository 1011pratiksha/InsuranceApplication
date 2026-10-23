
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Application.Interface;
using ProductManagementMS.Domain.Entities;
using ProductManagementMS.Infrastructure.Data;

namespace ProductManagementMS.Infrastructure.Repositories
{
    public class InsuranceProductService : IInsuranceProductService
    {
        AppDbContext db;
        IMapper mapper;

        public InsuranceProductService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddProduct(insurance_productDTO dto)
        {
            var category = await db.product_category.FindAsync(dto.category_id);

            if (category == null)
                throw new Exception("Product category not found.");

            var duplicate = await db.insurance_product.AnyAsync(
                x => x.product_code == dto.product_code
                  || x.product_name == dto.product_name);

            if (duplicate)
                throw new Exception("Product code or product name already exists.");

            var d = mapper.Map<insurance_product>(dto);

            d.product_id = 0;
            d.status = "Inactive";

            await db.insurance_product.AddAsync(d);
            await db.SaveChangesAsync();
        }

        public async Task<List<insurance_productDTO>> FetchAllProducts()
        {
            var d = await db.insurance_product.ToListAsync();

            var data = mapper.Map<List<insurance_productDTO>>(d);

            return data;
        }

        public async Task<insurance_productDTO?> FindProductById(int id)
        {
            var d = await db.insurance_product.FindAsync(id);

            if (d == null)
                return null;

            var data = mapper.Map<insurance_productDTO>(d);

            return data;
        }


        public async Task UpdateProduct(int id, insurance_productDTO dto)
        {
            var d = await db.insurance_product.FindAsync(id);

            if (d == null)
                throw new Exception("Product not found.");

            var category = await db.product_category.FindAsync(dto.category_id);

            if (category == null)
                throw new Exception("Product category not found.");

            var duplicate = await db.insurance_product.AnyAsync(
                x => x.product_id != id &&
                    (x.product_code == dto.product_code ||
                     x.product_name == dto.product_name));

            if (duplicate)
                throw new Exception("Product code or product name already exists.");

            // Validate product status
            if (dto.status != "Active" && dto.status != "Inactive")
                throw new Exception("Status must be Active or Inactive.");

            // Validate age limits
            if (dto.minimum_age < 0 || dto.maximum_age < dto.minimum_age)
                throw new Exception("Invalid minimum or maximum age.");

            // Validate sum-insured limits
            if (dto.minimum_sum_insured <= 0 ||
                dto.maximum_sum_insured < dto.minimum_sum_insured)
                throw new Exception("Invalid minimum or maximum sum insured.");

            d.category_id = dto.category_id;
            d.product_code = dto.product_code;
            d.product_name = dto.product_name;
            d.product_type = dto.product_type;
            d.description = dto.description;
            d.minimum_age = dto.minimum_age;
            d.maximum_age = dto.maximum_age;
            d.minimum_sum_insured = dto.minimum_sum_insured;
            d.maximum_sum_insured = dto.maximum_sum_insured;

            // The missing assignment
            d.status = dto.status;

            await db.SaveChangesAsync();
        }


        public async Task DeactivateProduct(int id)
        {
            var d = await db.insurance_product.FindAsync(id);

            if (d == null)
                throw new Exception("Product not found.");

            d.status = "Inactive";

            await db.SaveChangesAsync();
        }
    }
}
