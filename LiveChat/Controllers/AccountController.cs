using LiveChat.Models.Entities;
using LiveChat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(
            UserManager<StoredUser> userManager,
            SignInManager<StoredUser> signInManager
            )
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public UserManager<StoredUser> _UserManager { get; }
        public SignInManager<StoredUser> _SignInManager { get; }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = ReturnUrl ?? "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var userLoggingIn = await _UserManager.FindByEmailAsync(model.Email);
                var signInResult=await _SignInManager.PasswordSignInAsync(
                    userLoggingIn,model.Password, model.RememberMy,false);

                if(signInResult == Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                        return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("Login", "Usuario o contraseña incorrecto.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
