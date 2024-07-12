using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat2.Core.Entites.Identity;

namespace AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
        }

       

        public async Task<IActionResult> Index()
        {
            var Users = await userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DisplayName = u.DisplayName,
                Roles=userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(Users);
        }
        //Edit 
        //Get
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            // Get All Roles
            var AllRoles= await roleManager.Roles.ToListAsync();
            var userRoleViewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                UserName= user.DisplayName,
                Roles=AllRoles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected=userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            };
            return View(userRoleViewModel);
        }
        //Edit
        //Post
        [HttpPost]
         public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if(userRoles.Any(r=>r==role.Name)&& !role.IsSelected)
                {
                    await userManager.RemoveFromRoleAsync(user,role.Name);
                }
                if (!userRoles.Any(r => r==role.Name)&& role.IsSelected)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("Index");

        }
    }
}
