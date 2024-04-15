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
    public class LiveChatRepository : IRepository<LiveChat>
    {
        private readonly string _connectionString;

        public LiveChatRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(LiveChat entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO LiveChats (MatchId, Content, TimeStamp)
        VALUES (@MatchId, @Content, @TimeStamp);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@MatchId", entity.MatchId);
            command.Parameters.AddWithValue("@Content", entity.Content);
            command.Parameters.AddWithValue("@TimeStamp", entity.TimeStamp);

            int newLiveChatId = Convert.ToInt32(command.ExecuteScalar());
            return newLiveChatId;
        }
        public bool DeleteEntity(int id, LiveChat entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM LiveChats WHERE LiveChats.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, LiveChat entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE LiveChats
        SET MatchId = @MatchId,
            Content = @Content,
            TimeStamp = @TimeStamp
        WHERE id = @Id";
            command.Parameters.AddWithValue("@MatchId", entity.MatchId);
            command.Parameters.AddWithValue("@Content", entity.Content);
            command.Parameters.AddWithValue("@TimeStamp", entity.TimeStamp);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<LiveChat> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM LiveChats";

            SqlDataReader reader = command.ExecuteReader();
            List<LiveChat> liveChats = new List<LiveChat>();

            while (reader.Read())
            {
                LiveChat liveChat = new
                (
                    id : reader.GetInt32(0),
                    matchId : reader.GetInt32(1),
                    content : reader.GetString(2),
                    timeStamp : reader.GetDateTime(3)
                );

                liveChats.Add(liveChat);
            }

            return liveChats;
        }
        public LiveChat? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM LiveChats WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            LiveChat liveChat = new
            (
                id: reader.GetInt32(0),
                matchId: reader.GetInt32(1),
                content: reader.GetString(2),
                timeStamp: reader.GetDateTime(3)
            );

            return liveChat;
        }
    }
}