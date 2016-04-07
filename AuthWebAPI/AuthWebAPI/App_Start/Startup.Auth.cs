using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using AuthWebAPI.Providers;
using AuthWebAPI.Models;
using AuthWebAPI.Helpers;

namespace AuthWebAPI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/auth/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            if (OAuthProviderController.Microsoft.IsEnabled)
                app.UseMicrosoftAccountAuthentication(
                            clientId: OAuthProviderController.Microsoft.Key,
                            clientSecret: OAuthProviderController.Microsoft.Secret);

            if (OAuthProviderController.Twitter.IsEnabled)
                app.UseTwitterAuthentication(
                            consumerKey: OAuthProviderController.Twitter.Key,
                            consumerSecret: OAuthProviderController.Twitter.Secret);

            if (OAuthProviderController.Facebook.IsEnabled)
                app.UseFacebookAuthentication(
                            appId: OAuthProviderController.Facebook.Key,
                            appSecret: OAuthProviderController.Facebook.Secret);

            if (OAuthProviderController.Google.IsEnabled)
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = OAuthProviderController.Google.Key,
                    ClientSecret = OAuthProviderController.Google.Secret
                });
        }
    }
}
