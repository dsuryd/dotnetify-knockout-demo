using System.Collections.Generic;
using System.Windows.Input;
using DotNetify;
using Domain;
using Domain.Enums;
using Service.Interfaces;

namespace ViewModels
{
   public class AccountVM : BaseVM
   {
      private readonly IAccountService _accountService;

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

      public string SavedToaster => "Account saved";
      public int SavedToasterTrigger
      {
         get { return Get<int>(); }
         set { Set(value); }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      public AccountVM(IAccountService accountService)
      {
         _accountService = accountService;

         var userAccount = _accountService.GetAccount();
         FirstName = userAccount.FirstName;
         LastName = userAccount.LastName;
         Email = userAccount.Email;
         Language = (int)userAccount.Language;
         OptOutNotice = !userAccount.OptOut;
         TrackMyLocation = userAccount.TrackLocation;
      }

      public void Save()
      {
         var userAccount = new UserAccount()
         {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Language = (Languages)Language,
            OptOut = OptOutNotice,
            TrackLocation = TrackMyLocation
         };

         _accountService.SaveAccount(userAccount);

         // Send notification back that the account was saved.
         SavedToasterTrigger++;
      }
   }
}
