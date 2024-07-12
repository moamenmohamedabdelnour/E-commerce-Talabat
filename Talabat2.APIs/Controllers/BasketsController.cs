using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Errors;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;

namespace Talabat2.APIs.Controllers
{
    
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository=basketRepository;
            this.mapper=mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string id)
        {
            var basket= await basketRepository.GetBasketAsync(id);
            //if Not Find Basket To Tis User Not Return Null Or Exception Noooo Return Empty Basket Else Return UserCustomer
            return basket is null ? new CustomerBasket(id) : basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>>UpdateCreatebasket(CustomerBasketDto basket)
        {
            var MappedBasket= mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreatedOrUpdatedBasket=await basketRepository.UpdateBasketAsync(MappedBasket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiErrorResponse(400));
            return Ok (CreatedOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>>DeleteBasket(string id)
        {
            return await basketRepository.DeleteBasketAsync(id);
        }
    }
}
