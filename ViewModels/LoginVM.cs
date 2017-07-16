using DotNetify;
using DotNetify.Routing;

namespace ViewModels
{
   public class LoginVM : BaseVM
   {
      public string PageTitle => "My Fake App";
      public string Slogan => "Fresh meal delivered to your doorstep";
      public string GoogleAuthCaption => "Continue with Google";
      public string HowItWorksTitle => "How It Works";
      public string HowItWorksText => "It's as easy as 1, 2, 3.  You hungry, you tap what you want, we deliver it to you!";
   }
}
