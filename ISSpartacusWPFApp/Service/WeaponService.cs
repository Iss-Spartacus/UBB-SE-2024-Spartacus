using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class WeaponService : IService<Weapon>
    {
        private readonly IRepository<Weapon> weaponRepository;

        public WeaponService(IRepository<Weapon> weaponRepository)
        {
            this.weaponRepository = weaponRepository;
        }

        public int AddEntityService(Weapon entity)
        {
            return weaponRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Weapon entity)
        {
            return weaponRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Weapon> GetAllEntitiesService()
        {
            return weaponRepository.GetAllEntities();
        }

        public Weapon? GetEntityService(int entityId)
        {
            return weaponRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Weapon entity)
        {
            return weaponRepository.UpdateEntity(id, entity);
        }
    }
}
