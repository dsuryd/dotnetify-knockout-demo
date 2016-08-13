using Microsoft.Owin;
using Owin;
using System.Reflection;
using TinyIoC;
using Domain.Repository.Interfaces;
using Service.Interfaces;
using DataAccess.Repositories;
using Services;

[assembly: OwinStartup(typeof(WebApp.OWINStartup))]

namespace WebApp
{
   public class OWINStartup
   {
      public void Configuration( IAppBuilder app )
      {
         app.MapSignalR();

         DotNetify.VMController.RegisterAssembly(Assembly.Load("ViewModels"));

         DotNetify.VMController.CreateInstance = ( type, args ) => TinyIoCContainer.Current.Resolve(type);

         var container = TinyIoCContainer.Current;
         container.Register<IMenuRepository, MenuRepository>();
         container.Register<IMenuService, MenuService>();
      }
   }
}
