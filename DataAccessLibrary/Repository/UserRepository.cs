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
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(User entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Users (UserName, AccountId)
        VALUES (@UserName, @AccountId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@UserName", entity.UserName);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);

            int newUserId = Convert.ToInt32(command.ExecuteScalar());
            return newUserId;
        }
        public bool DeleteEntity(int id, User entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Users WHERE Users.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, User entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Users
        SET UserName = @UserName,
            AccountId = @AccountId
        WHERE id = @Id";
            command.Parameters.AddWithValue("@UserName", entity.UserName);
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<User> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Users";

            SqlDataReader reader = command.ExecuteReader();
            List<User> users = new List<User>();

            while (reader.Read())
            {
                User user = new
                (
                    userName : reader.GetString(1),
                    accountId : reader.GetInt32(2)
                );
                user.Id = reader.GetInt32(0);

                users.Add(user);
            }

            return users;
        }

        public User? GetEntity(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}