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
    public class ManagerRepository : IRepository<Manager>
    {
        private readonly string _connectionString;

        public ManagerRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("appsettings.json");
        }
        public int AddEntity(Manager entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Managers (Name, AccountId)
        VALUES (@Name, @AccountId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);

            int newManagerId = Convert.ToInt32(command.ExecuteScalar());
            return newManagerId;
        }
        public bool DeleteEntity(int id, Manager entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Managers WHERE Managers.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Manager entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Managers
        SET Name = @Name,
            AccountId = @AccountId
        WHERE id = @Id";
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Manager> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Managers";

            SqlDataReader reader = command.ExecuteReader();
            List<Manager> managers = new List<Manager>();

            while (reader.Read())
            {
                Manager manager = new
                (
                    id : reader.GetInt32(0),
                    name : reader.GetString(1),
                    accountId : reader.GetInt32(2)
                );

                managers.Add(manager);
            }

            return managers;
        }
        public Manager? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Managers WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Manager manager = new
            (
                id: reader.GetInt32(0),
                name: reader.GetString(1),
                accountId: reader.GetInt32(2)
            );

            return manager;
        }
    }
}