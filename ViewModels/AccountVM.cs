using System.Collections.Generic;
using System.Windows.Input;
using DotNetify;

namespace ViewModels
{
   public class AccountVM : BaseVM
   {
      public enum Languages
      {
         English = 1,
         French,
         Spanish,
         Japanese
      }

      public class LanguageOption
      {
         public int Id { get; set; }
         public string Text { get; set; }
      }

      public string FirstName_label => "First name";
      public string FirstName_placeholder => "Enter first name";
      public string FirstName
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public string LastName_label => "Last name";
      public string LastName_placeholder => "Enter last name";
      public string LastName
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public string Email_label => "Email address";
      public string Email_placeholder => "Enter email";
      public string Email
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public int Language
      {
         get { return Get<int>(); }
         set { Set(value); }
      }

      public string Language_optionsCaption => "Select a language";
      public string Language_optionsText => nameof(LanguageOption.Text);
      public string Language_optionsValue => nameof(LanguageOption.Id);
      public List<LanguageOption> Language_options => new List<LanguageOption>
         {
            new LanguageOption { Id = (int) Languages.English, Text = "English" },
            new LanguageOption { Id = (int) Languages.French, Text = "French" },
            new LanguageOption { Id = (int) Languages.Spanish, Text = "Spanish" },
            new LanguageOption { Id = (int) Languages.Japanese, Text = "Japanese" }
         };

      public string OptOutNoticeText => "Allow this app to periodically send promotional offers";
      public bool OptOutNotice
      {
         get { return Get<bool>(); }
         set { Set(value); }
      }

      public string TrackMyLocationText => "Track my location";
      public bool TrackMyLocation
      {
         get { return Get<bool>(); }
         set { Set(value); }
      }
      
      public string SaveCaption => "Save";
      public ICommand SaveCommand => new Command(() => Save());

      /// <summary>
      /// Constructor.
      /// </summary>
      public AccountVM()
      {
         Language = (int)Languages.English;
      }

      public void Save()
      {
      }
   }
}
