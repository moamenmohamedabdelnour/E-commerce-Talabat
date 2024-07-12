using AdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager=roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Roles= await roleManager.Roles.ToListAsync();
            return View(Roles);
        }
        //EndPoint To Create [Add] Role 
        [HttpPost]
        public async Task<IActionResult>Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var RoleExsist = await roleManager.RoleExistsAsync(model.Name);
                if(!RoleExsist)
                {
                    await roleManager.CreateAsync(new IdentityRole( model.Name.Trim()));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exist !!");
                    return View("Index", await roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction("Index");
        }
        //Delete Role 
        public async Task<IActionResult>Delete(string id)
        {
            var role =await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
        //Edit Role 
        //First Create View Model To Role To Edit
        //Get
        public async Task<IActionResult>Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var MappedRole = new RoleViewModel()
            {
                Name = role.Name
            };
            return View(MappedRole);

        }
        //Edit 
        //Post
        [HttpPost]
        public async Task<IActionResult>Edit(string id , RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var RoleExists= await roleManager.RoleExistsAsync(model.Name);
                if(!RoleExists)
                {
                    var role = await roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Name;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof (Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exist !!");
                    return View("Index", await roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction("Index");

        
        }
    }
}
