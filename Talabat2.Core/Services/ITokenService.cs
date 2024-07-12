using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites.Identity;

namespace Talabat2.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync (AppUser user,UserManager<AppUser> userManager);
    }
}
