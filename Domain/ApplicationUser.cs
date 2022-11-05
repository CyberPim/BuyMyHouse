
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser Creator { get; set; }

        public ApplicationUser()
        {

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return null;
        }
    }
}
