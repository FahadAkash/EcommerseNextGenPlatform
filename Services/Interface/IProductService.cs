using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;

namespace EcommerseNextGenPlatform.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync(bool isAdmin);
        Task<ProductDTO> AddProductAsync(ProductDTO productDto);
        Task<bool> ApproveProductAsync(int id);

    }
}
