using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Errors;
using Talabat2.Core;
using Talabat2.Core.Entites.Order_Aggregation;
using Talabat2.Core.Services;

namespace Talabat2.APIs.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService,IMapper mapper )
        {
           
            this.orderService=orderService;
            this.mapper=mapper;
        }
        [ProducesResponseType( typeof( Order ), StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest )]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto OrderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var ShippingAddress= mapper.Map<AddressDto,Address>(OrderDto.ShippingAddress);
            var Order = await orderService.CreateOrderAsync(buyerEmail,OrderDto.BasketId,OrderDto.DeliveryMethodId,ShippingAddress);
            if (Order ==null) return BadRequest(new ApiErrorResponse(400));
            return Ok(Order);


        }
        [HttpGet]
        public async Task<ActionResult<Order>> GetAllOrderForUser()
        {
            var buyerEmail= User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(orders);
        }
        [ProducesResponseType(typeof (Order), StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ApiErrorResponse ), StatusCodes.Status404NotFound )]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>>GetOrderByIdForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order=await orderService.GetOrderByIdForUserAsync(id,buyerEmail);
            if (order == null) return NotFound(new ApiErrorResponse(404));
            return Ok(order);
        }
        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliverymethods= await orderService.GetDeliveryMethodsAsync();
            return Ok(deliverymethods);
        }
    }
}
