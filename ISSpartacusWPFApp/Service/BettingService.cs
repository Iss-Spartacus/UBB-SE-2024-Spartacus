using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class BettingService : IService<Betting>
    {
        private readonly IRepository<Betting> bettingRepository;

        public BettingService(IRepository<Betting> bettingRepository)
        {
            this.bettingRepository = bettingRepository;
        }

        public int AddEntityService(Betting entity)
        {
            return bettingRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Betting entity)
        {
            return bettingRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Betting> GetAllEntitiesService()
        {
            return bettingRepository.GetAllEntities();
        }

        public Betting? GetEntityService(int entityId)
        {
            return bettingRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Betting entity)
        {
            return bettingRepository.UpdateEntity(id, entity);
        }
    }
}
