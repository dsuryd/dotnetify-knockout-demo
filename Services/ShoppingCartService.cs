using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Domain;
using Service.Interfaces;

namespace Services
{
   public class ShoppingCartService : IShoppingCartService
   {
      private readonly IMenuService _menuService;
      private readonly IUserCache _cache;

      private class ShoppingCartItem
      {
         public int ItemId { get; set; }
         public int Qty { get; set; }
      }

      public ShoppingCartService(IUserCache cache, IMenuService menuService)
      {
         _cache = cache;
         _menuService = menuService;
      }

      /// <summary>
      /// Deserializes the previous shopping cart data in json format that was stored in the client's HTML5 storage.
      /// </summary>
      public void DeserializeShoppingCart(string jsonData)
      {
         try
         {
            var shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            var items = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(jsonData);
            foreach (var item in items)
            {
               var menuItem = _menuService.GetMenuItem(item.ItemId);
               if (menuItem != null)
                  shoppingCart.AddOrder(menuItem, item.Qty);
            }
         }
         catch (Exception)
         { }
      }

      public string SerializeShoppingCart()
      {
         return JsonConvert.SerializeObject(
            GetShoppingCart()
            .GetOrders()
            .ToList()
            .Select(i => new { ItemId = i.Key, Qty = i.Value })
         );
      }

      public ShoppingCart GetShoppingCart()
      {
         var userName = Thread.CurrentPrincipal?.Identity.Name;
         var key = $"{nameof(ShoppingCart)}_{userName}";

         var shoppingCart = _cache.Get<ShoppingCart>(key);
         if (shoppingCart == null)
         {
            shoppingCart = new ShoppingCart();
            _cache.Set(key, shoppingCart);
         }

         return shoppingCart;
      }
   }
}
