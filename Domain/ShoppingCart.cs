using System;
using System.Collections.Generic;

namespace Domain
{
   public class ShoppingCart
   {
      private Dictionary<int, int> _orders = new Dictionary<int, int>();

      public event EventHandler Changed;

      public void AddOrder(MenuItem menuItem, int quantity = 1)
      {
         if (_orders.ContainsKey(menuItem.Id))
            _orders[menuItem.Id] += quantity;
         else
            _orders.Add(menuItem.Id, quantity);

         Changed?.Invoke(this, new EventArgs());
      }

      public void RemoveOrder(MenuItem menuItem)
      {
         if (_orders.ContainsKey(menuItem.Id))
         {
            if (--_orders[menuItem.Id] == 0)
               _orders.Remove(menuItem.Id);

            Changed?.Invoke(this, new EventArgs());
         }
      }

      public int GetOrderCount(MenuItem menuItem)
      {
         return _orders.ContainsKey(menuItem.Id) ? _orders[menuItem.Id] : 0;
      }
   }
}
