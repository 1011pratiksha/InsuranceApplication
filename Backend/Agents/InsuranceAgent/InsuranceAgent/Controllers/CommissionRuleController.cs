using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAgent.Controllers
{
    [Route("api/v1/commission-rules")]
    [ApiController]
    public class CommissionRuleController : ControllerBase
    {
        ICommissionRuleService service;

        public CommissionRuleController(ICommissionRuleService service)
        {
            this.service = service;
        }



        [HttpPost]
        public async Task<IActionResult> CreateCommissionRule(CreateCommissionRuleDto dto)
        {
            try
            {
                await service.CreateCommissionRule(dto);
                return Ok("Commission rule created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCommissionRules()
        {
            try
            {
                var rules = await service.GetAllCommissionRules();
                return Ok(rules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        [Route("product/{productId}")]
        public async Task<IActionResult> GetCommissionRuleByProductId(int productId)
        {
            try
            {
                var rule = await service.GetCommissionRuleByProductId(productId);
                return Ok(rule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut]
        [Route("{ruleId}")]
        public async Task<IActionResult> UpdateCommissionRule(int ruleId, UpdateCommissionRuleDto dto)
        {
            try
            {
                await service.UpdateCommissionRule(ruleId, dto);
                return Ok("Commission rule updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await service.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
