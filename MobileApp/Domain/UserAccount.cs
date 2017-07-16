using Domain.Enums;

namespace Domain
{
   public class UserAccount
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public Languages Language { get; set; } = Languages.English;
      public bool OptOut { get; set; }
      public bool TrackLocation { get; set; } = true;
   }
}
