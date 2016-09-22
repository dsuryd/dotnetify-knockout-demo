using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
   public interface ISessionCache
   {
      T Get<T>(string iKey);

      void Set<T>(string iKey, T iValue);
   }
}
