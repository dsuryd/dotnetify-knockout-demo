using System.Collections.Generic;
using System.Linq;
using Domain.Service.Interfaces;
using Domain.Repository.Interfaces;
using Domain;
using Domain.Enums;

namespace Services
{
   public class MenuService : IMenuService
   {
      private readonly IMenuRepository _menuRepository;

      public MenuService(IMenuRepository menuRepository)
      {
         _menuRepository = menuRepository;
      }

      public IEnumerable<MenuItem> GetMenuItems() => _menuRepository.GetMenuItems();

      public IEnumerable<MenuItem> GetMenuItems(MenuTypes menuType) =>
         _menuRepository.GetMenuItems()
            .Where(i => i.Type == menuType)
            .OrderBy(i => i.Name);

      public MenuItem GetMenuItem(int id) => _menuRepository.GetMenuItem(id);
   }
}
