using _22_MVC_Identity.Models.Entites.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _22_MVC_Identity.Controllers
{
    [Authorize(Roles ="admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View(_userManager.Users.ToList());
        }
    }
}
