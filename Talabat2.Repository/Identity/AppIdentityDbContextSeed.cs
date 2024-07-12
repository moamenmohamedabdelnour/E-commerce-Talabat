using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites.Identity;

namespace Talabat2.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        //To Seed To Users 
        //UserManager responsible For Users Data To Could Call Users Table
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Mostafa Ghazal",
                    Email="mostafaghazal248@gmail.com",
                    UserName="mostafaghazal248",
                    PhoneNumber="01020797493"

                };
                await userManager.CreateAsync(User,"Mostafa@10");
            //call This Function Program File
            }
        }

    }
}
