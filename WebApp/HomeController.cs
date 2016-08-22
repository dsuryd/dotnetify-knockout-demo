using System;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Web.Mvc;
using DotNetify.Routing;

namespace WebApp.Controllers
{
   public class HomeController : Controller
   {
      [Route("{*id}")]
      [Authorize]
      public ActionResult Index( string id )
      {
         if ( string.IsNullOrEmpty(id) )
            return RedirectToAction(nameof(App));

         return File(Server.MapPath("/views/" + ( id.EndsWith(".html") ? id : id + ".html" )), "text/html");
      }

      [Route("app/{*id}")]
      [Authorize]
      public ActionResult App( string id )
      {
         return File(Server.MapPath("/views/index.html"), "text/html");
      }

      [Route("login")]
      public ActionResult Login()
      {
         // Revoke any current authentication prior to login.
         Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);

         return File(Server.MapPath("/views/login.html"), "text/html");
      }

      [Route("auth")]
      public ActionResult AuthenticateGoogle()
      {
         return new ChallengeResult("Google", Url.Action("Index"));
      }
   }

   /// <summary>
   /// Causes the OWIN middleware to challenge the caller to authenticate.
   /// </summary>
   internal class ChallengeResult : HttpUnauthorizedResult
   {
      public string LoginProvider { get; set; }
      public string RedirectUri { get; set; }

      public ChallengeResult( string provider, string redirectUri )
      {
         LoginProvider = provider;
         RedirectUri = redirectUri;
      }

      public override void ExecuteResult( ControllerContext context )
      {
         var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
         context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
      }
   }
}