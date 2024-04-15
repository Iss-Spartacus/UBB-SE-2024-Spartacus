using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLibrary.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public Repository(/*IConfigurationManager configurationManager,*/ string tableName)
        {
            //_connectionString = configurationManager.GetConnectionString("appsettings.json");
            _tableName = tableName;
        }

        public int AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEntity(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public T? GetEntity(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
