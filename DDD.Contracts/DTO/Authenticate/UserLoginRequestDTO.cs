using System.ComponentModel.DataAnnotations;

namespace DDD.Contracts.DTO.Authenticate;

public class UserLoginRequestDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }   
    [Required]
    public string Password { get; set; }
}