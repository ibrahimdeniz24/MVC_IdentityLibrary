using _22_MVC_Identity.Models.DTOs;
using _22_MVC_Identity.Models.Entites.Concrete;
using _22_MVC_Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _22_MVC_Identity.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        //Rolleri yönet, rol listele, ekle,sil,güncelle

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var roles = _roleManager.Roles.Select(r => new { r.Id, r.Name }).ToList();

            List<RoleVM> rolesVMList = new List<RoleVM>();

            foreach (var item in roles)
            {
                rolesVMList.Add(new RoleVM { Id = item.Id, Name = item.Name });
            }

            return View(rolesVMList);
        }


        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM createRoleVM)
        {
            if (ModelState.IsValid) //Role eklerken name doldurulmuşu kontrol eder.
            {
                IdentityRole<Guid> identityRole = await _roleManager.FindByNameAsync(createRoleVM.Name); //Rol veri tabanını daha ön ce mevcut mu kontrol ediyor. aynısı birdaha eklenmesin diye

                if (identityRole == null) //veri tabanında bu isimde bir rol yoktur.
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = createRoleVM.Name });

                    if (result.Succeeded)
                    {
                        TempData["Succeeded"] = "The role has been createad!";
                        return RedirectToAction("Index"); //role list sayfasına git.
                    }
                    else
                    {
                        foreach (var eror in result.Errors)
                        {
                            ModelState.AddModelError("", eror.Description);
                            TempData["Eror"] = eror.Description;
                        }
                    }
                }
                else //Veritabanında bu isimde bir rol varsa tempdata ile mesaj gönderebiliyoruz.
                {

                    TempData["Message"] = "Bu isimde bir rol mevcut";
                }


            }
            return View(createRoleVM);

        }

        public async Task<IActionResult> AssignedUser(string id)
        {
            IdentityRole<Guid> identityRole = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, identityRole.Name) ? hasRole : hasNotRole;

                list.Add(user);
            }
            AssignedRoleDTO assignedRoleDTO = new AssignedRoleDTO()
            {

                Role = identityRole,
                HasRole = hasRole,
                HasNotRole = hasNotRole,
                RoleName = identityRole.Name

            };

            return View(assignedRoleDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO assignedRoleDTO)
        {
            foreach (var userId in assignedRoleDTO.AddIds ?? new string[] { })//model.AddIds null gelirse diye "?? string[] {]" ekliyoruz.
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);// multiplactiveresultsets = true appsettings.json ekle yoksa pakanmayan connection hatası veriyor.

                IdentityResult result = await _userManager.AddToRoleAsync(appUser, assignedRoleDTO.RoleName);

                //if(result.Succeeded) yazabiliriz.

            }

            foreach (var userId in assignedRoleDTO.DeleteIds ?? new string[] { })//model.AddIds null gelirse diye "?? string[] {]" ekliyoruz.
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                IdentityResult result = await _userManager.RemoveFromRoleAsync(appUser, assignedRoleDTO.RoleName);

                //if(result.Succeeded) yazabiliriz.

            }

            return RedirectToAction("Index");

        }

    }
}
