using EcommerseNextGenPlatform.Models.DTO.Profile;
using EcommerseNextGenPlatform.Services.Interface;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations;
using Intelligent_E_Commerce_Platform_with_Smart_Recommendations.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EcommerseNextGenPlatform.Services.ProfileService
{
    public class UserProfile(ECommerceDbContext? context) :  IUserProfileService
    {
 
        private readonly UserManager<ApplicationUser>? _userManager;
        private readonly ILogger? _logger;
        private readonly ECommerceDbContext? _context = context;

        public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            return new UserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Phone = user.Phone,
                PostalCode = user.PostalCode,
                Region = user.Region
            };
        }

        public IDisposable Subscribe(IObserver<UserProfile> observer)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfileAsync(string userId, UpdateProfileDTO profileDTO)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            if (profileDTO == null) {
                throw new Exception("Fixed The DTO");
            }
           user.FirstName = profileDTO.FirstName;
            user.LastName = profileDTO.LastName;
            user.Address = profileDTO.Address;
            user.City = profileDTO.City;
            user.Country = profileDTO.Country;
            user.Phone = profileDTO.Phone;
            user.PostalCode = profileDTO.PostalCode;
            user.Region = profileDTO.Region;
            await _context.SaveChangesAsync();

        }
    }
}
