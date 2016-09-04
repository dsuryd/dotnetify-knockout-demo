using System.Collections.Generic;

namespace Domain.Repository.Interfaces
{
   public interface IMenuRepository
   {
      IEnumerable<MenuItem> GetMenuItems();

      MenuItem GetMenuItem( int id );
   }
}
