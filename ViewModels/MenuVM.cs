using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Domain.Enums;
using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;

namespace ViewModels
{
   public class MenuVM : BaseVM, IRoutable
   {
      private IMenuService _menuService;

      public class MenuItemDTO
      {
         public int Id { get; set; }
         public string Name { get; set; }
         public string Price { get; set; }
         public string ImageUrl { get; set; }
         public Route Route { get; set; }
         public bool AddCommand
         {
            get { return false; }
            set { System.Diagnostics.Trace.WriteLine($"Add {Id}"); }
         }

      }

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

      private IEnumerable<MenuItemDTO> GetMenuItems( MenuTypes menuType )
      {
         return _menuService.GetMenuItems(menuType)
            .Select(i => new MenuItemDTO
            {
               Id = i.Id,
               Name = i.Name,
               Price = $"${i.Price}",
               ImageUrl = "/images/menu-items/" + i.ImageUri,
               Route = this.GetRoute("MenuItem", $"/{i.Id}")
            });
      }
   }
}
