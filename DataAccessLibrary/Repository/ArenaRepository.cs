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
    public class ArenaRepository : IRepository<Arena>
    {
        private readonly string _connectionString;

        public ArenaRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("appsettings.json");
        }
        public int AddEntity(Arena entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Arenas (Capacity, Location)
        VALUES (@Capacity, @Location);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@Capacity", entity.Capacity);
            command.Parameters.AddWithValue("@Location", entity.Location);

            int newArenaId = Convert.ToInt32(command.ExecuteScalar());
            return newArenaId;
        }
        public bool DeleteEntity(int id, Arena entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Arenas WHERE Arenas.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Arena entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Arenas
        SET Capacity = @Capacity,
            Location = @Location
        WHERE id = @Id";
            command.Parameters.AddWithValue("@Capacity", entity.Capacity);
            command.Parameters.AddWithValue("@Location", entity.Location);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Arena> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Arenas";

            SqlDataReader reader = command.ExecuteReader();
            List<Arena> arenas = new List<Arena>();

            while (reader.Read())
            {
                Arena arena = new Arena(
                    id: reader.GetInt32(0),
                    capacity: reader.GetInt32(1),
                    location: reader.GetString(2)
                );

                arenas.Add(arena);
            }

            return arenas;
        }
        public Arena? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Arenas WHERE Id = @entityId";
            command.Parameters.AddWithValue("@entityId", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            Arena arena = new
            (
                id: reader.GetInt32(0),
                capacity: reader.GetInt32(1),
                location: reader.GetString(2)
            );

            return arena;
        }
    }
}