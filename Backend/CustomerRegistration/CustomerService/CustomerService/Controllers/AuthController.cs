using CustomerRegistration.Application.DTO;
using CustomerRegistration.Application.Interface;
using CustomerRegistration.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("api/v1/auth/Register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await service.Register(dto);
            return Ok(new {message = "User Registered Successfully"});
        }

        [HttpPost]
        [Route("api/v1/auth/Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await service.Login(dto);
            if (user == null)
            {
                return Unauthorized(new {message = "Invalid email or password"});
            }
            return Ok(user);
        }
    }
}
