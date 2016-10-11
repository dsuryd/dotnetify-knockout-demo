using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;

namespace ViewModels
{
   public class HomeVM : BaseVM, IRoutable
   {
      private readonly IShoppingCartService _shoppingCartService;

      // All text that get displayed on this page.
      public string PageTitle => "My Fake App";
      public string Slogan => "Fresh meal delivered to your doorstep";
      public string MenuCaption => "See Our Menu";
      public string HowItWorksTitle => "How It Works";
      public string HowItWorksText => "It's as easy as 1, 2, 3.  You hungry, you tap what you want, we deliver it to you!";

      // Required by IRoutable.
      public RoutingState RoutingState { get; set; }

      // Used with the center button to route to the menu page.
      public Route MenuRoute => this.Redirect("app", "menu");

      // Used with the shopping cart icon to route to the shopping cart page.
      public Route CartRoute => this.Redirect("app", "cart");

      // Order count shown on the shopping cart icon.
      public int OrderCount => _shoppingCartService.GetShoppingCart().OrderCount;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="shoppingCartService">Service for getting shopping cart info.</param>
      public HomeVM( IShoppingCartService shoppingCartService )
      {
         _shoppingCartService = shoppingCartService;
      }
   }
}
