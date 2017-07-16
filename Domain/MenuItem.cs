using Domain.Enums;

namespace Domain
{
   public class MenuItem
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public float Price { get; set; }
      public MenuTypes Type { get; set; }
      public string ImageUri { get; set; }
   }
}
