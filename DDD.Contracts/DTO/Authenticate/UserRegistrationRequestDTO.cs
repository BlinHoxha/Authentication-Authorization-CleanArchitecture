using System.ComponentModel.DataAnnotations;

namespace DDD.Contracts.DTO.Authenticate;

public class UserRegistrationRequestDTO
{
    [Required]
    public string UserName { get; set; }
    public string FirstName { get; set; }   
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }   
    [Required]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Roles Role { get; set; }

    public enum Roles
    {
        Admin,
        User,
        HR,
        Manager
    }
}
