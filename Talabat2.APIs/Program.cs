using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat2.APIs.Errors;
using Talabat2.APIs.Extentions;
using Talabat2.APIs.Helper;
using Talabat2.APIs.Middlewares;
using Talabat2.Core.Entites.Identity;
using Talabat2.Core.Repositories;
using Talabat2.Repository;
using Talabat2.Repository.Data;
using Talabat2.Repository.Identity;

namespace Talabat2.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services works with APIs
            // Add services to the container.

            builder.Services.AddControllers();//To Add API Controller
                                              //(AddMVC : To Accept all Methods To All Project / AddControllersWithViews:To Add MVC Controllers / AddRazorPage : To Add Razor Page Controller)

            builder.Services.AddDbContext<StoreContext2>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Add DbContext For Identity
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //Using AddSingleton To Be Live With Session
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                //Create Connection String 
                var connection = builder.Configuration.GetConnectionString("RedisConnetionString");
                //return Object Of ConnectionMultiplexer Connected To Redis 
                return ConnectionMultiplexer.Connect(connection);
            });

            //Calling Identity Services
            builder.Services.AddIdentityServices(builder.Configuration);

            //Calling Services Of Application From It's Class
            builder.Services.AddApplicationServices();

            //Calling Services Of Swagger From It's Class SwaggerServicesExtentions
            builder.Services.AddSwaggerServices();


            #endregion

            var app = builder.Build();
            #region Update DataBase Inside Main
            //To Update DataBase With Code 
            //Create Variable Can Put Inside it All Services Which Work In Specific Scope 
            //create DbContext Explicitly
            var scope = app.Services.CreateScope();//Services Scoped :: :كدا انا مسكت اي حاجه شغاله scope
            var services = scope.ServiceProvider;//inject Objects To all Services which wait sepecific Object From Clr :: مسؤول انه يبعت ليا اي اوبجكت اي service مستنياه
            //To Log Errors
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext2>();//Ask CLR To Create Object From StoreContext2 Explicitly::جبت service معينه
                await dbContext.Database.MigrateAsync();//Update DateBase
                await StoreContext2Seed.SeedAsync(dbContext);

                var identity=services.GetRequiredService<AppIdentityDbContext>();
                await identity.Database.MigrateAsync();
                var UserManager= services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(UserManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();//عشان يقدر يعمل لوج ع مستوي ال بروجرم نفسه
                logger.LogError(ex, "An Error Ocuread During Apply Migrations");
                throw;
            } 
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure Request into Pipelines
            if (app.Environment.IsDevelopment())
            {
                //Calling Piplines Of Swagger From It's Class SwaggerServicesExtentions
                app.UseSwaggerIddlwares();

            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            //To Use Any Fiels In Program
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}