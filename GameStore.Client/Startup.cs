using System;
using System.Data.Entity;
using System.Threading.Tasks;
using GameStore.Client.Utils;
using GameStore.DAL;
using Microsoft.AspNet.Identity;
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
        }
    }
}
