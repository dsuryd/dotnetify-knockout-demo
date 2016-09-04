using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Service.Interfaces
{
   public interface IMenuService
   {
      IEnumerable<MenuItem> GetMenuItems();

      IEnumerable<MenuItem> GetMenuItems( MenuTypes menuType );

      MenuItem GetMenuItem( int Id );
   }
}
