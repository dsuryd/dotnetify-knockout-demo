
namespace Domain.Service.Interfaces
{
   public interface IUserCache
   {
      T Get<T>(string iKey);

      void Set<T>(string iKey, T iValue);
   }
}
