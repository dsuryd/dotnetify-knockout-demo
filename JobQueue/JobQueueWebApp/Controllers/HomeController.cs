using System;
using System.Web.Mvc;

namespace JobQueueWebApp
{
   public class HomeController : Controller
   {
      public ActionResult Index(string id)
      {
         return File(Server.MapPath("/Views/JobQueue.html"), "text/html");
      }
   }
}
