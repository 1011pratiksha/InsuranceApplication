
using Microsoft.AspNetCore.Mvc;
using QuoteGenerationMS.Application.DTO;
using QuoteGenerationMS.Application.Interface;

namespace QuoteGenerationMS.Controllers
{
    [Route("api/v1/quotes")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        IQuoteService service;

        public QuoteController(IQuoteService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote(createQuoteDTO dto)
        {
            try
            {
                await service.CreateQuote(dto);
                return Ok(new { msg = "Quote created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteById(int id)
        {
            var data = await service.GetQuoteById(id);

            if (data == null)
                return NotFound(new { msg = "Quote not found" });

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotes()
        {
            var data = await service.GetAllQuotes();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(
            int id, updateQuoteDTO dto)
        {
            try
            {
                await service.UpdateQuote(id, dto);
                return Ok(new { msg = "Quote updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelQuote(int id)
        {
            try
            {
                await service.CancelQuote(id);
                return Ok(new { msg = "Quote cancelled" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
