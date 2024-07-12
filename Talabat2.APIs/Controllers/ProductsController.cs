using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Errors;
using Talabat2.APIs.Helpers;
using Talabat2.Core;
using Talabat2.Core.Entites;
using Talabat2.Core.Repositories;
using Talabat2.Core.Specifications;
using Talabat2.Repository;

namespace Talabat2.APIs.Controllers
{
    public class ProductsController : ApiBaseController
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        //To Use Methods Must Inject IGenericRepository By Constructor
        //Develope Against Interface Not Concreate Class
        public ProductsController(IUnitOfWork unitOfWork
                                  ,IMapper mapper)
        {
            
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpec)
        {

            //static Methods To return Products 
            // var products= await productRepo.GetAllAsync();

            //So Will Use Dynamic Method With Dynamic Query

            //var spec = new BaseSpecification<Product>();//by using BaseSpecification We Cannot Includes Members To Products
            //Becouse it's Generic So We Should Cretat ProductSpecification To Using Include And Where
            
            //will Pass sorting method To ProductSpecification To Allow Sorting If Found
            var spec = new ProductSpecification(productSpec);
            var products=await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var MappedProducts=mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFilterationForCountSpecification(productSpec);
            var count = await unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(productSpec.PageIndex,productSpec.PageSize, count,MappedProducts));
        }
        //to Refactor Swagger Documentation To Show Which This EndPoint Can Return Case Of (Ok) and (When Not Found)
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductSpecification(id);

            var product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiErrorResponse(404));
            var MappedProduct=mapper.Map<Product,ProductToReturnDto>(product);
            return Ok(MappedProduct);
        }
       //Add Fregment To Diffrent Between Same Method Type 
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList< ProductType>>> GetAllTypes()
        {
            var Types=await unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok (Types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList< ProductBrand>>> GetAlltBrands()
        {
            var Brands = await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }

    }
}
