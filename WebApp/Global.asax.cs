using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();
         RouteTable.Routes.MapMvcAttributeRoutes();
      }
   }
}
