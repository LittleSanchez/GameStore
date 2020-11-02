using System;
using System.Data.Entity;
using System.Threading.Tasks;
using GameStore.Client.Utils;
using GameStore.DAL;
using GameStore.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GameStore.Client.Startup))]

namespace GameStore.Client
{
    public class Startup
    {
        //Install: Microsoft.Aspnet.Identity.Core
        //Install: Microsoft.Aspnet.Identity.EntityFramework

        // Microsoft.OWIN.SystemWeb
        // Microsoft.AspNet.Identity.OWIN
        // MS.OWIN

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());

            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.CreatePerOwinContext<AppSigninManager>(AppSigninManager.Create);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });

            InitUser();
        }

        private void InitUser()
        {
            var userStore = new UserStore<User>(new ApplicationContext());
            var userManager = new AppUserManager(userStore);

            var role = new IdentityRole
            {
                Name = "Admin"
            };

            var roleStore = new RoleStore<IdentityRole>(new ApplicationContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(role);

            var user = new User
            {
                UserName = "rick",
                Email = "rick@example.com"
            };

            var admin = new User
            {
                UserName = "ricardo",
                Email = "ricardo_milos@root.net"
            };
            userManager.Create(user, "Qwerty123");
            userManager.Create(admin, "Ricardo123");

            userManager.AddToRole(userManager.FindByName("ricardo").Id, "Admin");

        }
    }
}
