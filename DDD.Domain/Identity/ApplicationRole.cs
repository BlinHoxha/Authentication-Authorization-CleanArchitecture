using Microsoft.AspNetCore.Identity;

namespace DDD.Domain.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string name) : this()
        {
            base.Name = name;
        }
    }
}
