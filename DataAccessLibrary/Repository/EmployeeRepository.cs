using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfigurationManager configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("appsettings.json");
        }
        public int AddEntity(Employee entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Employees (FullName, Power, Money, PhotoFilePath, ReadyToFight, AccountId)
        VALUES (@FullName, @Power, @Money, @PhotoFilePath, @ReadyToFight, @AccountId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@FullName", entity.FullName);
            command.Parameters.AddWithValue("@Power", entity.Power);
            command.Parameters.AddWithValue("@Money", entity.Money);
            command.Parameters.AddWithValue("@PhotoFilePath", entity.PhotoFilePath);
            command.Parameters.AddWithValue("@ReadyToFight", entity.ReadyToFight);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);

            int newEmployeeId = Convert.ToInt32(command.ExecuteScalar());
            return newEmployeeId;
        }
        public bool DeleteEntity(int id, Employee entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Employees WHERE Employees.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Employee entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Employees
        SET FullName = @FullName,
            Power = @Power,
            Money = @Money,
            PhotoFilePath = @PhotoFilePath,
            ReadyToFight = @ReadyToFight,
            AccountId = @AccountId
        WHERE id = @Id";
            command.Parameters.AddWithValue("@FullName", entity.FullName);
            command.Parameters.AddWithValue("@Power", entity.Power);
            command.Parameters.AddWithValue("@Money", entity.Money);
            command.Parameters.AddWithValue("@PhotoFilePath", entity.PhotoFilePath);
            command.Parameters.AddWithValue("@ReadyToFight", entity.ReadyToFight);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Employee> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Employees";

            SqlDataReader reader = command.ExecuteReader();
            List<Employee> employees = new List<Employee>();

            while (reader.Read())
            {
                Employee employee = new()
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Power = reader.GetInt32(2),
                    Money = reader.GetInt32(3),
                    PhotoFilePath = reader.GetString(4),
                    ReadyToFight = reader.GetBoolean(5),
                    AccountId = reader.GetInt32(6),
                };

                employees.Add(employee);
            }

            return employees;
        }
        public Employee? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Employees WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Employee employee = new()
            {
                Id = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Power = reader.GetInt32(2),
                Money = reader.GetInt32(3),
                PhotoFilePath = reader.GetString(4),
                ReadyToFight = reader.GetBoolean(5),
                AccountId = reader.GetInt32(6),
            };

            return employee;
        }
    }
}