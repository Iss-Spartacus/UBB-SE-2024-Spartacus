using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLoader;
using DataAccessLibrary.Model;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace DataAccessLibrary.Repository
{
    public class MatchRepository : IRepository<Match>
    {
        private readonly string _connectionString;

        public MatchRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Match entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Matches (TournamentId, Employee1Id, Employee2Id, RegistrationDate, WinnerId)
        VALUES (@TournamentId, @Employee1Id, @Employee2Id, @RegistrationDate, @WinnerId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TournamentId", entity.TournamentId);
            command.Parameters.AddWithValue("@Employee1Id", entity.Employee1Id);
            command.Parameters.AddWithValue("@Employee2Id", entity.Employee2Id);
            command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);
            command.Parameters.AddWithValue("@WinnerId", entity.WinnerId);

            int newMatchId = Convert.ToInt32(command.ExecuteScalar());
            return newMatchId;
        }
        public bool DeleteEntity(int id, Match entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Matches WHERE Matches.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Match entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Matches
        SET TournamentId = @TournamentId,
            Employee1Id = @Employee1Id,
            Employee2Id = @Employee2Id,
            RegistrationDate = @RegistrationDate,
            WinnerId = @WinnerId
        WHERE id = @Id";
            command.Parameters.AddWithValue("@TournamentId", entity.TournamentId);
            command.Parameters.AddWithValue("@Employee1Id", entity.Employee1Id);
            command.Parameters.AddWithValue("@Employee2Id", entity.Employee2Id);
            command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);
            command.Parameters.AddWithValue("@WinnerId", entity.WinnerId);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Match> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Matches";

            SqlDataReader reader = command.ExecuteReader();
            List<Match> matches = new List<Match>();

            while (reader.Read())
            {
                Match match = new
                (
                    tournamentId : reader.GetInt32(1),
                    employee1Id : reader.GetInt32(2),
                    employee2Id : reader.GetInt32(3),
                    registrationDate : reader.GetDateTime(4),
                    winnerId : reader.GetInt32(5)
                );
                match.Id = reader.GetInt32(0);

                // Retrieve observers for the match
                //match.Observers = GetObserversForMatch(match.Id);

                matches.Add(match);
            }

            return matches;
        }
        public Match? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Matches WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Match match = new
            (
                tournamentId: reader.GetInt32(1),
                employee1Id: reader.GetInt32(2),
                employee2Id: reader.GetInt32(3),
                registrationDate: reader.GetDateTime(4),
                winnerId: reader.GetInt32(5)
            );

            match.Id = reader.GetInt32(0);

            // Retrieve observers for the match
            //match.Observers = GetObserversForMatch(match.Id);

            return match;
        }

        private List<MatchObserver> GetObserversForMatch(int id)
        {
            throw new NotImplementedException();
        }

        public void AddObserver(int matchId, MatchObserver observer)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO MatchObservers (MatchId, EmployeeId)
        VALUES (@MatchId, @EmployeeId);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@MatchId", matchId);
            command.Parameters.AddWithValue("@EmployeeId", observer.UserId);

            int newObserverId = Convert.ToInt32(command.ExecuteScalar());
        }
        public void RemoveObserver(int matchId, int observerId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM MatchObservers WHERE MatchId = @matchId AND EmployeeId = @observerId";
            command.Parameters.AddWithValue("@matchId", matchId);
            command.Parameters.AddWithValue("@observerId", observerId);

            command.ExecuteNonQuery();
        }
    }
}