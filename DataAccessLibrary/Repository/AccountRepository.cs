using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Model

namespace DataAccessLibrary.Repository
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly string _connectionString;

        public AccountRepository(IConfigurationManager configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("appsettings.json");
        }
        public int AddEntity(Account entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Accounts (Email, Username, Password, IsAdult)
                VALUES (@Email, @Username, @Password, @IsAdult);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@Username", entity.Username);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@IsAdult", entity.IsAdult);

            int newAccountId = Convert.ToInt32(command.ExecuteScalar());
            return newAccountId;
        }
        public bool DeleteEntity(int id, Account entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Accounts WHERE Accounts.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Account entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                UPDATE Accounts
                SET Email = @Email,
                    Username = @Username,
                    Password = @Password,
                    IsAdult = @IsAdult
                WHERE id = @Id";
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@Username", entity.Username);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@IsAdult", entity.IsAdult);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Account> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Accounts";

            SqlDataReader reader = command.ExecuteReader();
            List<Account> accounts = new List<Account>();

            while (reader.Read())
            {
                Account account = new()
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Username = reader.GetString(2),
                    Password = reader.GetString(3),
                    IsAdult = reader.GetBoolean(4),
                };

                accounts.Add(account);
            }

            return accounts;
        }
        public IEnumerable<Account> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Accounts";

            SqlDataReader reader = command.ExecuteReader();
            List<Account> accounts = new List<Account>();

            while (reader.Read())
            {
                Account account = new()
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Username = reader.GetString(2),
                    Password = reader.GetString(3),
                    IsAdult = reader.GetBoolean(4),
                };

                accounts.Add(account);
            }

            return accounts;
        }
    }
