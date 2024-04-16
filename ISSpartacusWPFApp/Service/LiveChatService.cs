using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class LiveChatService : IService<LiveChat>
    {
        private readonly IRepository<LiveChat> liveChatRepository;

        public LiveChatService(IRepository<LiveChat> liveChatRepository)
        {
            this.liveChatRepository = liveChatRepository;
        }

        public int AddEntityService(LiveChat entity)
        {
            return liveChatRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, LiveChat entity)
        {
            return liveChatRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<LiveChat> GetAllEntitiesService()
        {
            return liveChatRepository.GetAllEntities();
        }

        public LiveChat? GetEntityService(int entityId)
        {
            return liveChatRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, LiveChat entity)
        {
            return liveChatRepository.UpdateEntity(id, entity);
        }
    }
}
