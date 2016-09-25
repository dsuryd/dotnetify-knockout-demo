using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Domain;
using Service.Interfaces;

namespace Services
{
   public class ShoppingCartService : IShoppingCartService
   {
      private readonly IMenuService _menuService;
      private readonly ISessionCache _cache;

      private class ShoppingCartItem
      {
         public int ItemId { get; set; }
         public int Qty { get; set; }
      }

      public ShoppingCartService(ISessionCache cache, IMenuService menuService)
      {
         _cache = cache;
         _menuService = menuService;
      }

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

      public ShoppingCart GetShoppingCart()
      {
         var key = nameof(ShoppingCart);

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
