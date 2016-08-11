using System.Collections.Generic;
using DotNetify;

namespace ViewModels
{
   public class Page3VM : BaseVM
   {
      public string FirstName
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public string FirstName_label => "First name";

      public string LastName
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public string LastName_label => "Last name";

      public int Language
      {
         get { return Get<int>(); }
         set { Set(value); }
      }

      public class LanguageOption
      {
         public int Id { get; set; }
         public string Text { get; set; }
      }

      public string Language_optionsCaption => "Select a language";
      public string Language_optionsText => nameof(LanguageOption.Text);
      public string Language_optionsValue => nameof(LanguageOption.Id);
      public List<LanguageOption> Language_options => new List<LanguageOption>
         {
            new LanguageOption { Id = 1, Text = "English" },
            new LanguageOption { Id = 2, Text = "French" },
            new LanguageOption { Id = 3, Text = "Spanish" },
            new LanguageOption { Id = 4, Text = "Japanese" }
         };

      public bool OptOutNotice
      {
         get { return Get<bool>(); }
         set { Set(value); }
      }

      public string OptOutNoticeText => "Allow this app to periodically send promotional offers";

      public bool TrackMyLocation
      {
         get { return Get<bool>(); }
         set { Set(value); }
      }

      public string TrackMyLocationText => "Track My Location";

      public bool AllowNotification
      {
         get { return Get<bool>(); }
         set { Set(value); }
      }

      public string AllowNotificationText => "Receive Notifications";

      public Page3VM()
      {
         Language = 1;
      }
   }
}
