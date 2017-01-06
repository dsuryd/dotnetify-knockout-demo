using System;
using System.Reflection;
using DataAccess.Repositories;
using Domain.Repository.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
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

         app.Map("/signalr", map =>
         {
            map.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration
            {
               // You can enable JSONP by uncommenting line below.
               // JSONP requests are insecure but some older browsers (and some
               // versions of IE) require JSONP to work cross domain
               // EnableJSONP = true
            };
            map.RunSignalR(hubConfiguration);
         });

         DotNetify.VMController.RegisterAssembly(Assembly.Load("ViewModels"));

         DotNetify.VMController.CreateInstance = ( type, args ) => TinyIoCContainer.Current.Resolve(type);

         var container = TinyIoCContainer.Current;
         container.Register<IMenuRepository, MenuRepository>().AsMultiInstance();
         container.Register<IMenuService, MenuService>().AsMultiInstance();
         container.Register<IShoppingCartService, ShoppingCartService>().AsMultiInstance();
         container.Register<IAccountService, AccountService>().AsMultiInstance();
         container.Register<IUserCache, UserCache>().AsSingleton();
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
