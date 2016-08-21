using System.Collections.Generic;
using Domain.Entity.Interfaces;
using Domain.Enums;

namespace Service.Interfaces
{
   public interface IMenuService
   {
      IEnumerable<IMenuItemEntity> GetMenuItems();

      IEnumerable<IMenuItemEntity> GetMenuItems( MenuTypes menuType );

      IMenuItemEntity GetMenuItem( int Id );
   }
}
