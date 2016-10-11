using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class MenuVM : BaseVM, IRoutable
   {
      private readonly IMenuService _menuService;
      private readonly IShoppingCartService _shoppingCartService;

      // All text that get displayed on this page.
      public string PageTitle => "Daily Menu";
      public string BreakfastTabCaption => "Breakfast";
      public string LunchTabCaption => "Lunch";
      public string DinnerTabCaption => "Dinner";

      // List of menu items for each tab.
      public IEnumerable<MenuItemDTO> BreakfastMenu => GetMenuItems(MenuTypes.Breakfast);
      public IEnumerable<MenuItemDTO> LunchMenu => GetMenuItems(MenuTypes.Lunch);
      public IEnumerable<MenuItemDTO> DinnerMenu => GetMenuItems(MenuTypes.Dinner);

      // The active tab.
      public string ActiveTab { get; set; } = "tab-breakfast";

      // Used with the shopping cart icon to route to the shopping cart page.
      public Route CartRoute => this.Redirect("app", "cart");

      // Required by IRoutable.
      public RoutingState RoutingState { get; set; }

      // Order count shown on the shopping cart icon.
      public int OrderCount => _shoppingCartService.GetShoppingCart().OrderCount;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="menuService">Service for getting menu info.</param>
      /// <param name="shoppingCartService">Service for getting shopping cart info.</param>
      public MenuVM(IMenuService menuService, IShoppingCartService shoppingCartService)
      {
         _menuService = menuService;
         _shoppingCartService = shoppingCartService;

         // If the shopping cart is changed, show the order count on the affected menu items.
         _shoppingCartService.GetShoppingCart().Changed += OnShoppingCartChanged;

         // Register the route that originates from this page.
         this.RegisterRoutes("menu", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "MenuItem", UrlPattern = "item(/:id)", Target = "RightDrawer", ViewUrl = "/menu-item" }
         });
      }

      // The following methods are required for dotNetify to handle client-side update on an item in an items property.
      // By convention, the method name starts with the items property name and ends with '_get' suffix.
      public MenuItemDTO BreakfastMenu_get(string key) => BreakfastMenu.FirstOrDefault(i => i.Id.ToString() == key);
      public MenuItemDTO LunchMenu_get(string key) => LunchMenu.FirstOrDefault(i => i.Id.ToString() == key);
      public MenuItemDTO DinnerMenu_get(string key) => DinnerMenu.FirstOrDefault(i => i.Id.ToString() == key);

      private IEnumerable<MenuItemDTO> GetMenuItems(MenuTypes menuType)
      {
         var cart = _shoppingCartService.GetShoppingCart();

         return _menuService.GetMenuItems(menuType)
            .Select(i => new MenuItemDTO
            {
               Id = i.Id,
               Name = i.Name,
               Price = $"${i.Price}",
               ImageUrl = "/images/menu-items/" + i.ImageUri,
               Route = this.GetRoute("MenuItem", $"item/{i.Id}"),
               AddCommand = new Command(() => AddToShoppingCart(i.Id)),
               ItemAdded = cart.GetOrderCount(i) > 0 ? $"{cart.GetOrderCount(i)} in cart" : null
            });
      }

      private void OnShoppingCartChanged( object sender, int menuItemId)
      {
         Changed(() => OrderCount);
         if (menuItemId > 0)
            UpdateOrderCount(menuItemId);
         else
         {
            Changed(() => BreakfastMenu);
            Changed(() => LunchMenu);
            Changed(() => DinnerMenu);
         }
      }

      private void AddToShoppingCart(int menuItemId)
      {
         var cart = _shoppingCartService.GetShoppingCart();
         var menuItem = _menuService.GetMenuItem(menuItemId);
         cart.AddOrder(menuItem);

         UpdateOrderCount(menuItemId);
      }

      private void UpdateOrderCount(int menuItemId)
      {
         var cart = _shoppingCartService.GetShoppingCart();
         var menuItem = _menuService.GetMenuItem(menuItemId);
         if (menuItem != null)
         {
            var update = new { Id = menuItem.Id, ItemAdded = $"{cart.GetOrderCount(menuItem)} in cart" };
            switch (menuItem.Type)
            {
               case MenuTypes.Breakfast:
                  this.UpdateList(() => BreakfastMenu, update);
                  break;

               case MenuTypes.Lunch:
                  this.UpdateList(() => LunchMenu, update);
                  break;

               case MenuTypes.Dinner:
                  this.UpdateList(() => DinnerMenu, update);
                  break;
            }
         }
      }
   }
}
