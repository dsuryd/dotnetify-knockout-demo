using System.Collections.Generic;
using System.Linq;
using DotNetify;
using Service.Interfaces;
using Domain.Entity.Interfaces;

namespace ViewModels
{
   public class MenuVM : BaseVM
   {
      private IMenuService _menuService;

      public class MenuItem
      {
         public string Name;
         public float Price;
      }

      public string PageTitle => "Our Menu";

      public IEnumerable<MenuItem> MenuItems => _menuService.GetMenuItems().Select(i => new MenuItem { Name = i.Name, Price = i.Price });

      public MenuVM( IMenuService menuService )
      {
         _menuService = menuService;
      }
   }
}
