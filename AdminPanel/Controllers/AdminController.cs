using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat2.APIs.Dtos;
using Talabat2.Core.Entites.Identity;

namespace AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AdminController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager=userManager;
            this.signInManager=signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        //Login
        [HttpPost]
        public async Task<IActionResult>Login(LoginDto login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Eamil");
                return RedirectToAction(nameof(Login));
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password,false);

            if (!result.Succeeded ||! await userManager.IsInRoleAsync(user,"Admin") )
            {
                ModelState.AddModelError(string.Empty, "You Are Not Authorized");
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        //LogOut
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
