using DataAccessLibrary.Modules;
using DataAccessLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSpartacusWPFApp.Service
{
    public class LoginService
    {
        public AccountType AccountType { get; private set; }
        public int AccountId { get; private set; }

        public string ValidateLogin(string email, string password)
        {
            if (!Validator.ValidateEmail(email))
                return "Invalid email";

            if (!Validator.ValidatePassword(password))
                return "Invalid password";

            ConfigurationLoader.Configuration config = new ConfigurationLoader.Configuration();
            config.LoadFromJson("ConfigurationFile.json");

            AccountRepository accountRepository = new AccountRepository(config);
            AccountService accountService = new AccountService(accountRepository);

            var accounts = accountService.GetAllEntitiesService();

            foreach (var account in accounts)
            {
                if (account.Email == email && account.Password == password)
                {
                    AccountType = AccountVerifier.VerifyAccountType(email);
                    AccountId = account.Id;
                    return "Success";
                }
            }

            return "Invalid email and password combination";
        }
    }

}
