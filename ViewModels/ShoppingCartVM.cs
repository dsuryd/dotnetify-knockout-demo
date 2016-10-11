using System.Collections.Generic;
using System.Linq;
using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class ShoppingCartVM : BaseVM, IRoutable
   {
      private readonly IShoppingCartService _shoppingCartService;
      private readonly IMenuService _menuService;

      // All text displayed on this page.
      public string PageTitle => "My Fake App";
      public string ReviewOrderCaption => "Review your order";
      public string RemoveCaption => "Remove";
      public string QtyCaption => "QTY";
      public string PlaceOrderCaption => "Place Order";

      // Shopping cart item list.
      public IEnumerable<ShoppingCartItemDTO> ShoppingCartItems => GetShoppingCartItems();

      // Required by IRoutable.
      public RoutingState RoutingState { get; set; }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="menuService">Service for getting menu info.</param>
      /// <param name="shoppingCartService">Service for getting shopping cart info.</param>
      public ShoppingCartVM(IMenuService menuService, IShoppingCartService shoppingCartService)
      {
         _shoppingCartService = shoppingCartService;
         _menuService = menuService;
      }

      private IEnumerable<ShoppingCartItemDTO> GetShoppingCartItems()
      {
         var cartItems = _shoppingCartService.GetShoppingCart().GetOrders();
         return cartItems.ToList().Select(i =>
         {
            var menuItem = _menuService.GetMenuItem(i.Key);
            return new ShoppingCartItemDTO
            {
               Id = i.Key,
               Qty = i.Value,
               Name = menuItem.Name,
               Price = $"${menuItem.Price}",
               ImageUrl = "/images/menu-items/" + menuItem.ImageUri
            };
         });
      }
   }
}
