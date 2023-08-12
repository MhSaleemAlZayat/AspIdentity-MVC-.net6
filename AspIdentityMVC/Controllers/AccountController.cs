using AspIdentityMVC.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspIdentityMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            try
            {
                return View(new Models.Account.LoginModel { ReturnUrl = returnUrl });
            }
            catch (Exception exp)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["LoginError"] = String
                        .Join(" ", 
                        ModelState.Values.ToList().SelectMany(v => v.Errors).ToList()
                        .Select(err => err.ErrorMessage).ToArray());
                    return RedirectToAction("Login", new { returnUrl = model.ReturnUrl });
                }

                //*/Use following code if you want to signin using user name.//*/

                //var signInResult = await _signInManager
                //    .PasswordSignInAsync(model.Username, model.Password, 
                //    model.RememberLogin, lockoutOnFailure: true);

                //*/Use following code if you want to signin using email//*/

                var user = await _userManager.FindByEmailAsync(model.Username);
                var signInResult = await _signInManager
                    .PasswordSignInAsync(user, model.Password,
                    model.RememberLogin, lockoutOnFailure: true);

                if (!signInResult.Succeeded)
                {
                    TempData["LoginError"] = "User name or passsword dit not correct.";
                    return RedirectToAction("Login", new { returnUrl = model.ReturnUrl });
                }
                
                return Redirect(model.ReturnUrl);
            }
            catch (Exception exp)
            {
                throw;
            }
        }


    }
}
