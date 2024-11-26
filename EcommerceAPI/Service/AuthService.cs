using EcommerceAPI.Data;
using EcommerceAPI.Models.AuthModels;
using EcommerceAPI.Models.Dtos;
using EcommerceAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Register(RegistrationRequestDto registrationdeet)
    {
        string errorMessage = "";
        ApplicationUser user = new ApplicationUser()
        {
            UserName = registrationdeet.Email,
            FullName = registrationdeet.FullName,
            Email = registrationdeet.Email,
            NormalizedEmail = registrationdeet.Email.ToUpper(),
            PhoneNumber = registrationdeet.PhoneNumber,
                    
                    
        };
        
        try
        {
            var result = await _userManager.CreateAsync(user, registrationdeet.Password);
            if (result.Succeeded)
            {
                var retrievedUser = _db.ApplicationUsers.First(u => u.Email == user.Email);
                UserDto userDto = new UserDto()
                {
                    ID = retrievedUser.Id,
                    FullName = retrievedUser.FullName,
                    Email = retrievedUser.Email,
                    PhoneNumber = retrievedUser.PhoneNumber
                };
                

                string roleName = "Customer";
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                // Assign the role to the user
                var roleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!roleResult.Succeeded)
                {
                    return roleResult.Errors.FirstOrDefault().Description;
                }


                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
        }

        return errorMessage;

    }

    public async Task<LoginResponseDto> Login(LoginRequestDto logindeet)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == logindeet.Email.ToLower());
        bool passwordIsValid = await _userManager.CheckPasswordAsync(user, logindeet.Password);
        if (user != null && passwordIsValid)
        {
            UserDto userDto = new UserDto()
            {
                ID = user.Id,
                FullName = user.FullName,
                Email = user.Email,                      
                PhoneNumber = user.PhoneNumber,
                
            };
            var roles = await _userManager.GetRolesAsync(user);
            var generatedToken = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto()
            {
                User = userDto,
                Token = generatedToken
            };
        }
        
        return new LoginResponseDto()
        {
            User = null,
            Token = ""
        };
        
    }

    

}