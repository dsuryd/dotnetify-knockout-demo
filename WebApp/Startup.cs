using System;
using System.Reflection;
using DataAccess.Repositories;
using Domain.Repository.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Service.Interfaces;
using Services;
using TinyIoC;

[assembly: OwinStartup(typeof(WebApp.OWINStartup))]

namespace WebApp
{
   public class OWINStartup
   {
      public void Configuration( IAppBuilder app )
      {
         ConfigureAuth(app);
         app.MapSignalR();

         DotNetify.VMController.RegisterAssembly(Assembly.Load("ViewModels"));

         DotNetify.VMController.CreateInstance = ( type, args ) => TinyIoCContainer.Current.Resolve(type);

         var container = TinyIoCContainer.Current;
         container.Register<IMenuRepository, MenuRepository>();
         container.Register<IMenuService, MenuService>();
      }

      public void ConfigureAuth( IAppBuilder app )
      {
         var cookieOptions = new CookieAuthenticationOptions { LoginPath = new PathString("/login") };
         app.UseCookieAuthentication(cookieOptions);
         app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType);

         app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
         {
            ClientId = "378148702389-b804945knu3j8smj9202htuse2phfl74.apps.googleusercontent.com",
            ClientSecret = "C0n9v98_8pa0bYyQ94p9_Rcr"
         });
      }
   }
}
