using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class UserService : IService<User>
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public int AddEntityService(User entity)
        {
            return userRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, User entity)
        {
            return userRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<User> GetAllEntitiesService()
        {
            return userRepository.GetAllEntities();
        }

        public User? GetEntityService(int entityId)
        {
            return userRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, User entity)
        {
            return userRepository.UpdateEntity(id, entity);
        }
    }
}
