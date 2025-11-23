using Microsoft.AspNetCore.Mvc;
using WorkTimePanelFull.Application.DTOs;
using WorkTimePanelFull.Application.Services;

namespace WorkTimePanelFull.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] UserLoginDto dto)
        {
            var token = await _auth.AuthenticateAsync(dto);
            if (token == null) return Unauthorized(new ProblemDetails { Title = "Invalid credentials", Status = 401 });
            return Ok(new { access_token = token });
        }
    }
}
