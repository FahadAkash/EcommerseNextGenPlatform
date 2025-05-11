using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models;
using Microsoft.EntityFrameworkCore;
using EcommerseNextGenPlatform.Services.Interface;


namespace Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Services
{
    public class CartService : ICartService
    {
        private readonly ECommerceDbContext _context;
        public CartService(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<CartDTO> AddToCartAsync(string userId, int productId, int quantity)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                _context.Carts.Add(cart);
            }
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null || !product.IsApproved || product.Stock < quantity)
                    throw new Exception("Product unavailable");

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null)
                {
                    cartItem = new CartItem { CartId = cart.Id, ProductId = productId, Quantity = quantity };
                    cart.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new CartDTO { Id = cart.Id, UserId = cart.UserId, CartItems = cart.CartItems.Select(ci => new CartItemDTO { Id = ci.Id, CartId = ci.CartId, ProductId = ci.ProductId, Quantity = ci.Quantity }).ToList() };
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                throw;

            }
            
        }


    }
}
