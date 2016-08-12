using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entity.Interfaces
{
   public interface IMenuItemEntity
   {
      int Id { get; set; }
      string Name { get; set; }
      float Price { get; set; }
      MenuTypes Type { get; set; }
   }
}
