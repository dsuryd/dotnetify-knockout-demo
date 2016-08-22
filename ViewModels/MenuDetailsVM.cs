using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class MenuDetailsVM : BaseVM, IRoutable
   {
      private IMenuService _menuService;

      public string PageTitle { get; set; }
      public MenuDetailsDTO MenuDetails { get; set; }

      public RoutingState RoutingState { get; set; }

      public MenuDetailsVM( IMenuService menuService )
      {
         _menuService = menuService;

         this.OnRouted(( sender, e ) => LoadMenuItem(e.From.Replace("/", "")));
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
               MenuDetails = new MenuDetailsDTO
               {
                  Name = menuItem.Name,
                  Description = menuItem.Description,
                  Price = $"${menuItem.Price}",
                  ImageUrl = "/images/menu-items/" + menuItem.ImageUri,
                  AddCommand = new Command(() => { })
               };
            }
         }
      }
   }
}
