namespace Domain
{
   public class UserAccount
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public bool OptOut { get; set; }
      public bool TrackLocation { get; set; } = true;
   }
}
