using DDD.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DDD.Domain.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public Roles Role { get; set; }
    }
}
