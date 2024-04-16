using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class TournamentService : IService<Tournament>
    {
        private readonly IRepository<Tournament> tournamentRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        public int AddEntityService(Tournament entity)
        {
            return tournamentRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Tournament entity)
        {
            return tournamentRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Tournament> GetAllEntitiesService()
        {
            return tournamentRepository.GetAllEntities();
        }

        public Tournament? GetEntityService(int entityId)
        {
            return tournamentRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Tournament entity)
        {
            return tournamentRepository.UpdateEntity(id, entity);
        }
    }
}
