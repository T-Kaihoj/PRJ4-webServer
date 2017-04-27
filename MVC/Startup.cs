using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MVC.OwinStartup))]

namespace MVC
{
    [ExcludeFromCodeCoverage]
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Set the cookie options. 
            var cookieOptions = new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieSecure = CookieSecureOption.Never,
                ExpireTimeSpan = TimeSpan.FromMinutes(20),
                LoginPath = new PathString("/"),
                SlidingExpiration = true
            };

            app.UseCookieAuthentication(cookieOptions);
        }
    }
}
