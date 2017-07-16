using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;
using DotNetify.Routing;

namespace WebApp.Controllers
{
   public class HomeController : Controller
   {
      private readonly IHostingEnvironment _hostingEnvironment;

      public HomeController(IHostingEnvironment hostingEnvironment)
      {
         _hostingEnvironment = hostingEnvironment;
      }

      [Route("{*id}")]
      [Authorize(ActiveAuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
      public IActionResult Index(string id)
      {
         if (string.IsNullOrEmpty(id))
            return RedirectToAction(nameof(App), "home");

         // If not ending with .js or .map, assume it's a request for static html file.
         if (!id.EndsWith(".js") && !id.EndsWith(".map"))
            id = Path.Combine(_hostingEnvironment.ContentRootPath, "Views\\" + (id.EndsWith(".html") ? id : id + ".html"));
         return File(id);
      }

      [Route("app/{*id}")]
      [Authorize(ActiveAuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
      public IActionResult App(string id) => File(Path.Combine(_hostingEnvironment.ContentRootPath, "Views\\index.html"));

      [Route("login")]
      [AllowAnonymous]
      public IActionResult Login()
      {
         // Revoke any current authentication prior to login.
         HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

         return File(Path.Combine(_hostingEnvironment.ContentRootPath, "Views\\login.html"));
      }

      [Route("auth")]
      public ActionResult AuthenticateGoogle() => new ChallengeResult("Google", new AuthenticationProperties() { RedirectUri = Url.Action("Index") });

      private FileStreamResult File(string path)
      {
         var mimeType = "text/plain";
         if (path.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
            mimeType = "text/js";
         else if (path.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            mimeType = "text/html";
         try
         {
            return File(System.IO.File.OpenRead(path), mimeType);
         }
         catch (Exception)
         {
            System.Diagnostics.Trace.WriteLine(path);
            return null;
         }
      }
   }
}