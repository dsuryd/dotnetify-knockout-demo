using System.Collections.Generic;
using System.Linq;
using DotNetify;
using Service.Interfaces;
using Domain.Enums;

namespace ViewModels
{
   public class MenuVM : BaseVM
   {
      private IMenuService _menuService;

      public class MenuItem
      {
         public string Name;
         public string Price;
         public string ImageUrl;
      }

      public string PageTitle => "Our Menu";

      public string BreakfastTabCaption => "Breakfast";
      public string LunchTabCaption => "Lunch";
      public string DinnerTabCaption => "Dinner";

      public IEnumerable<MenuItem> BreakfastMenu => GetMenuItems(MenuTypes.Breakfast);
      public IEnumerable<MenuItem> LunchMenu => GetMenuItems(MenuTypes.Lunch);
      public IEnumerable<MenuItem> DinnerMenu => GetMenuItems(MenuTypes.Dinner);

      public MenuVM( IMenuService menuService )
      {
         _menuService = menuService;
      }

      private IEnumerable<MenuItem> GetMenuItems( MenuTypes menuType )
      {
         return _menuService.GetMenuItems()
            .Where(i => i.Type == menuType)
            .Select(j => new MenuItem
            {
               Name = j.Name,
               Price = $"${j.Price}",
               ImageUrl = "/images/menu-items/" + j.ImageUri
            });
      }
   }
}
