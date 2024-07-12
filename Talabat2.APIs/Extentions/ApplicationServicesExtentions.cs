using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Errors;
using Talabat2.APIs.Helper;
using Talabat2.Core;
using Talabat2.Core.Repositories;
using Talabat2.Core.Services;
using Talabat2.Repository;
using Talabat2.Service;

namespace Talabat2.APIs.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) 
        {
            Services.AddScoped<IPaymentService,PaymentService>();
            Services.AddScoped<IOrderService,OrderService>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Any Call To IGenericRepo Will Get GenericRepo Of Same Type Must Be Inherit From BaseEntity
           // Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped<IBasketRepository, BasketRepository>();

            #region Handel ValidationError
            //To Change Behavior Of Validation Error Response 
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                //by Next Line Will See In case of  Invalid ModelState catch actionContext 
                options.InvalidModelStateResponseFactory= (actionContsxt) =>
                {
                    //from actionContext If ModelState Of Errors > Zero Will Select All Objects Of Errors Then Select From Each Object ErrorMessage then ToArray
                    var errors = actionContsxt.ModelState.Where(P => P.Value.Errors.Count()>0)
                                                            .SelectMany(P => P.Value.Errors)
                                                            .Select(E => E.ErrorMessage).ToArray();
                    //To Full Errors array In ApiValiditionErrorResponse Create Object Of It And Errors = errors We Catches in Perivouse Code
                    var ValidationErrorResponse = new ApiValiditionErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            #endregion
            return Services;

        }
    }
}
