namespace EcommerceAPI.Models.Dtos;

public class RegistrationRequestDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}