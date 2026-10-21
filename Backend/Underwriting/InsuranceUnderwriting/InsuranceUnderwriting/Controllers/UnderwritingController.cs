using InsuranceUnderwriting.Application.DTO;
using InsuranceUnderwriting.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceUnderwriting.Controllers
{
    [Route("api/v1/underwriting")]
    [ApiController]
    public class UnderwritingController : ControllerBase
    {

        IUnderwritingService service;

        public UnderwritingController(IUnderwritingService service)
        {
            this.service = service;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUnderwrithingCase(CreateUnderwritingCaseDto dto)
        {
            await service.CreateUnderwritingCase(dto);
            return Ok(new { msg = "Creted Suuccessfully !" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var d = await service.GetAllUnderwritingCase();
            return Ok(d);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var d = await service.GetById(id);

            if (d == null)
            {
                return NotFound(new { msg = "Underwriting Case Not Found!" });
            }

            return Ok(d);
        }



        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(UpdateUnderwritingCaseDto dto, long id)
        {
            await service.update(dto,id);

            return Ok(new { msg = "Update Successfully !" });

        }


        [HttpPost]
        [Route("{id}/medical")]
        public async Task<IActionResult> SaveMedicalData(SaveMedicalDetailsDto dto, long id)
        {
            await service.SaveMedicalDetails(dto, id);
            return Ok(new { msg = "Medical Data Added Successfully!" });
        }



        [HttpGet]
        [Route("{id}/medical")]
        public async Task<IActionResult> GetMedicalDetails(long id)
        {
          var d = await service.GetMedicalDetails(id);

            if(d == null)
            {
                return NotFound(new { msg = "Not Found This Data!" });
            }
            
            return Ok(d);
        }


        [HttpPut]
        [Route("{id}/medical")]
        public async Task<IActionResult> UpdateMedicalDetails(UpdateMedicalDetailsDto dto, long id)
        {
            await service.UpdateMedicalDetails(dto, id);
            return Ok(new { msg = "Medical Details Update Successfully !!" });
        }


        [HttpPost]
        [Route("{id}/financial")]
        public async Task<IActionResult> SaveFinanceDetails(SaveFinancialDetailsDto dto, long id)
        {
            await service.SaveFinanceDetails(dto, id);
            return Ok(new { msg = "Financial Details Added Successfully!" });

        }


        [HttpGet]
        [Route("{id}/financial")]
        public async Task<IActionResult> GetFincinalDetails(long id)
        {
            var f = await service.GetFinancialDetails(id);

            if(f == null)
            {
                return NotFound(new { msg = "Financial Details Not Found!" });
            }

            return Ok(f);
        }


        [HttpPut]
        [Route("{id}/financial")]
        public async Task<IActionResult> UpdateFincinalDetails(UpdateFinancialDetailsDto dto, long id)
        {
            await service.UpdateFinancialDetails(dto, id);

            return Ok(new { msg = "Financial Details Updated Successfully!" });
        }

        [HttpPut]
        [Route("{id}/approve")]
        public async Task<IActionResult> ApprovalUnderwrtingStatus(long id)
        {
            await service.ApproveUnderwriting(id);

            return Ok(new { msg = "Underwriting Case Approved Successfully!" });

        }


        [HttpPut]
        [Route("{id}/reject")]
        public async Task<IActionResult> RejectUnderwrtingStatus(long id)
        {
            await service.RejectUnderwriting(id);

            return Ok(new { msg = "Underwriting Case Rejected Successfully!" });
        }


        [HttpPut]
        [Route("{id}/loading")]
        public async Task<IActionResult> ApplyPremiumLoading(ApplyPremiumLoadingDto dto, long id)
        {
            await service.ApplyPremiumLoading(dto, id);
            return Ok("Premium Loading Applied Successfully!");
        }


        [HttpGet]
        [Route("{id}/decision")]
        public async Task<IActionResult> GetDetailsDecision(long id)
        {
           var d = await service.GetDecision(id);

            if(d == null)
            {
                return NotFound("Underwriting Decision Not Found");
            }

            return Ok(d);

        }

    }
}
