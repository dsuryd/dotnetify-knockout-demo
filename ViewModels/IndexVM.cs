using System.Collections.Generic;
using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class IndexVM : BaseVM, IRoutable
   {
      public RoutingState RoutingState { get; set; }

      public Route Page1 => this.GetRoute("Page1");
      public Route Page2 => this.GetRoute("Page2");
      public Route Page3 => this.GetRoute("Page3");

      public string Page1Title => "Home";
      public string Page2Title => "Page 2";
      public string Page3Title => "My Settings";

      public IndexVM()
      {
         this.RegisterRoutes("app", new List<RouteTemplate>
      {
         new RouteTemplate { Id = "Page1", UrlPattern = "page1", Target = "content", ViewUrl = "/page1" },
         new RouteTemplate { Id = "Page2", UrlPattern = "page2", Target = "content", ViewUrl = "/page2" },
         new RouteTemplate { Id = "Page3", UrlPattern = "page3", Target = "content", ViewUrl = "/page3" }
      });

      }
   }
}
