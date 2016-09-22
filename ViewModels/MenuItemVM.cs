using DotNetify;
using DotNetify.Routing;
using Service.Interfaces;
using ViewModels.DTO;

namespace ViewModels
{
   public class MenuItemVM : BaseVM, IRoutable
   {
      private readonly IMenuService _menuService;
      private readonly IShoppingCartService _shoppingCartService;

      public string PageTitle { get; set; }

      public MenuItemDTO MenuItem { get; set; }

      public RoutingState RoutingState { get; set; }

      public MenuItemVM( IMenuService menuService, IShoppingCartService shoppingCartService )
      {
         _menuService = menuService;
         _shoppingCartService = shoppingCartService;

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
                  AddCommand = new Command(() => AddToShoppingCart(menuItem.Id))
               };
            }
         }
      }

      private void AddToShoppingCart(int menuItemId)
      {
         var cart = _shoppingCartService.GetShoppingCart();
         cart.AddOrder(_menuService.GetMenuItem(menuItemId));
      }
   }
}
