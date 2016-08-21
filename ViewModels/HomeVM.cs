using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class HomeVM : BaseVM, IRoutable
   {
      public string PageTitle => "My Fake App";

      public Route MenuRoute;

      public RoutingState RoutingState { get; set; }

      public HomeVM()
      {
         MenuRoute = this.Redirect("app", "menu");
      }
   }
}
