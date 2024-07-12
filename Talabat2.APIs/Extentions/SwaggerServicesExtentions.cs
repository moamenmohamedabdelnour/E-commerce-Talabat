namespace Talabat2.APIs.Extentions
{
    public static class SwaggerServicesExtentions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(); //Generate Swagger Page
            return Services;
        }
        public static WebApplication UseSwaggerIddlwares(this WebApplication app)
        {
            //Custome Pipline person Create it
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
