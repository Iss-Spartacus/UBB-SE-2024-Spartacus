using DataAccessLibrary.Repository;
using DataAccessLibrary.Model;
using System.Collections.Generic;

namespace ISSpartacusWPFApp.Service
{
    public class EmployeeService : IService<Employee>
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public int AddEntityService(Employee entity)
        {
            return employeeRepository.AddEntity(entity);
        }

        public bool DeleteEntityService(int id, Employee entity)
        {
            return employeeRepository.DeleteEntity(id, entity);
        }

        public IEnumerable<Employee> GetAllEntitiesService()
        {
            return employeeRepository.GetAllEntities();
        }

        public Employee? GetEntityService(int entityId)
        {
            return employeeRepository.GetEntity(entityId);
        }

        public bool UpdateEntityService(int id, Employee entity)
        {
            return employeeRepository.UpdateEntity(id, entity);
        }
    }
}
