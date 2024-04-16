using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSpartacusWPFApp.Service
{
    internal interface IService<T> where T : class
    {
        int AddEntityService(T entity);
        bool DeleteEntityService(int id, T entity);
        bool UpdateEntityService(int id, T entity);
        IEnumerable<T> GetAllEntitiesService();
        T? GetEntityService(int entityId);
    }
}
