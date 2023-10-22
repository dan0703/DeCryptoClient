using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using DataAccess;
using DeCrypto.Domain;

namespace DeCrypto.Logic
{
    internal class AccountLogic
    {
        public static bool RegisterAccount(Account account)
        {
            using (var context = new DeCryptoDBEntities())
            {
                context.AccountSet.Add(account);
            }
        }
    }
}
