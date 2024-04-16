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
        INSERT INTO Matches (match_tournament, employee1, employee2, match_registrationDate, match_winner)
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
            command.CommandText = "SELECT * FROM Matches WHERE match_id = @entityId";
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

        private List<MatchObserver> GetObserversForMatch(int matchId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        SELECT UserId, MatchId
        FROM MatchObservers
        WHERE MatchId = @MatchId";
            command.Parameters.AddWithValue("@MatchId", matchId);

            List<MatchObserver> observers = new List<MatchObserver>();

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        // MatchId is not necessary to read since it's the same for all rows in this context
                        MatchObserver observer = new MatchObserver(userId, matchId);
                        observers.Add(observer);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log exception or handle it according to your error handling policy
                Console.WriteLine("An error occurred while retrieving match observers: " + ex.Message);
            }

            return observers;
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

        public string GetEmployeeFullName(int employeeId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT employee_fullName FROM Employee WHERE employee_id = @employeeId";
            command.Parameters.AddWithValue("@employeeId", employeeId);

            string fullName = command.ExecuteScalar() as string;

            return fullName ?? string.Empty;
        }

        public bool GetCurrentTurn(int matchId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT match_employee2Turn FROM Matches WHERE match_id = @MatchId";
                    command.Parameters.AddWithValue("@MatchId", matchId);

                    return (bool)command.ExecuteScalar();
                }
            }
        }

        public bool FlipCurrentTurn(int matchId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // First, get the current value of match_employee2Turn
                bool currentTurn = GetCurrentTurn(matchId);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                UPDATE Matches
                SET match_employee2Turn = @NewTurn
                WHERE match_id = @MatchId";
                    command.Parameters.AddWithValue("@NewTurn", !currentTurn); // Flip the boolean value
                    command.Parameters.AddWithValue("@MatchId", matchId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the update was successful
                }
            }
        }

        public bool UpdateWinner(int matchId, int winnerId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
            UPDATE Matches
            SET match_winner = @WinnerId
            WHERE match_id = @MatchId";

                    command.Parameters.AddWithValue("@WinnerId", winnerId);
                    command.Parameters.AddWithValue("@MatchId", matchId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if the update was successful
                }
            }
        }

        public int GetEmployeeAccount(int employeeId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT employee_id FROM Employee WHERE employee_account = @employeeId";
                command.Parameters.AddWithValue("@employeeId", employeeId);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }
                else
                {
                    throw new InvalidOperationException("Employee not found or account not set.");
                }
            }
        }





    }
}