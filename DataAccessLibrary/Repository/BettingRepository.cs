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
    public class BettingRepository : IRepository<Betting>
    {
        private readonly string _connectionString;

        public BettingRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Betting entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Bettings (AccountId, Amount, BetOnId, InitialOdd, MatchId)
        VALUES (@AccountId, @Amount, @BetOnId, @InitialOdd, @MatchId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);
            command.Parameters.AddWithValue("@Amount", entity.Amount);
            command.Parameters.AddWithValue("@BetOnId", entity.BetOnId);
            command.Parameters.AddWithValue("@InitialOdd", entity.InitialOdd);
            command.Parameters.AddWithValue("@MatchId", entity.MatchId);

            int newBettingId = Convert.ToInt32(command.ExecuteScalar());
            return newBettingId;
        }
        public bool DeleteEntity(int id, Betting entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Bettings WHERE Bettings.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Betting entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Bettings
        SET AccountId = @AccountId,
            Amount = @Amount,
            BetOnId = @BetOnId,
            InitialOdd = @InitialOdd,
            MatchId = @MatchId
        WHERE id = @Id";
            command.Parameters.AddWithValue("@AccountId", entity.AccountId);
            command.Parameters.AddWithValue("@Amount", entity.Amount);
            command.Parameters.AddWithValue("@BetOnId", entity.BetOnId);
            command.Parameters.AddWithValue("@InitialOdd", entity.InitialOdd);
            command.Parameters.AddWithValue("@MatchId", entity.MatchId);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Betting> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Bettings";

            SqlDataReader reader = command.ExecuteReader();
            List<Betting> bettings = new List<Betting>();

            while (reader.Read())
            {
                Betting betting = new
                (
                    accountId : reader.GetInt32(1),
                    amount : reader.GetFloat(2),
                    betOnId : reader.GetInt32(3),
                    initialOdd : reader.GetInt32(4),
                    matchId : reader.GetInt32(5)
                );
                betting.Id = reader.GetInt32(0);

                bettings.Add(betting);
            }

            return bettings;
        }
        public Betting? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Bettings WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Betting betting = new
            (
                accountId: reader.GetInt32(1),
                amount: reader.GetFloat(2),
                betOnId: reader.GetInt32(3),
                initialOdd: reader.GetInt32(4),
                matchId: reader.GetInt32(5)
            );

            return betting;
        }
    }
}