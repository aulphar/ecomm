using EcommerceAPI.Models.AuthModels;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Service.IService;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}