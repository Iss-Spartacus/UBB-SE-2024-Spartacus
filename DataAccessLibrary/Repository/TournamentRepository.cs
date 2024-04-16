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
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly string _connectionString;

        public TournamentRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Tournament entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Tournaments (StartDateTime, EndDateTime, ArenaId, IsFinished)
        VALUES (@StartDateTime, @EndDateTime, @ArenaId, @IsFinished);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@StartDateTime", entity.StartDateTime);
            command.Parameters.AddWithValue("@EndDateTime", entity.EndDateTime);
            command.Parameters.AddWithValue("@ArenaId", entity.ArenaId);
            command.Parameters.AddWithValue("@IsFinished", entity.IsFinished);

            int newTournamentId = Convert.ToInt32(command.ExecuteScalar());
            return newTournamentId;
        }
        public bool DeleteEntity(int id, Tournament entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Tournaments WHERE Tournaments.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Tournament entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Tournaments
        SET StartDateTime = @StartDateTime,
            EndDateTime = @EndDateTime,
            ArenaId = @ArenaId,
            IsFinished = @IsFinished
        WHERE id = @Id";
            command.Parameters.AddWithValue("@StartDateTime", entity.StartDateTime);
            command.Parameters.AddWithValue("@EndDateTime", entity.EndDateTime);
            command.Parameters.AddWithValue("@ArenaId", entity.ArenaId);
            command.Parameters.AddWithValue("@IsFinished", entity.IsFinished);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Tournament> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Tournaments";

            SqlDataReader reader = command.ExecuteReader();
            List<Tournament> tournaments = new List<Tournament>();

            while (reader.Read())
            {
                Tournament tournament = new
                (
                    startDateTime : reader.GetDateTime(1),
                    endDateTime : reader.GetDateTime(2),
                    arenaId : reader.GetInt32(3),
                    isFinished : reader.GetBoolean(4)
                );

                // Retrieve fighters and matches for the tournament
                tournament.Fighters = GetFightersForTournament(tournament.Id);
                tournament.Matches = GetMatchesForTournament(tournament.Id);

                tournaments.Add(tournament);
            }

            return tournaments;
        }

        private List<Employee> GetFightersForTournament(int id)
        {
            List<Employee> fighters = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Select employee_id and employee details from EmployeesInTournament and Employee tables
                string query = @"
                SELECT E.employee_id, E.employee_fullName, E.employee_power, E.employee_money, E.employee_photoFilePath, E.employee_readyToFight, E.employee_account
                FROM EmployeesInTournament AS EIT
                INNER JOIN Employee AS E ON EIT.employee_id = E.employee_id
                WHERE EIT.tournament_id = @tournamentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tournamentId", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Create Employee object from the data
                            Employee fighter = new
                            (                            
                                fullName : reader.GetString(1), // Assuming employee_fullName is the second column
                                power : reader.GetInt32(2), // Assuming employee_power is the third column
                                money : reader.GetInt32(3), // Assuming employee_money is the fourth column
                                photoFilePath : reader.GetString(4), // Assuming employee_photoFilePath is the fifth column
                                readyToFight : reader.GetBoolean(5), // Assuming employee_readyToFight is the sixth column
                                accountId : reader.GetInt32(6)
                            );
                            fighters.Add(fighter);
                        }
                    }
                }
            }

            return fighters;
        }


        public Tournament? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Tournaments WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Tournament tournament = new
            (
                startDateTime: reader.GetDateTime(1),
                endDateTime: reader.GetDateTime(2),
                arenaId: reader.GetInt32(3),
                isFinished: reader.GetBoolean(4)
            );
            tournament.Id = reader.GetInt32(0);

            // Retrieve fighters and matches for the tournament
            tournament.Fighters = GetFightersForTournament(tournament.Id);
            tournament.Matches = GetMatchesForTournament(tournament.Id);

            return tournament;
        }

        private List<Match> GetMatchesForTournament(int id)
        {
            throw new NotImplementedException();
        }
    }
}