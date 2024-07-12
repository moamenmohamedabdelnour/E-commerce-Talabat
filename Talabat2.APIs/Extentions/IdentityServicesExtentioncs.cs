using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat2.Core.Entites.Identity;
using Talabat2.Core.Services;
using Talabat2.Repository.Identity;
using Talabat2.Service;

namespace Talabat2.APIs.Extentions
{
    public static class IdentityServicesExtentioncs
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection Services,IConfiguration configuration)
        {
            //Becouse We Change Default Of IdentityUser Must Create Service For It 
            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //here Can Put Any Configrution To Sign In Or Login
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;

            })./*To Complete Code For Methods Built In In Identity Package*/AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddScoped<ITokenService, TokenService>();
            //return Object Of UserManager RoleManager SignInManager
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
                {
                    options.TokenValidationParameters=new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer=configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience=configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                }
                
                );
            return Services;
        }
    }
}
