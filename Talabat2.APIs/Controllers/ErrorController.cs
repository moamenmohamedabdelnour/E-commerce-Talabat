using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Errors;

namespace Talabat2.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        //This Class Have One EndPoint Which Use It If Ask For EndPoint Not Found Will Direct Me TO This EndPoint
        public ActionResult Errors(int code)
        {
            return NotFound(new ApiErrorResponse(code));
        }
    }
}
