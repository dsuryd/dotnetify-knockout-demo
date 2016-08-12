using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccess.Entities;
using Domain.Entity.Interfaces;
using Domain.Repository.Interfaces;

namespace DataAccess.Repositories
{
   public class MenuRepository : DbContext, IMenuRepository
   {
      public MenuRepository()
           : base( "name=MenuRepository" )
      {
      }

      public virtual DbSet<MenuItemEntity> MenuItemEntities { get; set; }

      public IEnumerable<IMenuItemEntity> GetMenuItems()
      {
         throw new NotImplementedException();
      }
   }
}