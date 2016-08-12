using System;
using System.Web.Mvc;
using DotNetify.Routing;

namespace WebApp.Controllers
{
   public class HomeController : Controller
   {
      [Route( "{*id}" )]
      public ActionResult Index( string id )
      {
         if ( string.IsNullOrEmpty( id ) )
            id = "index";

         return File( Server.MapPath( "/Views/" + ( id.EndsWith( ".html" ) ? id : id + ".html" ) ), "text/html" );
      }

      [Route( "app/{*id}" )]
      public ActionResult App( string id )
      {
         return Index( null );
      }
   }
}