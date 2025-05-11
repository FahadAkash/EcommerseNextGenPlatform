using EcommerseNextGenPlatform.Models.DTO.Profile;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models;

namespace EcommerseNextGenPlatform.Services.Interface
{
    public interface IUserProfileService
    {
        Task<UserProfileDTO> GetUserProfileAsync(int userId);
        Task UpdateProfileAsync(string userId , UpdateProfileDTO profileDTO);

    }
}
