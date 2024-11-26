using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Models.AuthModels;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}