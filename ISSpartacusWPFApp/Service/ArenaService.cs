using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISSpartacusWPFApp.Service
{
    public class ArenaService : IService<Arena>
    {
        private readonly IRepository<Arena> arenaRepository;

        public ArenaService(IRepository<Arena> arenaRepository)
        {
            this.arenaRepository = arenaRepository;
        }

        public int AddEntityService(Arena entity)
        {
            return arenaRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Arena entity)
        {
            return arenaRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Arena> GetAllEntitiesService()
        {
            return arenaRepository.GetAllEntities();
        }

        public Arena? GetEntityService(int entityId)
        {
            return arenaRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Arena entity)
        {
            return arenaRepository.UpdateEntity(id, entity);
        }
    }
}
