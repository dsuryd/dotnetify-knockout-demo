using System;
using Domain.Entity.Interfaces;
using Domain.Enums;

namespace DataAccess.Entities
{
   public partial class MenuItemEntity : IMenuItemEntity
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public float Price { get; set; }
      public MenuTypes Type { get; set; }
      public string ImageUri { get; set; }
   }
}
