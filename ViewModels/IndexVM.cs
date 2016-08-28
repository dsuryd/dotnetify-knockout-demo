using System.Collections.Generic;
using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class IndexVM : BaseVM, IRoutable
   {
      public class SideNavItem
      {
         public Route Route { get; set; }
         public string Caption { get; set; }
         public string Icon { get; set; }
      }

      public RoutingState RoutingState { get; set; }

      public string SignOut => "Sign Out";

      public string UserName { get; set; }

      public List<SideNavItem> SideNav => new List<SideNavItem>
      {
         new SideNavItem { Route = this.GetRoute("Home"), Caption = "Home", Icon = "fa fa-home btn-warning" },
         new SideNavItem { Route = this.GetRoute("Menu", "menu"), Caption = "Menu", Icon = "fa fa-list-alt btn-primary" },
         new SideNavItem { Route = this.GetRoute("Account"), Caption = "Account", Icon = "fa fa-user btn-primary" },
         new SideNavItem { Route = this.GetRoute("Help"), Caption = "Help", Icon ="fa fa-question-circle btn-positive" }
      };


      public IndexVM()
      {
         UserName = "Travis Lee";

         this.RegisterRoutes("app", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "Home", UrlPattern = "", Target = "MainPage", ViewUrl = "/home" },
            new RouteTemplate { Id = "Menu", UrlPattern = "menu(/:tab)", Target = "MainPage", ViewUrl = "/menu" },
            new RouteTemplate { Id = "Account", UrlPattern = "account", Target = "MainPage", ViewUrl = "/account" },
            new RouteTemplate { Id = "Help", UrlPattern = "help", Target = "MainPage", ViewUrl = "/help" }
         });

      }
   }
}
