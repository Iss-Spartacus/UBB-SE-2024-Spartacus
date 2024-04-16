using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Modules
{
    public class AccountVerifier
    {
        public static AccountType VerifyAccountType(string email)
        {
            if (email.EndsWith("@manager.com", StringComparison.OrdinalIgnoreCase))
            {
                return AccountType.Manager;
            }
            else if (email.EndsWith("@employee.com", StringComparison.OrdinalIgnoreCase))
            {
                return AccountType.Employee;
            }
            else
            {
                return AccountType.User;
            }
        }
    }

    public enum AccountType
    {
        Manager,
        Employee,
        User
    }

}
