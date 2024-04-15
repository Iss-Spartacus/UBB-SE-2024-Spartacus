using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository
{
    public interface IRepository<T> where T : class
    {
        int AddEntity(T entity);
        bool DeleteEntity(int id, T entity);
        bool UpdateEntity(int id, T entity);
        IEnumerable<T> GetAllEntities();
        T? GetEntity(int entityId);
    }
}
