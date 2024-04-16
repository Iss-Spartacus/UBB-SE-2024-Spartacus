using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLoader;
using DataAccessLibrary.Model;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString;

        public EmployeeRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Employee entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Employee (FullName, Power, Money, PhotoFilePath, ReadyToFight, AccountId)
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
            command.CommandText = "DELETE FROM Employee WHERE Employees.id = @id";
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
        UPDATE Employee
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
            command.CommandText = "SELECT * FROM Employee";

            SqlDataReader reader = command.ExecuteReader();
            List<Employee> employees = new List<Employee>();

            while (reader.Read())
            {
                Employee employee = new
                (
                    fullName: reader.GetString(1),
                    power: reader.GetInt32(2),
                    money: reader.GetInt32(3),
                    photoFilePath: reader.GetString(4),
                    readyToFight: reader.GetBoolean(5),
                    accountId: reader.GetInt32(6)
                );
                employee.Id = reader.GetInt32(0);

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
            command.CommandText = "SELECT * FROM Employee WHERE employee_id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Employee employee = new
            (
                fullName: reader.GetString(1),
                power: reader.GetInt32(2),
                money: reader.GetInt32(3),
                photoFilePath: reader.GetString(4),
                readyToFight: reader.GetBoolean(5),
                accountId: reader.GetInt32(6)
            );
            employee.Id = reader.GetInt32(0);

            return employee;
        }
    }
}