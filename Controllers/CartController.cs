using EcommerseNextGenPlatform.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok(cart);
        }
    }
}
