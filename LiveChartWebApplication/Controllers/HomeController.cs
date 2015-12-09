using System;
using System.Web.Mvc;

namespace LiveChartWebApplication
{
   public class HomeController : Controller
   {
      public ActionResult Index(string id)
      {
         return File(Server.MapPath("/Views/LiveChart.html"), "text/html");
      }
   }
}
