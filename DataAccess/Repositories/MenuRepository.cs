using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using DataAccess.Properties;
using Domain;
using Domain.Repository.Interfaces;

namespace DataAccess.Repositories
{
   public class MenuRepository : DbContext, IMenuRepository
   {
      private IEnumerable<MenuItem> _cache;

      public MenuRepository()
      {
      }

      public virtual DbSet<MenuItemEntity> MenuItemEntities { get; set; }

      public IEnumerable<MenuItem> GetMenuItems()
      {
         // Read mockup data from json file.
         return _cache = _cache ??
            JsonConvert
               .DeserializeObject<List<MenuItemEntity>>(Resources.menu_json)
               .Select(i => SimpleMapper.Map<MenuItemEntity, MenuItem>(i));
      }

      public MenuItem GetMenuItem(int id) => GetMenuItems().FirstOrDefault(i => i.Id == id);
   }
}