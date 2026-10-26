using ClaimManagement.Application.DTO;
using ClaimManagement.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimManagement.API.Controllers;

[Route("api")]
[ApiController]
public class ClaimSettlementController : ControllerBase
{
    IClaimSettlementRepository settlementservice;
    public ClaimSettlementController(IClaimSettlementRepository settlementservice)
    {
        this.settlementservice = settlementservice;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/claims/{id:long}/settlement")]
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

    [Authorize(Roles = "Admin,Customer")]
    [HttpGet("claims/{id:long}/settlement")]
    public async Task<IActionResult> FindSettlementByClaimId(long id)
    {
        var data = await settlementservice.FindSettlementByClaimId(id);
        if (data == null)
            return NotFound(new { msg = "Settlement not found." });
        return Ok(data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("admin/claims/{id:long}/approve")]
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

    [Authorize(Roles = "Admin")]
    [HttpPut("admin/claims/{id:long}/reject")]
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