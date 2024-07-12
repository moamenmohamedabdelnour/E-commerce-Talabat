using System.Net;
using System.Text.Json;
using Talabat2.APIs.Errors;

namespace Talabat2.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        //To Slove Problem Of SereverError

        //any MiddleWare use 
        //--1)RequestDelegete => Using For Invoke Request if it's Ok Compelete Else Throw Exception
        //--2)ILogger To Log Error Will Appered Will Apper That Error In ConsoleApplication Becouse I Run In Kesteral
        //--3)IHostEnviorment 
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.next=next;
            this.logger=logger;
            this.env=env;
        }
        //HttpContext Special To Use Request From It 
        public async Task InvokeAsync(HttpContext context)
        {
            //By This I Have Request Which Will Go Through Piplines
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                //Will Apper Error In ConsoleApplication(Kesteral) As Error And ErrorMessage
                logger.LogError(ex, ex.Message);

                //To Show Error As JsonFile To FrontEnd User 
                context.Response.ContentType = "application/json";
                context.Response.StatusCode= (int) HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy=JsonNamingPolicy.CamelCase
                };
                var respone = env.IsDevelopment() ?
                             new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                             :
                             new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message);
                var json=JsonSerializer.Serialize(respone,options);
                await context.Response.WriteAsync(json);
                             
            }
        }
    }
}
