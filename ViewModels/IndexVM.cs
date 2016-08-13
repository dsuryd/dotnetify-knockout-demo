using System.Collections.Generic;
using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class IndexVM : BaseVM, IRoutable
   {
      public class SideMenuItem
      {
         public Route Route { get; set; }
         public string Caption { get; set; }
         public string Icon { get; set; }
      }

      public RoutingState RoutingState { get; set; }

      public Route Page1 => this.GetRoute("Page1");
      public Route Page2 => this.GetRoute("Page2");
      public Route Page3 => this.GetRoute("Page3");

      public string Page1Title => "Home";
      public string Page2Title => "Page 2";
      public string Page3Title => "My Settings";

      public string Title => "My Take-out App";

      public string UserName { get; set; }

      public List<SideMenuItem> Menu => new List<SideMenuItem>
      {
         new SideMenuItem { Route = this.GetRoute("Home"), Caption = "Home", Icon = "fa fa-home btn-warning" },
         new SideMenuItem { Route = this.GetRoute("Menu"), Caption = "Menu", Icon = "fa fa-list-alt btn-primary" },
         new SideMenuItem { Route = this.GetRoute("Account"), Caption = "Account", Icon = "fa fa-user btn-primary" },
         new SideMenuItem { Route = this.GetRoute("Help"), Caption = "Help", Icon ="fa fa-question-circle btn-positive" }
      };


      public IndexVM()
      {
         UserName = "Travis Lee";

         this.RegisterRoutes("app", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "Page1", UrlPattern = "page1", Target = "MainPage", ViewUrl = "/page1" },
            new RouteTemplate { Id = "Page2", UrlPattern = "page2", Target = "MainPage", ViewUrl = "/page2" },
            new RouteTemplate { Id = "Page3", UrlPattern = "page3", Target = "MainPage", ViewUrl = "/page3" },

            new RouteTemplate { Id = "Home", UrlPattern = "", Target = "MainPage", ViewUrl = "/home" },
            new RouteTemplate { Id = "Menu", UrlPattern = "menu", Target = "MainPage", ViewUrl = "/menu" },
            new RouteTemplate { Id = "Account", UrlPattern = "account", Target = "MainPage", ViewUrl = "/account" },
            new RouteTemplate { Id = "Help", UrlPattern = "help", Target = "MainPage", ViewUrl = "/help" }
         });

      }
   }
}
