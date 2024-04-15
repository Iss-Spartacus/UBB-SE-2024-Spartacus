using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly string _connectionString;

        public TournamentRepository(IConfigurationManager configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("appsettings.json");
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
                Tournament tournament = new()
                {
                    Id = reader.GetInt32(0),
                    StartDateTime = reader.GetDateTime(1),
                    EndDateTime = reader.GetDateTime(2),
                    ArenaId = reader.GetInt32(3),
                    IsFinished = reader.GetBoolean(4),
                };

                // Retrieve fighters and matches for the tournament
                tournament.Fighters = GetFightersForTournament(tournament.Id);
                tournament.Matches = GetMatchesForTournament(tournament.Id);

                tournaments.Add(tournament);
            }

            return tournaments;
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

            Tournament tournament = new()
            {
                Id = reader.GetInt32(0),
                StartDateTime = reader.GetDateTime(1),
                EndDateTime = reader.GetDateTime(2),
                ArenaId = reader.GetInt32(3),
                IsFinished = reader.GetBoolean(4),
            };

            // Retrieve fighters and matches for the tournament
            tournament.Fighters = GetFightersForTournament(tournament.Id);
            tournament.Matches = GetMatchesForTournament(tournament.Id);

            return tournament;
        }
    }
}