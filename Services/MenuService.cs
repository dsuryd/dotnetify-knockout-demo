using System.Collections.Generic;
using System.Linq;
using Service.Interfaces;
using Domain.Repository.Interfaces;
using Domain.Entity.Interfaces;
using Domain.Enums;

namespace Services
{
   public class MenuService : IMenuService
   {
      private IMenuRepository _menuRepository;

      public MenuService( IMenuRepository menuRepository )
      {
         _menuRepository = menuRepository;
      }

      public IEnumerable<IMenuItemEntity> GetMenuItems()
      {
         return _menuRepository.GetMenuItems();
      }

      public IEnumerable<IMenuItemEntity> GetMenuItems( MenuTypes menuType )
      {
         return _menuRepository.GetMenuItems().Where(i => i.Type == menuType).OrderBy( i => i.Name );
      }

      public IMenuItemEntity GetMenuItem( int id )
      {
         return _menuRepository.GetMenuItem(id);
      }
   }
}
