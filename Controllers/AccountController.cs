using _22_MVC_Identity.Models.DTOs;
using _22_MVC_Identity.Models.Entites.Concrete;
using _22_MVC_Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace _22_MVC_Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        //dependency ınejctionu program.cs'de servicse'ler yönetiyor. Oradan geliyor.
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = registerVM.UserName,
                    Email = registerVM.Email,
                };

                appUser.CreatedBy = "Zafer";

                IdentityResult id = await _userManager.CreateAsync(appUser, registerVM.Password);

                if (id.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError eror in id.Errors)
                    {
                        ModelState.AddModelError("", eror.Description);
                    }
                }

            }


            return View();

        }


        //geldiği sayfayı yakala lonig olmazsa bun sayfayı gönder.
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {

            returnUrl = returnUrl is null ? "Index" : returnUrl;
            return View(new LoginVM() { ReturnUrl = returnUrl });
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(loginVM.UserName);

                if (appUser is not null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(loginVM.ReturnUrl != null ? loginVM.ReturnUrl : Url.Action("Index", "Home"));


                    }

                    ModelState.AddModelError("", "Wrong credation şnformation..");

                }
            }
            else
            {
                ModelState.AddModelError("Kullanıcın Bulunamadı", "Girdiğinizi bilgilere Kullanıcı bulunamadı , Lütfen Tekrar Deneyiniz");
            }

            return View(loginVM);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Edit()
        {
            //edit login sonrası oldugu için findbynamedeki user içi dolu oldugundan oradaki kullanıcı yakalıyoruz.
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);


            UserUpdateDTO userUpdateDTO = new UserUpdateDTO()
            {
                Email = appUser.Email,
                Password = appUser.PasswordHash,
                UserName = appUser.UserName,

            };

            return View(userUpdateDTO);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateDTO userUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);


                appUser.UserName = userUpdateDTO.UserName;
                if (userUpdateDTO.Password is not null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, userUpdateDTO.Password);
                }
                IdentityResult result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                    TempData["Success"] = "Your profile has been edited!";
                else
                    TempData["Eror"] = "Your profile has not been edited!";

            }
            return View(userUpdateDTO);

        }
    }
}
