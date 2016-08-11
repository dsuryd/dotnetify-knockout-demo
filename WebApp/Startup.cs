using Microsoft.Owin;
using Owin;
using System.Reflection;

[assembly: OwinStartup(typeof(WebApp.OWINStartup))]

namespace WebApp
{
   public class OWINStartup
   {
      public void Configuration( IAppBuilder app )
      {
         app.MapSignalR();

         DotNetify.VMController.RegisterAssembly(Assembly.Load("ViewModels"));
      }
   }
}
