using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Entities;
using DataAccess.Properties;
using Domain.Entity.Interfaces;
using Domain.Repository.Interfaces;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
   public class MenuRepository : DbContext, IMenuRepository
   {
      private IEnumerable<IMenuItemEntity> _cache;

      public MenuRepository()
           : base("name=MenuRepository")
      {
      }

      public virtual DbSet<MenuItemEntity> MenuItemEntities { get; set; }

      public IEnumerable<IMenuItemEntity> GetMenuItems() => _cache = _cache ?? JsonConvert.DeserializeObject<List<MenuItemEntity>>(Resources.menu_json);

      public IMenuItemEntity GetMenuItem( int id ) => GetMenuItems().FirstOrDefault(i => i.Id == id);
   }
}