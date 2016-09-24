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

      public string PageTitle => "Daily Menu";

      public string BreakfastTabCaption => "Breakfast";
      public string LunchTabCaption => "Lunch";
      public string DinnerTabCaption => "Dinner";

      public string ActiveTab { get; set; } = "tab-breakfast";

      public IEnumerable<MenuItemDTO> BreakfastMenu => GetMenuItems(MenuTypes.Breakfast);
      public IEnumerable<MenuItemDTO> LunchMenu => GetMenuItems(MenuTypes.Lunch);
      public IEnumerable<MenuItemDTO> DinnerMenu => GetMenuItems(MenuTypes.Dinner);

      public RoutingState RoutingState { get; set; }

      public MenuVM(IMenuService menuService, IShoppingCartService shoppingCartService)
      {
         _menuService = menuService;
         _shoppingCartService = shoppingCartService;

         this.RegisterRoutes("menu", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "MenuItem", UrlPattern = "item(/:id)", Target = "RightDrawer", ViewUrl = "/menu-item" }
         });

         this.OnRouted((sender, e) =>
         {
            var tab = e.From.Replace("menu/", "").ToLower();
            if (tab == "lunch" || tab == "dinner")
               ActiveTab = "tab-" + tab;
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

      private void AddToShoppingCart(int menuItemId)
      {
         var cart = _shoppingCartService.GetShoppingCart();
         var menuItem = _menuService.GetMenuItem(menuItemId);
         if (menuItem != null)
         {
            cart.AddOrder(menuItem);

            var itemAdded = $"{cart.GetOrderCount(menuItem)} in cart";
            switch (menuItem.Type)
            {
               case MenuTypes.Breakfast:
                  this.UpdateList(() => BreakfastMenu, new { Id = menuItem.Id, ItemAdded = itemAdded });
                  break;

               case MenuTypes.Lunch:
                  this.UpdateList(() => LunchMenu, new { Id = menuItem.Id, ItemAdded = itemAdded });
                  break;

               case MenuTypes.Dinner:
                  this.UpdateList(() => DinnerMenu, new { Id = menuItem.Id, ItemAdded = itemAdded });
                  break;
            }
         }
      }
   }
}
