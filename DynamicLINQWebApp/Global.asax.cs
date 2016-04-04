using System;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetify;
using SimpleInjector;

namespace DynamicLINQWebApp
{
   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();
         RouteConfig.RegisterRoutes(RouteTable.Routes);

         // Register the assembly that has the view model.
         VMController.RegisterAssembly(typeof(MvcApplication).Assembly);

         // Using IoC container to inject dependencies.
         var container = new Container();
         VMController.CreateInstance = (type, args) => args == null ? container.GetInstance(type) : Activator.CreateInstance(type, args);
      }
   }
}
