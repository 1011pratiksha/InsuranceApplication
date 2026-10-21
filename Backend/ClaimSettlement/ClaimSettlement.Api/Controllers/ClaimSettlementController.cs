using ClaimSettlement.Application.DTO;
using ClaimSettlement.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClaimSettlement.Api.Controllers;

[Route("api/v1/claims/{id:long}/settlement")]
[ApiController]
public class ClaimSettlementController : ControllerBase
{
    IClaimSettlementService settlementservice;
    public ClaimSettlementController(IClaimSettlementService settlementservice)
    {
        this.settlementservice = settlementservice;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessSettlement(long id, SettlementDTO dto)
    {
        try
        {
            var data = await settlementservice.ProcessSettlement(id, dto);
            return Ok(data);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { msg = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { msg = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { msg = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> FindSettlementByClaimId(long id)
    {
        var data = await settlementservice.FindSettlementByClaimId(id);
        if (data == null)
            return NotFound(new { msg = "Settlement not found." });
        return Ok(data);
    }


    [HttpPut("/api/v1/claims/{id:long}/approve")]
    public async Task<IActionResult> ApproveClaim(long id)
    {
        try
        {
            var message = await settlementservice.ApproveClaim(id);
            return Ok(new { message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("/api/v1/claims/{id:long}/reject")]
    public async Task<IActionResult> RejectClaim(long id)
    {
        try
        {
            var message = await settlementservice.RejectClaim(id);
            return Ok(new { message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

}