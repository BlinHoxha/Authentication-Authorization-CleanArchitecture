using DDD.Domain.Common.Models;
using DDD.Domain.Enums;

namespace DDD.Domain.User
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public Roles Role { get; set; }
        
    }
}
