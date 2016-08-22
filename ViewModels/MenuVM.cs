using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Domain.Enums;
using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class MenuVM : BaseVM, IRoutable
   {
      private IMenuService _menuService;

      public string PageTitle => "Daily Menu";

      public string BreakfastTabCaption => "Breakfast";
      public string LunchTabCaption => "Lunch";
      public string DinnerTabCaption => "Dinner";

      public IEnumerable<MenuItemDTO> BreakfastMenu => GetMenuItems(MenuTypes.Breakfast);
      public IEnumerable<MenuItemDTO> LunchMenu => GetMenuItems(MenuTypes.Lunch);
      public IEnumerable<MenuItemDTO> DinnerMenu => GetMenuItems(MenuTypes.Dinner);

      public RoutingState RoutingState { get; set; }

      public MenuVM( IMenuService menuService )
      {
         _menuService = menuService;

         this.RegisterRoutes("menu", new List<RouteTemplate>
         {
            new RouteTemplate { Id = "MenuItem", UrlPattern = "(/:id)", Target = "MainPage", ViewUrl = "/menu-details" }
         });
      }

      // The following methods are required for dotNetify to handle client-side update on an item in an items property.
      // By convention, the method name starts with the items property name and ends with '_get' suffix.

      public MenuItemDTO BreakfastMenu_get( string key ) => BreakfastMenu.FirstOrDefault(i => i.Id.ToString() == key);
      public MenuItemDTO LunchMenu_get( string key ) => LunchMenu.FirstOrDefault(i => i.Id.ToString() == key);
      public MenuItemDTO DinnerMenu_get( string key ) => DinnerMenu.FirstOrDefault(i => i.Id.ToString() == key);


      private IEnumerable<MenuItemDTO> GetMenuItems( MenuTypes menuType )
      {
         return _menuService.GetMenuItems(menuType)
            .Select(i => new MenuItemDTO
            {
               Id = i.Id,
               Name = i.Name,
               Price = $"${i.Price}",
               ImageUrl = "/images/menu-items/" + i.ImageUri,
               Route = this.GetRoute("MenuItem", $"/{i.Id}"),
               AddCommand = new Command(() => { System.Diagnostics.Trace.WriteLine($"Add {i.Id}"); })
      });
      }
   }
}
