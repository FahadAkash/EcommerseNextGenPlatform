using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models.DTO;

namespace EcommerseNextGenPlatform.Services.Interface
{
    public interface IAuthService
    {
        Task<TokenDTO> RegisterAsync(RegisterDTO registerDto);
        Task<TokenDTO> LoginAsync(LoginDTO loginDto);
        Task<TokenDTO> RefreshTokenAsync(string refreshToken);
    }
}
