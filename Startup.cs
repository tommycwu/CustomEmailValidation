using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Okta.AspNet;
using System.Configuration;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(CustomEmailValidation.Startup))]

namespace CustomEmailValidation
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = new PathString("/WebForm1.aspx"),
            });

            app.UseOktaMvc(new OktaMvcOptions()
            {
                OktaDomain = ConfigurationManager.AppSettings["OktaDomain"],
                ClientId = ConfigurationManager.AppSettings["ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutUri"],
                AuthorizationServerId = ConfigurationManager.AppSettings["AuthZserver"],
                Scope = new List<string> { "openid", "profile", "email" },
                LoginMode = LoginMode.SelfHosted,
            });
        }
    }
}
