using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class MenuItemVM : BaseVM, IRoutable
   {
      private IMenuService _menuService;

      public string PageTitle { get; set; }
      public MenuItemDTO MenuItem { get; set; }
      public Route Back { get; set; } 

      public RoutingState RoutingState { get; set; }

      public MenuItemVM( IMenuService menuService )
      {
         _menuService = menuService;

         this.OnRouted(( sender, e ) => LoadMenuItem(e.From.Replace("item/", "")));
      }

      private void LoadMenuItem( string strId )
      {
         int id;
         if ( int.TryParse(strId, out id) )
         {
            var menuItem = _menuService.GetMenuItem(id);
            if ( menuItem != null )
            {
               PageTitle = menuItem.Name;
               MenuItem = new MenuItemDTO
               {
                  Name = menuItem.Name,
                  Description = menuItem.Description,
                  Price = $"${menuItem.Price}",
                  ImageUrl = "/images/menu-items/" + menuItem.ImageUri,
                  AddCommand = new Command(() => { })
               };

               Back = this.Redirect("app", $"menu/{menuItem.Type}");
            }
         }
      }
   }
}
