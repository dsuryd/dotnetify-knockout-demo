using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using DataAccess.Entities;
using DataAccess.Properties;
using Domain.Entity.Interfaces;
using Domain.Repository.Interfaces;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
   public class MenuRepository : DbContext, IMenuRepository
   {
      public MenuRepository()
           : base("name=MenuRepository")
      {
      }

      public virtual DbSet<MenuItemEntity> MenuItemEntities { get; set; }

      public IEnumerable<IMenuItemEntity> GetMenuItems()
      {
         return JsonConvert.DeserializeObject<List<MenuItemEntity>>(Resources.menu_json);
      }
   }
}