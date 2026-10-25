
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace UnderwritingMS.Controllers
{
    [Authorize]
    [Route("api/v1/underwriting/{id}/premium-loading")]
    [ApiController]
    public class PremiumLoadingController : ControllerBase
    {
        IPremiumLoadingService service;

        public PremiumLoadingController(IPremiumLoadingService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> CalculateLoading(int id)
        {
            try
            {
                var data = await service.CalculateLoading(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Underwriting case not found." ||
                    ex.Message == "Risk score not found. Calculate risk score first.")
                {
                    return NotFound(new { msg = ex.Message });
                }

                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
