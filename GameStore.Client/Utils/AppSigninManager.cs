using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Client.Utils
{
    public class AppSigninManager: SignInManager<IdentityUser, string>
    {
        public AppSigninManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {

        }

        public static AppSigninManager Create(IdentityFactoryOptions<AppSigninManager> options,
                                                IOwinContext owinContext)
        {
            var userManager = owinContext.GetUserManager<AppUserManager>();
            var signInManager = new AppSigninManager(userManager, owinContext.Authentication);

            return signInManager;
        }
    }
}