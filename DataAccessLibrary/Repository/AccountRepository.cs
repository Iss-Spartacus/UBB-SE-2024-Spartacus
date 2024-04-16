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
    public class AccountRepository : IRepository<Account>
    {
        private readonly string _connectionString;

        public AccountRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Account entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Account (account_email, account_username, account_password, account_age)
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
            command.CommandText = "DELETE FROM Account WHERE Account.id = @id";
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
                UPDATE Account
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
            command.CommandText = "SELECT * FROM Account";

            SqlDataReader reader = command.ExecuteReader();
            List<Account> accounts = new List<Account>();

            while (reader.Read())
            {
                Account account = new Account
                (
                    email: reader.GetString(1),
                    username: reader.GetString(2),
                    password: reader.GetString(3),
                    isAdult: reader.GetBoolean(4)
                );

                accounts.Add(account);
            }

            return accounts;
        }

        public Account? GetEntity(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
