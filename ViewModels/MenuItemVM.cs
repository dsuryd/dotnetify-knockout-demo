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

      // Text displayed on this page.
      public string AddCaption => "Add";

      // Shows the menu item name.
      public string PageTitle { get; set; }

      // Menu item info to be shown on the page.
      public MenuItemDTO MenuItem { get; set; }

      // Required by IRoutable.
      public RoutingState RoutingState { get; set; }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="menuService">Service for getting menu info.</param>
      /// <param name="shoppingCartService">Service for getting shopping cart info.</param>
      public MenuItemVM(IMenuService menuService, IShoppingCartService shoppingCartService)
      {
         _menuService = menuService;
         _shoppingCartService = shoppingCartService;

         // When this VM is invoked due to routing, use the item ID given in the URL to load and display the menu item.
         this.OnRouted((sender, e) => LoadMenuItem(e.From.Replace("item/", "")));
      }

      /// <summary>
      /// Loads a menu item data to be delivered to front-end for display.
      /// </summary>
      private void LoadMenuItem(string strId)
      {
         int id;
         if (int.TryParse(strId, out id))
         {
            var menuItem = _menuService.GetMenuItem(id);
            if (menuItem != null)
            {
               var cart = _shoppingCartService.GetShoppingCart();

               PageTitle = menuItem.Name;
               MenuItem = new MenuItemDTO
               {
                  Name = menuItem.Name,
                  Description = menuItem.Description,
                  Price = $"${menuItem.Price}",
                  ImageUrl = "/images/menu-items/" + menuItem.ImageUri,
                  AddCommand = new Command(() => OnAdded(menuItem.Id)),
                  ItemAdded = cart.GetOrderCount(menuItem) > 0 ? $"{cart.GetOrderCount(menuItem)} in cart" : null
               };
            }
         }
      }

      /// <summary>
      /// Adds a menu item to the shopping cart in response to the Add button click.
      /// </summary>
      private void OnAdded(int menuItemId)
      {
         var menuItem = _menuService.GetMenuItem(menuItemId);
         _shoppingCartService.GetShoppingCart().AddOrder(menuItem);

         // Update the order count that's displayed on the menu item.
         var cart = _shoppingCartService.GetShoppingCart();
         MenuItem.ItemAdded = cart.GetOrderCount(menuItem) > 0 ? $"{cart.GetOrderCount(menuItem)} in cart" : null;
         Changed(() => MenuItem);
      }
   }
}
