using EcommerceAPI.Models.Dtos;
using EcommerceAPI.Service;
using EcommerceAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private ResponseDto _response = new ResponseDto();

        public AuthController(IAuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto regrequestDto)
        {
            
                var errorMessage = await _authService.Register(regrequestDto);
                if (!errorMessage.IsNullOrEmpty())
                {
                    _response.IsSuccess = false;
                    _response.Message = errorMessage;

                }

                return Ok(_response);
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto logindeets)
        {
            LoginResponseDto loginResponse = await _authService.Login(logindeets);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is Incorrect";
                return BadRequest(_response);
            }

            _response.Result = loginResponse;
            return Ok(_response);
        }


    }
}
