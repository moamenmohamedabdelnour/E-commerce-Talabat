using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Errors;
using Talabat2.APIs.Extentions;
using Talabat2.Core.Entites.Identity;
using Talabat2.Core.Services;

namespace Talabat2.APIs.Controllers
{

    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService,IMapper mapper)
        {
            this.userManager=userManager;
            this.signInManager=signInManager;
            this.tokenService=tokenService;
            this.mapper=mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            //create UserDto To Create Specific Class To Return Specific Data Of User
            var User = await userManager.FindByEmailAsync(model.Email);
            if (User == null) return Unauthorized(new ApiErrorResponse(401));
            var Result = await signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiErrorResponse(401));
            return Ok(new UserDto()
            {
                DisplayName=User.DisplayName,
                Email=User.Email,
                Token=await tokenService.CreateTokenAsync(User,userManager)
            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExsist(model.Email).Result)
                return BadRequest(new ApiValiditionErrorResponse() { Errors= new string[] { "This Email Already Exsist" } });
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName=model.Email.Split('@')[0],
                PhoneNumber=model.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await tokenService.CreateTokenAsync(user, userManager)

            });
        }
        [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //first Get Current Email Of Current User by ClaimsPrinciples Which In Controler(User)
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            //map This User Which Return Of userManager To UserDto
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token= await tokenService.CreateTokenAsync(user,userManager)

            });
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserWithAddressByEmailAsync(User);
            var UserAddress= mapper.Map<Address,AddressDto>(user.Address);
            return Ok (UserAddress);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdatedAddress)
        {
            var address = mapper.Map<AddressDto,Address> (UpdatedAddress);
            var user = await userManager.FindUserWithAddressByEmailAsync(User);

            address.Id=user.Address.Id;
            user.Address = address;

            var result= await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            return Ok(UpdatedAddress);
        }

        [HttpGet("checkemail")]
        public async Task<bool> CheckEmailExsist(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }    
}
