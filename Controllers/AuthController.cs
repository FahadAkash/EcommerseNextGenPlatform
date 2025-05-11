using EcommerseNextGenPlatform.Services.Interface;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var token = await _authService.RegisterAsync(registerDto);
            return Ok(token);
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var token = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(token);
        }
        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin")] // Only existing admins can create new admins
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
        {
            var token = await _authService.RegisterAsync(registerDto);
            return Ok(token);
        }


    }
}
