using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat2.Core.Entites.Identity;
using Talabat2.Core.Services;

namespace Talabat2.Service
{
    public class TokenService : ITokenService
    {
        //This DI To Access AppSetting File To Get JWT Attriputes
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        //To Create Token Of User By Passing Some Attriputes
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //Private Calims [User Defiend]
            var authClaims = new List<Claim>()
            {
                new Claim (ClaimTypes.GivenName,user.DisplayName),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            };
            //Create RolesClaims 
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
               authClaims.Add(new Claim (ClaimTypes.Role, role));
            }
            //AuthKey 
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            //Register Claims Will Pass When Create Token
            var token = new JwtSecurityToken
                (
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                //expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"]))),
                claims:authClaims,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
