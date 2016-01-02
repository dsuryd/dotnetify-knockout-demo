using System;
using System.Web.Mvc;

namespace DynamicLINQWebApp
{
   public class HomeController : Controller
   {
      public ActionResult Index(string id)
      {
         return File(Server.MapPath("/Views/BunnyPen.html"), "text/html");
      }
   }
}
