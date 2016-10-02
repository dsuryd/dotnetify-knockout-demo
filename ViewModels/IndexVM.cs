using System;
using System.Collections.Generic;
using System.Threading;
using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;

namespace ViewModels
{
   public class IndexVM : BaseVM, IRoutable
   {
      private readonly IShoppingCartService _shoppingCartService;

      public class SideNavItem
      {
         public Route Route { get; set; }
         public string Caption { get; set; }
         public string Icon { get; set; }
      }

      public RoutingState RoutingState { get; set; }

      public string SignOut => "Sign Out";

      public string UserName => Thread.CurrentPrincipal?.Identity?.Name;

      public string CartLocalData
      {
         get { return Get<string>(); }
         set { _shoppingCartService.DeserializeShoppingCart(value); }
      }

      public Route CartRoute => this.GetRoute("Cart");

      public List<SideNavItem> SideNav => new List<SideNavItem>
      {
         new SideNavItem { Route = this.GetRoute("Home"), Caption = "Home", Icon = "fa fa-home btn-warning" },
         new SideNavItem { Route = this.GetRoute("Menu", "menu"), Caption = "Menu", Icon = "fa fa-list-alt btn-primary" },
         new SideNavItem { Route = this.GetRoute("Account"), Caption = "Account", Icon = "fa fa-user btn-primary" },
         new SideNavItem { Route = this.GetRoute("Help"), Caption = "Help", Icon ="fa fa-question-circle btn-positive" }
      };

      public IndexVM(IShoppingCartService shoppingCartService)
      {
         _shoppingCartService = shoppingCartService;

         this.RegisterRoutes("app", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "Home", UrlPattern = "", Target = "MainPage", ViewUrl = "/home" },
            new RouteTemplate { Id = "Menu", UrlPattern = "menu(/:tab)", Target = "MainPage", ViewUrl = "/menu" },
            new RouteTemplate { Id = "Account", UrlPattern = "account", Target = "MainPage", ViewUrl = "/account" },
            new RouteTemplate { Id = "Help", UrlPattern = "help", Target = "MainPage", ViewUrl = "/help" },
            new RouteTemplate { Id = "Login", UrlPattern = "login", Target = "MainPage", ViewUrl = "/login" },
            new RouteTemplate { Id = "Cart", UrlPattern = "cart", Target = "MainPage", ViewUrl = "/cart" }
         });
      }
   }
}
