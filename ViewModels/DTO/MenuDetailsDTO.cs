using System.Windows.Input;

namespace ViewModels.DTO
{
   public class MenuDetailsDTO
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string Price { get; set; }
      public string ImageUrl { get; set; }
      public ICommand AddCommand { get; set; }
   }
}
