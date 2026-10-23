
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuoteGenerationMS.Application.DTO;
using QuoteGenerationMS.Application.Interface;
using QuoteGenerationMS.Domain.Entities;
using QuoteGenerationMS.Infrastructure.Data;

namespace QuoteGenerationMS.Infrastructure.Repositories
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        private readonly IProductManagementClient productClient;
        private readonly IUnderwritingClient underwritingClient;

        public QuoteService(
            AppDbContext db,
            IMapper mapper,
            IProductManagementClient productClient,
            IUnderwritingClient underwritingClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.productClient = productClient;
            this.underwritingClient = underwritingClient;
        }

        public async Task CreateQuote(createQuoteDTO dto)
        {
            // Fetch product from Product Management MS
            var product =
                await productClient.GetProductById(dto.product_id);

            if (product == null)
                throw new Exception(
                    "Product not found in Product Management.");

            // Only active products can be quoted
            if (!string.Equals(
                product.status,
                "Active",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(
                    "Cannot create quote because the product is inactive.");
            }

            // Validate sum insured
            if (dto.sum_insured < product.minimum_sum_insured ||
                dto.sum_insured > product.maximum_sum_insured)
            {
                throw new Exception(
                    $"Sum insured must be between " +
                    $"{product.minimum_sum_insured} and " +
                    $"{product.maximum_sum_insured}.");
            }

            // Create quote
            var d = mapper.Map<quote>(dto);

            d.quote_id = 0;
            d.quote_number =
                $"QT-{Guid.NewGuid().ToString("N")[..12].ToUpper()}";
            d.quote_date = DateTime.Today;
            d.expiry_date = DateTime.Today.AddDays(30);
            d.quote_status = "Pending";

            await db.quote.AddAsync(d);
            await db.SaveChangesAsync();

            // Add quote history
            await db.quote_history.AddAsync(new quote_history
            {
                quote_id = d.quote_id,
                action = "Quote Created",
                performed_by = 0,
                remarks = "Quote created with Pending status.",
                action_date = DateTime.Now
            });

            await db.SaveChangesAsync();
        }

        // Send a pending quote to Underwriting MS
        public async Task<int> SendQuoteForUnderwriting(
            int quoteId,
            int customerId)
        {
            var quote = await db.quote.FindAsync(quoteId);

            if (quote == null)
                throw new Exception("Quote not found.");

            if (quote.quote_status != "Pending")
                throw new Exception(
                    "Only Pending quotes can be sent for underwriting.");

            if (customerId <= 0)
                throw new Exception("Invalid customer ID.");

            // Prepare request for Underwriting MS
            var request = new createUnderwritingRequestDTO
            {
                quote_id = quote.quote_id,
                customer_id = customerId,
                product_id = quote.product_id
            };

            // Call Underwriting MS
            int underwritingId =
                await underwritingClient.CreateCase(request);

            // Record successful case creation in quote history
            await db.quote_history.AddAsync(new quote_history
            {
                quote_id = quote.quote_id,
                action = "Underwriting Case Created",
                performed_by = 0,
                remarks =
                    $"Underwriting case created. ID: {underwritingId}",
                action_date = DateTime.Now
            });

            await db.SaveChangesAsync();

            return underwritingId;
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
                throw new Exception(
                    "Only Pending quotes can be updated.");

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
                throw new Exception(
                    "Converted quote cannot be cancelled.");

            if (d.quote_status == "Expired" ||
                d.quote_status == "Rejected")
            {
                throw new Exception(
                    "Expired or rejected quote cannot be cancelled.");
            }

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
