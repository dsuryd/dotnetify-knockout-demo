using System;
using System.Collections.Generic;

namespace Domain
{
   public class ShoppingCart
   {
      private Dictionary<MenuItem, int> _orders = new Dictionary<MenuItem, int>();

      public event EventHandler Changed;

      public void AddOrder(MenuItem menuItem, int quantity = 1)
      {
         if (_orders.ContainsKey(menuItem))
            _orders[menuItem] += quantity;
         else
            _orders.Add(menuItem, quantity);

         Changed?.Invoke(this, new EventArgs());
      }

      public void RemoveOrder(MenuItem menuItem)
      {
         if (_orders.ContainsKey(menuItem))
         {
            if (--_orders[menuItem] == 0)
               _orders.Remove(menuItem);

            Changed?.Invoke(this, new EventArgs());
         }
      }
   }
}
