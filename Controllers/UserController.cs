using EcommerseNextGenPlatform.Models.DTO.Profile;
using EcommerseNextGenPlatform.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerseNextGenPlatform.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserProfileService userService) : ControllerBase
    {
        private readonly IUserProfileService _profileService = userService;

       
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = await _profileService.GetUserProfileAsync(Int32.Parse(userId));
            return Ok(profile);
        }
        
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _profileService.UpdateProfileAsync(userId, updateDto);
            return NoContent();
        }
    }
}
