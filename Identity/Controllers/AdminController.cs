using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager { get; set; }

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser() 
                {
                  Email = user.Email,
                  UserName = user.Name
                };

                IdentityResult identityResult = await userManager.CreateAsync(appUser , password:user.Password);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else {
                    foreach (IdentityError error in identityResult.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
    }
}
