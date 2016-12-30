using System;
using System.Linq;
using System.Threading;
using Service.Interfaces;
using Domain;

namespace Services
{
   public class AccountService : IAccountService
   {
      private readonly IUserCache _cache;

      public AccountService(IUserCache cache)
      {
         _cache = cache;
      }

      public UserAccount GetAccount()
      {
         var key = BuildCacheKey();
         var userAccount = _cache.Get<UserAccount>(key);
         if (userAccount == null)
         {
            userAccount = new UserAccount();

            // Try to set default first and last names by parsing the principal's identity name.
            var userName = Thread.CurrentPrincipal?.Identity?.Name;
            var nameTokens = userName.Split(' ');
            if (nameTokens.Length > 1)
            {
               userAccount.LastName = nameTokens.Last();
               userAccount.FirstName = string.Join(" ", nameTokens, 0, nameTokens.Length - 1);
            }
            else
               userAccount.FirstName = userName;

            _cache.Set(key, userAccount);
         }

         return userAccount;
      }

      public void SaveAccount(UserAccount userAccount) => _cache.Set<UserAccount>(BuildCacheKey(), userAccount);

      private string BuildCacheKey()
      {
         var userName = Thread.CurrentPrincipal?.Identity?.Name;
         return $"{nameof(UserAccount)}_{userName}";
      }
   }
}
