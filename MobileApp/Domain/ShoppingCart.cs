using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
   public class ShoppingCart
   {
      private Dictionary<int, Order> _orders = new Dictionary<int, Order>();

      public int OrderCount => _orders.Select(i => i.Value.Quantity).Sum();

      public event EventHandler<int> Changed;

      public void AddOrder(MenuItem menuItem, int quantity = 1)
      {
         if (_orders.ContainsKey(menuItem.Id))
            _orders[menuItem.Id].Quantity += quantity;
         else
            _orders.Add(menuItem.Id, new Order { MenuItemId = menuItem.Id, Quantity = quantity });

         Changed?.Invoke(this, menuItem.Id);
      }

      public void Clear()
      {
         _orders.Clear();
         Changed?.Invoke(this, 0);
      }

      public IEnumerable<Order> GetOrders() => _orders.Values;

      public int GetOrderCount(MenuItem menuItem) => _orders.ContainsKey(menuItem.Id) ? _orders[menuItem.Id].Quantity : 0;

      public void RemoveOrder(MenuItem menuItem)
      {
         if (_orders.ContainsKey(menuItem.Id))
         {
            if (--_orders[menuItem.Id].Quantity == 0)
               _orders.Remove(menuItem.Id);

            Changed?.Invoke(this, menuItem.Id);
         }
      }
   }
}
