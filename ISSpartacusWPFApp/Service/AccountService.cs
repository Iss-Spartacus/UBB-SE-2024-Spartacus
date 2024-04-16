using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Modules;

namespace ISSpartacusWPFApp.Service
{
    public class AccountService : IService<Account>
    {
        private readonly IRepository<Account> accountRepository;

        public AccountService(IRepository<Account> accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public int AddEntityService(Account entity)
        {
            if (!Validator.ValidateEmail(entity.Email))
                throw new Exception("Email is not valid!");
            if (!Validator.ValidatePassword(entity.Password))
                throw new Exception("Password is not valid!");

            return accountRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Account entity)
        {
            return accountRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Account> GetAllEntitiesService()
        {
            return accountRepository.GetAllEntities();
        }

        public Account? GetEntityService(int entityId)
        {
            return accountRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Account entity)
        {
            return accountRepository.UpdateEntity(id, entity);
        }

        public Account GetAccountByEmailService(string email)
        {
            var accounts = GetAllEntitiesService();
            return accounts.FirstOrDefault(acc => acc.Email == email);
        }
    }
}