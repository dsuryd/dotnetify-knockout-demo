using Domain.Enums;

namespace Domain.Entity.Interfaces
{
   public interface IMenuItemEntity
   {
      int Id { get; set; }
      string Name { get; set; }
      string Description { get; set; }
      float Price { get; set; }
      MenuTypes Type { get; set; }
      string ImageUri { get; set; }
   }
}
