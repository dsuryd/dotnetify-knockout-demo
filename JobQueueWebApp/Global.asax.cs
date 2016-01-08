using System.Web.Mvc;
using System.Web.Routing;
using DotNetify;

namespace JobQueueWebApp
{
   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();
         RouteConfig.RegisterRoutes(RouteTable.Routes);

         VMController.RegisterAssembly(typeof(MvcApplication).Assembly);
      }
   }
}
