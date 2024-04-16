using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class MatchService : IService<Match>
    {
        private readonly IRepository<Match> matchRepository;

        public MatchService(IRepository<Match> matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public int AddEntityService(Match entity)
        {
            return matchRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Match entity)
        {
            return matchRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Match> GetAllEntitiesService()
        {
            return matchRepository.GetAllEntities();
        }

        public Match? GetEntityService(int entityId)
        {
            return matchRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Match entity)
        {
            return matchRepository.UpdateEntity(id, entity);
        }
    }
}
