using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class ManagerService : IService<Manager>
    {
        private readonly IRepository<Manager> managerRepository;

        public ManagerService(IRepository<Manager> managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public int AddEntityService(Manager entity)
        {
            return managerRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Manager entity)
        {
            return managerRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Manager> GetAllEntitiesService()
        {
            return managerRepository.GetAllEntities();
        }

        public Manager? GetEntityService(int entityId)
        {
            return managerRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Manager entity)
        {
            return managerRepository.UpdateEntity(id, entity);
        }
    }
}
