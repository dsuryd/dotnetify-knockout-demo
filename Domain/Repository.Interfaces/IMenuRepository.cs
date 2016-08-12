using System.Collections.Generic;
using Domain.Entity.Interfaces;

namespace Domain.Repository.Interfaces
{
   public interface IMenuRepository
   {
      IEnumerable<IMenuItemEntity> GetMenuItems();
   }
}
