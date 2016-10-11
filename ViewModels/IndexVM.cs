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

      // Class to hold side navigation items.
      public class SideNavItem
      {
         public Route Route { get; set; }
         public string Caption { get; set; }
         public string Icon { get; set; }
      }

      // Shopping cart data that's stored in the client-side HTML5 local storage.
      public string CartLocalData
      {
         get { return _shoppingCartService.SerializeShoppingCart(); }
         set { _shoppingCartService.DeserializeShoppingCart(value); }
      }

      // Side navigation list. Each list item routes to a different page.
      public List<SideNavItem> SideNav => new List<SideNavItem>
      {
         new SideNavItem { Route = this.GetRoute("Home"), Caption = "Home", Icon = "fa fa-home btn-warning" },
         new SideNavItem { Route = this.GetRoute("Menu", "menu"), Caption = "Menu", Icon = "fa fa-list-alt btn-primary" },
         new SideNavItem { Route = this.GetRoute("Account"), Caption = "Account", Icon = "fa fa-user btn-positive" }
      };

      // Used with the shopping cart icon to route to the shopping cart page.
      public Route CartRoute => this.GetRoute("Cart");

      // Required by IRoutable.
      public RoutingState RoutingState { get; set; }

      // Name of authenticated user that's shown above the side navigation list.
      public string UserName => Thread.CurrentPrincipal?.Identity?.Name;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="shoppingCartService">Service for getting shopping cart info.</param>
      public IndexVM(IShoppingCartService shoppingCartService)
      {
         _shoppingCartService = shoppingCartService;

         // When shopping cart content is changed, raise the changed event to get the data stored in client-side HTML5 local storage.
         var shoppingCart = _shoppingCartService.GetShoppingCart();
         shoppingCart.Changed += (sender, e) => Changed( () => CartLocalData );

         // Register all the routes that originate from this page.
         this.RegisterRoutes("app", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "Home", UrlPattern = "", Target = "MainPage", ViewUrl = "/home" },
            new RouteTemplate { Id = "Menu", UrlPattern = "menu(/:tab)", Target = "MainPage", ViewUrl = "/menu" },
            new RouteTemplate { Id = "Account", UrlPattern = "account", Target = "MainPage", ViewUrl = "/account" },
            new RouteTemplate { Id = "Login", UrlPattern = "login", Target = "MainPage", ViewUrl = "/login" },
            new RouteTemplate { Id = "Cart", UrlPattern = "cart", Target = "MainPage", ViewUrl = "/shopping-cart" }
         });
      }
   }
}
