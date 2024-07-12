using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat2.Core.Entites.Identity;

namespace Talabat2.APIs.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email);
            return user;
        }
    }
}
