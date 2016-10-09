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

      public string PageTitle => "My Fake App";

      public IEnumerable<ShoppingCartItemDTO> ShoppingCartItems => GetShoppingCartItems();

      public RoutingState RoutingState { get; set; }

      public ShoppingCartVM( IShoppingCartService shoppingCartService, IMenuService menuService )
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
