using EcommerceAPI.Models.AuthModels;
using EcommerceAPI.Models.Dtos;

namespace EcommerceAPI.Service.IService;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDto registrationdeet);
    Task<LoginResponseDto> Login(LoginRequestDto logindeet);

}