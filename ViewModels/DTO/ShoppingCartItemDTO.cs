using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DTO
{
   public class ShoppingCartItemDTO
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Qty { get; set; }
      public string Price { get; set; }
      public string ImageUrl { get; set; }
   }
}
