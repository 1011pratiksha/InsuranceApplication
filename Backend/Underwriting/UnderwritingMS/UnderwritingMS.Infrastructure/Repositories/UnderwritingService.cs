
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class UnderwritingService : IUnderwritingService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        private readonly ICustomerRegistrationClient customerClient;
        private readonly IProductManagementClient productClient;
        private readonly IKycClient kycClient;
        private readonly IConfiguration configuration;

        public UnderwritingService(
            AppDbContext db,
            IMapper mapper,
            ICustomerRegistrationClient customerClient,
            IProductManagementClient productClient,
            IKycClient kycClient,
            IConfiguration configuration)
        {
            this.db = db;
            this.mapper = mapper;
            this.customerClient = customerClient;
            this.productClient = productClient;
            this.kycClient = kycClient;
            this.configuration = configuration;
        }

        public async Task<int> CreateCase(createUnderwritingDTO dto)
        {
            // 1. Validate request IDs
            if (dto.quote_id <= 0 ||
                dto.customer_id <= 0 ||
                dto.product_id <= 0)
            {
                throw new ArgumentException(
                    "Quote ID, customer ID and product ID must be positive.");
            }

            // 2. Check for an existing underwriting case
            var duplicate = await db.underwriting_case
                .AnyAsync(x => x.quote_id == dto.quote_id);

            if (duplicate)
            {
                throw new InvalidOperationException(
                    "An underwriting case already exists for this quote.");
            }

            // 3. Validate customer using Customer Registration API
            var customer =
                await customerClient.GetCustomerById(dto.customer_id);

            if (customer == null)
            {
                throw new KeyNotFoundException(
                    $"Customer with ID {dto.customer_id} was not found.");
            }

            if (!string.Equals(
                customer.CustomerStatus,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Only active customers can enter underwriting.");
            }

            // 4. Validate product using Product Management API
            var product =
                await productClient.GetProductById(dto.product_id);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    $"Product with ID {dto.product_id} was not found.");
            }

            if (!string.Equals(
                product.status,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Only active products can enter underwriting.");
            }

            // 5. Optionally validate KYC.
            // Keep EnableKycValidation false until the KYC API
            // endpoint and response contract have been confirmed.
            var enableKyc = bool.TryParse(
    configuration["UnderwritingSettings:EnableKycValidation"],
    out var enabled) && enabled;

            if (enableKyc)
            {
                var kyc =
                    await kycClient.GetKycStatus(dto.customer_id);

                if (kyc == null ||
                    !kyc.is_verified ||
                    !string.Equals(
                        kyc.kyc_status,
                        "Verified",
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "Customer KYC must be verified before underwriting.");
                }
            }

            // 6. Create underwriting case
            var d = mapper.Map<underwriting_case>(dto);

            d.underwriting_id = 0;
            d.underwriting_date = DateTime.Today;
            d.status = "Pending";

            await db.underwriting_case.AddAsync(d);
            await db.SaveChangesAsync();

            // 7. Record audit history after the ID is generated
            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = d.underwriting_id,
                    action = "Case Created",
                    performed_by = null,
                    remarks =
                        "Underwriting case created after customer and " +
                        "product validation.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();

            return d.underwriting_id;
        }

        public async Task<List<underwriting_caseDTO>> FetchAllCases()
        {
            var data = await db.underwriting_case.ToListAsync();

            return mapper.Map<List<underwriting_caseDTO>>(data);
        }

        public async Task<underwriting_caseDTO?> FindCaseById(int id)
        {
            var d = await db.underwriting_case.FindAsync(id);

            if (d == null)
                return null;

            return mapper.Map<underwriting_caseDTO>(d);
        }

        public async Task UpdateCase(
            int id,
            createUnderwritingDTO dto)
        {
            var d = await db.underwriting_case.FindAsync(id);

            if (d == null)
                throw new KeyNotFoundException(
                    "Underwriting case not found.");

            if (d.status != "Pending")
                throw new InvalidOperationException(
                    "Only Pending cases can be updated.");

            if (dto.quote_id <= 0 ||
                dto.customer_id <= 0 ||
                dto.product_id <= 0)
            {
                throw new ArgumentException(
                    "Quote ID, customer ID and product ID must be positive.");
            }

            var duplicate = await db.underwriting_case.AnyAsync(
                x => x.quote_id == dto.quote_id &&
                     x.underwriting_id != id);

            if (duplicate)
            {
                throw new InvalidOperationException(
                    "An underwriting case already exists for this quote.");
            }

            // Validate customer before updating the case
            var customer =
                await customerClient.GetCustomerById(dto.customer_id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            if (!string.Equals(
                customer.CustomerStatus,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Only active customers can enter underwriting.");
            }

            // Validate product before updating the case
            var product =
                await productClient.GetProductById(dto.product_id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (!string.Equals(
                product.status,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Only active products can enter underwriting.");
            }

            // Apply KYC validation when explicitly enabled
            var enableKyc = bool.TryParse(
    configuration["UnderwritingSettings:EnableKycValidation"],
    out var enabled) && enabled;

            if (enableKyc)
            {
                var kyc =
                    await kycClient.GetKycStatus(dto.customer_id);

                if (kyc == null ||
                    !kyc.is_verified ||
                    !string.Equals(
                        kyc.kyc_status,
                        "Verified",
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "Customer KYC must be verified before underwriting.");
                }
            }

            d.quote_id = dto.quote_id;
            d.customer_id = dto.customer_id;
            d.product_id = dto.product_id;

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = d.underwriting_id,
                    action = "Case Updated",
                    performed_by = null,
                    remarks = "Underwriting case details updated.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }
    }
}
