using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class MatchService : IService<Match>
    {
        private readonly MatchRepository matchRepository;

        public MatchService(MatchRepository matchRepository)
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

        public Match GetOnGoingMatchService()
        {
            var matches = GetAllEntitiesService();
            return matches.FirstOrDefault(match => match.WinnerId == 0);
        }
        
        public Match GetMatchById(int matchId)
        {
            return matchRepository.GetEntity(matchId);
        }

        public string GetEmployeeFullNameForMatch(int employeeId)
        {
            return matchRepository.GetEmployeeFullName(employeeId);
        }

        public bool getTurn(int matchId)
        {
            return matchRepository.GetCurrentTurn(matchId);
        }

        public void flipTurn(int matchId)
        {
            matchRepository.FlipCurrentTurn(matchId);
        }

        public void updateWinner(int matchId, int winnerId)
        {
            matchRepository.UpdateWinner(matchId, winnerId);
        }
    }
}
