using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kimlik.BLL.Account;
using Kimlik.Models.IdentityModels;
using Kimlik.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using static Kimlik.BLL.Account.MembershipTools;

namespace Kimlik.Web.MVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userManager = MembershipTools.NewUserManager();
            var checkUser = userManager.FindByName(model.UserName);
            if (checkUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten alınmış.");
                return View(model);
            }
            checkUser = userManager.FindByEmail(model.Email);
            if (checkUser != null)
            {
                ModelState.AddModelError("", "Bu e-mail zaten alınmış.");
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname
            };
            var result = userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, userManager.Users.Count() == 1 ? "Admin" : "User");
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "Kayıt işleminde bir hata oluştu.");
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userManager = NewUserManager();
            var user = await userManager.FindAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity =
                await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = model.RememberMe
            },userIdentity);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}