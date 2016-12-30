using Domain;

namespace Service.Interfaces
{
   public interface IAccountService
   {
      UserAccount GetAccount();

      void SaveAccount(UserAccount userAccount);
   }
}
