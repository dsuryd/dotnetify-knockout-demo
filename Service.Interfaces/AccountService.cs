using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Service.Interfaces
{
   public interface AccountService
   {
      UserAccount GetAccount();
   }
}
