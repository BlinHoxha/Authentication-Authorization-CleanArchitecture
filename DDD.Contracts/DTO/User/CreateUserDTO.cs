using DDD.Domain.Enums;

namespace DDD.Contracts.DTO.User;

public class CreateUserDTO
{
public string UserName { get; set; }
public string FirstName { get; set; }
public string LastName { get; set; }
public string Email { get; set; }
public string PhoneNumber { get; set; }
public DateTime DateOfBirth { get; set; }
public string Country { get; set; }
public Roles Role { get; set; }
}