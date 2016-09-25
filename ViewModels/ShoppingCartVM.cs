using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;

namespace ViewModels
{
   public class ShoppingCartVM : BaseVM, IRoutable
   {
      private readonly IShoppingCartService _shoppingCartService;

      public string PageTitle => "My Fake App";

      public RoutingState RoutingState { get; set; }

      public ShoppingCartVM( IShoppingCartService shoppingCartService )
      {
         _shoppingCartService = shoppingCartService;
      }
   }
}
