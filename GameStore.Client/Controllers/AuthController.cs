using GameStore.Client.Models;
using GameStore.Client.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace GameStore.Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper mapper;

        public AuthController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var signInManager = HttpContext.GetOwinContext().Get<AppSigninManager>();
            var status = signInManager.PasswordSignIn(model.Username, model.Password, false, false);
            if (status == SignInStatus.Success)
            { 
                return RedirectToAction("Index", "Games");
            }
            return Content("Can't log in");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }

            }
            return RedirectToAction("Register");
        }

        public async Task<ActionResult> Profile()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var userId = User.Identity.GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Auth");
            var user = userManager.FindById(userId);
            var model = mapper.Map<UserViewModel>(user);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var userId = User.Identity.GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Auth");
            var user = userManager.FindById(userId);
            var model = mapper.Map<UserViewModel>(user);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditProfile(UserViewModel model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var userId = User.Identity.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            var result = await userManager.UpdateAsync(user);
            return RedirectToAction("Profile");
        }

        public async Task<ActionResult> Logout()
        {
            var signInManager = HttpContext.GetOwinContext().Get<AppSigninManager>();
            signInManager.AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}