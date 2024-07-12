using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Errors;
using Talabat2.Core.Services;

namespace Talabat2.APIs.Controllers
{
    [Authorize]
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService=paymentService;
        }
        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
        [HttpPost("{basketid}")]
        public async Task<ActionResult<CustomerBasketDto>>CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await paymentService.CreateOrUpdatePaymentIntent(basketid);
            if (basket == null) return BadRequest(new ApiErrorResponse(400,"A Problem With Your Basket"));
            return Ok(basket);
        }
    }
}
