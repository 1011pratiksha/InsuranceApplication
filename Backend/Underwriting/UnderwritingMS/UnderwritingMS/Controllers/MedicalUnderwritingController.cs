
using Microsoft.AspNetCore.Mvc;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;

namespace UnderwritingMS.Controllers
{
    [Route("api/v1/underwriting/{id}/medical")]
    [ApiController]
    public class MedicalUnderwritingController : ControllerBase
    {
        IMedicalUnderwritingService service;

        public MedicalUnderwritingController(
            IMedicalUnderwritingService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalDetails(
            int id, medical_underwritingDTO dto)
        {
            try
            {
                await service.AddMedicalDetails(id, dto);
                return Ok(new { msg = "Medical details added" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicalDetails(int id)
        {
            var data = await service.GetMedicalDetails(id);

            if (data == null)
                return NotFound(new { msg = "Medical details not found" });

            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedicalDetails(
            int id, medical_underwritingDTO dto)
        {
            try
            {
                await service.UpdateMedicalDetails(id, dto);
                return Ok(new { msg = "Medical details updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
