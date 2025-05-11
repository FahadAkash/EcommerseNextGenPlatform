using EcommerseNextGenPlatform.Services.Interface;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Controllers
{

    [ApiController]
    [Route("api/[controller]")] 
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var isAdmin = User.IsInRole("Admin");
            var products = await _productService.GetAllProductsAsync(isAdmin);
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDto)
        {
            try
            {
                var product = await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveProduct(int id)
        {
            var result = await _productService.ApproveProductAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
