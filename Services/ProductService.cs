using EcommerseNextGenPlatform.Services.Interface;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceDbContext _context;

        public ProductService(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync(bool isAdmin)
        {
            var query = _context.Products.AsQueryable();
            if (!isAdmin)
                query = query.Where(p => p.IsApproved);

            return await query.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CatergoryId,
                IsApproved = p.IsApproved
            }).ToListAsync();
        }
        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            // Validate category exists
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == productDto.CategoryId);

            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {productDto.CategoryId} not found");

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CatergoryId = productDto.CategoryId, // Correct property name
                IsApproved = productDto.IsApproved
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return the complete product info
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CatergoryId,
                IsApproved = product.IsApproved
            };
        }
        public async Task<bool> ApproveProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
