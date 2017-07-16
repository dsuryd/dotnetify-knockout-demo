using System.Windows.Input;
using DotNetify.Routing;

namespace ViewModels.DTO
{
   public class MenuItemDTO
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string Price { get; set; }
      public string ImageUrl { get; set; }
      public Route Route { get; set; }
      public ICommand AddCommand { get; set; }
      public string ItemAdded { get; set; }
   }
}
