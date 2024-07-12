using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Talabat2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        //Commen EndPoints 
    
        //To Not Repaete ( [Route("api/[controller]")] ,    [ApiController]) Each New Controller

        // All New Controller Will Inherit From ApiBaseController

    }
}
