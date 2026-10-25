
using Microsoft.AspNetCore.Mvc;
using QuoteGenerationMS.Application.DTO;
using QuoteGenerationMS.Application.Interface;

namespace QuoteGenerationMS.Controllers
{
    [Route("api/v1/quotes")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService service;

        public QuoteController(IQuoteService service)
        {
            this.service = service;
        }

        // Create Quote
        [HttpPost]
        public async Task<IActionResult> CreateQuote(createQuoteDTO dto)
        {
            try
            {
                await service.CreateQuote(dto);

                return Ok(new
                {
                    msg = "Quote created"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message
                });
            }
        }

        // Get Quote By ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteById(int id)
        {
            var data = await service.GetQuoteById(id);

            if (data == null)
            {
                return NotFound(new
                {
                    msg = "Quote not found"
                });
            }

            return Ok(data);
        }

        // Get All Quotes
        [HttpGet]
        public async Task<IActionResult> GetAllQuotes()
        {
            var data = await service.GetAllQuotes();

            return Ok(data);
        }

        // Update Quote
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(
            int id,
            updateQuoteDTO dto)
        {
            try
            {
                await service.UpdateQuote(id, dto);

                return Ok(new
                {
                    msg = "Quote updated"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message
                });
            }
        }

        // Cancel Quote
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelQuote(int id)
        {
            try
            {
                await service.CancelQuote(id);

                return Ok(new
                {
                    msg = "Quote cancelled"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message
                });
            }
        }

        // Send Quote For Underwriting
        [HttpPost("{quoteId}/send-for-underwriting")]
        public async Task<IActionResult> SendQuoteForUnderwriting(
            int quoteId,
            [FromQuery] int customerId)
        {
            try
            {
                var underwritingId =
                    await service.SendQuoteForUnderwriting(
                        quoteId,
                        customerId);

                return Ok(new
                {
                    msg = "Quote sent for underwriting successfully.",
                    quote_id = quoteId,
                    underwriting_id = underwritingId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message
                });
            }
        }
    }
}
