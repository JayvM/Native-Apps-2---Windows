﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(NativeAppsII_Services.Startup1))]

namespace NativeAppsII_Services
{
    public class Startup1
    { 
        /// OAUTH options property.  
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; } 
        /// Public client ID property.  
        public static string PublicClientId { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            
            // Enable the application to use a cookie to store information for the signed in user  
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider  
            // Configure the sign in cookie  
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/LogOff"),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow  
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new AppOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                AllowInsecureHttp = true //Don't do this in production ONLY FOR DEVELOPING: ALLOW INSECURE HTTP!  
            };

            // Enable the application to use bearer tokens to authenticate users  
            app.UseOAuthBearerTokens(OAuthOptions);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.  
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.  
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.  
            // This is similar to the RememberMe option when you log in.  
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers  
            //app.UseMicrosoftAccountAuthentication(  
            //    clientId: "",  
            //    clientSecret: "");  
        }
    }
}
