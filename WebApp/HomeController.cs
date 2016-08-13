using System;
using System.Web.Mvc;
using DotNetify.Routing;

namespace WebApp.Controllers
{
   public class HomeController : Controller
   {
      [Route("{*id}")]
      public ActionResult Index( string id )
      {
         if ( string.IsNullOrEmpty(id) )
            return RedirectToAction(nameof(App));

         return File(Server.MapPath("/views/" + ( id.EndsWith(".html") ? id : id + ".html" )), "text/html");
      }

      [Route("app/{*id}")]
      public ActionResult App( string id )
      {
         return File(Server.MapPath("/views/index.html"), "text/html");
      }
   }
}