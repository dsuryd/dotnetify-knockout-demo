using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LiveChartWebApplication.Core.Controllers
{
   public class HomeController : Controller
   {
      private readonly IHostingEnvironment _hostingEnvironment;

      public HomeController(IHostingEnvironment hostingEnvironment)
      {
         _hostingEnvironment = hostingEnvironment;
      }

      public IActionResult Index()
      {
         var htmlFile = Path.Combine(_hostingEnvironment.ContentRootPath, "Views\\LiveChart.html");
         return File(System.IO.File.OpenRead(htmlFile), "text/html");
      }
   }
}
