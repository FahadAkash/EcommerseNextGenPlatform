using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;

namespace EcommerseNextGenPlatform.Services.Interface
{
    public interface ICartService
    {
        Task<CartDTO> AddToCartAsync(string userId, int productId, int quantity);
    }

}
