using Domain;

namespace Domain.Service.Interfaces
{
   public interface IAccountService
   {
      UserAccount GetAccount();

      void SaveAccount(UserAccount userAccount);
   }
}
