using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class LoginVM : BaseVM
   {
      public string PageTitle => "My Fake App";

      public string GoogleLogin => "Continue with Google";

      public LoginVM()
      {
      }
   }
}
