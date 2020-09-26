using IOWebFramework.Core.Models.Account;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models.Identity;
using IOWebFramework.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IOWebFramework.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRepository repo;

        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<ApplicationRole> _roleManager,
            IRepository _repo)
        {
            repo = _repo;
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                bool userIsFromAD = false;
                    userIsFromAD = ActiveDirectoryHelper.LdapLogin(model.UserName, model.Password);
              
                if (!userIsFromAD)
                {
                    ModelState.AddModelError(string.Empty, "Невалидно потребителско име или парола");
                    return View(model);
                }

                var userInfo = ActiveDirectoryHelper.GetUserInfo(model.UserName, model.Password);

                ApplicationUser loggedUserFromDB;
                IdentityResult result = null;

                loggedUserFromDB = userManager.FindByNameAsync(model.UserName).GetAwaiter().GetResult();

                if (loggedUserFromDB == null)
                {
                    loggedUserFromDB = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Email = userInfo["mail"],
                    };

                    result = userManager.CreateAsync(loggedUserFromDB).GetAwaiter().GetResult();
                }

                //Entry point to System
                if (((result != null) && result.Succeeded) || loggedUserFromDB != null)
                {
                    var roleNamesAttachedToPrincipal = userManager.GetRolesAsync(loggedUserFromDB).Result;

                    if (!roleNamesAttachedToPrincipal.Any() && (!roleNamesAttachedToPrincipal.Contains(CommonConstant.Role.HR)
                        || !roleNamesAttachedToPrincipal.Contains(CommonConstant.Role.Employee)))
                    {
                        var positionFromAD = userInfo["department"].ToLower();
                        var isHumanResource = positionFromAD.ToLower().Contains(CommonConstant.HRSubstring); // Дирекция "Човешки ресурси и администрация"
                        var isMSDeveloper = positionFromAD.ToLower().Contains(CommonConstant.MSDeveloperSubstring); // Отдел Microsoft технологии

                        IdentityResult addingRolesResult;

                        if (isHumanResource || isMSDeveloper)
                        {
                            addingRolesResult = userManager.AddToRoleAsync(loggedUserFromDB, CommonConstant.Role.HR).Result;

                        }
                        else
                        {
                            addingRolesResult = userManager.AddToRoleAsync(loggedUserFromDB, CommonConstant.Role.Employee).Result;
                        }

                        if (addingRolesResult == null || !addingRolesResult.Succeeded)
                        {
                            AddErrors(addingRolesResult);
                        }

                    }

                    //set cookie in browser
                    await signInManager.SignInAsync(loggedUserFromDB, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult LogOff()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return Json("ok");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl)
        {
            TempData[Core.Constants.MessageConstant.ErrorMessage] = Core.Constants.MessageConstant.Values.Unauthorized;
            return LocalRedirect("/");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}