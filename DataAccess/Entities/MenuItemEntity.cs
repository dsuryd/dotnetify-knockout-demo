using System;
using Domain.Enums;

namespace DataAccess.Entities
{
   /// <summary>
   /// Persistence model of MenuItem.
   /// </summary>
   public partial class MenuItemEntity
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public float Price { get; set; }
      public MenuTypes Type { get; set; }
      public string ImageUri { get; set; }
   }
}
