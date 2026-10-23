
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteGenerationMS.Application.DTO;
using QuoteGenerationMS.Application.Interface;
using QuoteGenerationMS.Domain.Entities;
using QuoteGenerationMS.Infrastructure.Data;

namespace QuoteGenerationMS.Infrastructure.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        private readonly IProductManagementClient productClient;

        public QuoteService(
            AppDbContext db,
            IMapper mapper,
            IProductManagementClient productClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.productClient = productClient;
        }

        public async Task CreateQuote(createQuoteDTO dto)
        {
            // Call Product Management to fetch the product
            var product = await productClient.GetProductById(dto.product_id);

            if (product == null)
                throw new Exception("Product not found in Product Management.");

            // Only active products can be used for quotes
            if (!string.Equals(
                product.status,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Cannot create quote because the product is inactive.");
            }

            // Validate sum insured against product limits
            if (dto.sum_insured < product.minimum_sum_insured ||
                dto.sum_insured > product.maximum_sum_insured)
            {
                throw new Exception(
                    $"Sum insured must be between " +
                    $"{product.minimum_sum_insured} and " +
                    $"{product.maximum_sum_insured}.");
            }

            // Existing quote creation logic
            var d = mapper.Map<quote>(dto);

            d.quote_id = 0;
            d.quote_number =
                $"QT-{Guid.NewGuid().ToString("N")[..12].ToUpper()}";
            d.quote_date = DateTime.Today;
            d.expiry_date = DateTime.Today.AddDays(30);
            d.quote_status = "Pending";

            await db.quote.AddAsync(d);
            await db.SaveChangesAsync();

            var history = new quote_history
            {
                quote_id = d.quote_id,
                action = "Quote Created",
                performed_by = 0,
                remarks = "Quote created with Pending status.",
                action_date = DateTime.Now
            };

            await db.quote_history.AddAsync(history);
            await db.SaveChangesAsync();
        }

        public async Task<List<quoteDTO>> GetAllQuotes()
        {
            var d = await db.quote.ToListAsync();

            return mapper.Map<List<quoteDTO>>(d);
        }

        public async Task<quoteDTO?> GetQuoteById(int id)
        {
            var d = await db.quote.FindAsync(id);

            if (d == null)
                return null;

            return mapper.Map<quoteDTO>(d);
        }

        public async Task UpdateQuote(int id, updateQuoteDTO dto)
        {
            var d = await db.quote.FindAsync(id);

            if (d == null)
                throw new Exception("Quote not found.");

            if (d.quote_status != "Pending")
                throw new Exception("Only Pending quotes can be updated.");

            if (d.expiry_date < DateTime.Today)
            {
                d.quote_status = "Expired";

                await db.quote_history.AddAsync(new quote_history
                {
                    quote_id = d.quote_id,
                    action = "Quote Expired",
                    performed_by = 0,
                    remarks = "Quote expired.",
                    action_date = DateTime.Now
                });

                await db.SaveChangesAsync();

                throw new Exception("Quote has expired.");
            }

            d.premium_frequency_id = dto.premium_frequency_id;
            d.policy_term = dto.policy_term;
            d.sum_insured = dto.sum_insured;

            await db.quote_history.AddAsync(new quote_history
            {
                quote_id = d.quote_id,
                action = "Quote Updated",
                performed_by = 0,
                remarks = "Quote details updated.",
                action_date = DateTime.Now
            });

            await db.SaveChangesAsync();
        }

        public async Task CancelQuote(int id)
        {
            var d = await db.quote.FindAsync(id);

            if (d == null)
                throw new Exception("Quote not found.");

            if (d.quote_status == "Converted")
                throw new Exception("Converted quote cannot be cancelled.");

            if (d.quote_status == "Expired" ||
                d.quote_status == "Rejected")
                throw new Exception(
                    "Expired or rejected quote cannot be cancelled.");

            d.quote_status = "Rejected";

            await db.quote_history.AddAsync(new quote_history
            {
                quote_id = d.quote_id,
                action = "Quote Cancelled",
                performed_by = 0,
                remarks = "Quote cancelled.",
                action_date = DateTime.Now
            });

            await db.SaveChangesAsync();
        }
    }
}
