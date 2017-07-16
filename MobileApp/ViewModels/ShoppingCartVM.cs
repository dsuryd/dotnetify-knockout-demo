using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using DotNetify;
using DotNetify.Routing;
using Domain.Service.Interfaces;
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

      public bool DisablePlaceOrder => ShoppingCartItems.Count() == 0;
      public ICommand PlaceOrderCommand => new Command(() => OnPlaceOrder());

      public string OrderPlacedToaster => "Thank you for your order!";
      public int OrderPlacedToasterTrigger
      {
         get { return Get<int>(); }
         set { Set(value); }
      }

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

      // The following methods are required for dotNetify to handle client-side update on an item in an items property.
      // By convention, the method name starts with the items property name and ends with '_get' suffix.
      public ShoppingCartItemDTO ShoppingCartItems_get(string key) => ShoppingCartItems.FirstOrDefault(i => i.Id.ToString() == key);

      /// <summary>
      /// Build the shopping cart item data to be delivered to the front-end for display.
      /// </summary>
      private IEnumerable<ShoppingCartItemDTO> GetShoppingCartItems()
      {
         var orders = _shoppingCartService.GetShoppingCart().GetOrders();
         return orders.ToList().Select(i =>
         {
            var menuItem = _menuService.GetMenuItem(i.MenuItemId);
            return new ShoppingCartItemDTO
            {
               Id = i.MenuItemId,
               Qty = i.Quantity,
               Name = menuItem.Name,
               Price = $"${menuItem.Price}",
               ImageUrl = "/images/menu-items/" + menuItem.ImageUri,
               RemoveCommand = new Command(() => OnRemove(i.MenuItemId))
            };
         });
      }

      /// <summary>
      /// Removes an item from the shopping cart in response to the Remove button click.
      /// </summary>
      private void OnRemove(int iMenuItemId)
      {
         _shoppingCartService.GetShoppingCart().RemoveOrder(_menuService.GetMenuItem(iMenuItemId));
         Changed(() => ShoppingCartItems);
         Changed(() => DisablePlaceOrder);
      }

      /// <summary>
      /// Places the order.
      /// </summary>
      private void OnPlaceOrder()
      {
         _shoppingCartService.GetShoppingCart().Clear();
         Changed(() => ShoppingCartItems);
         Changed(() => DisablePlaceOrder);
         OrderPlacedToasterTrigger++;
      }
   }
}
