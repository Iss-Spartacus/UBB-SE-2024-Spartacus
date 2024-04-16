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
    public class WeaponRepository : IRepository<Weapon>
    {
        private readonly string _connectionString;
        public WeaponRepository(Configuration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("connectionSpartacus");
        }
        public int AddEntity(Weapon entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        INSERT INTO Weapons (Name, Power, Type, Price, Availability)
        VALUES (@Name, @Power, @Type, @Price, @Availability);
        SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Power", entity.Power);
            command.Parameters.AddWithValue("@Type", entity.Type);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Availability", entity.Availability);

            int newWeaponId = Convert.ToInt32(command.ExecuteScalar());
            return newWeaponId;
        }
        public bool DeleteEntity(int id, Weapon entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Weapons WHERE Weapons.id = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool UpdateEntity(int id, Weapon entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
        UPDATE Weapon
        SET weapon_name = @Name,
            weapon_power = @Power,
            weapon_type = @Type,
            weapon_price = @Price,
            weapon_availability = @Availability
        WHERE Weapon.weapon_id = @id";
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Power", entity.Power);
            command.Parameters.AddWithValue("@Type", entity.Type);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Availability", entity.Availability);
            command.Parameters.AddWithValue("@id", id);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool zUpdateEntityByName(string name, Weapon entity)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            UPDATE Weapon
                SET weapon_power = @Power,
                    weapon_type = @Type,
                    weapon_price = @Price,
                    weapon_availability = @Availability
            WHERE Weapon.weapon_name = @OriginalName";
            command.Parameters.AddWithValue("@Power", entity.Power);
            command.Parameters.AddWithValue("@Type", entity.Type);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Availability", entity.Availability);
            command.Parameters.AddWithValue("@OriginalName", name);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public IEnumerable<Weapon> GetAllEntities()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Weapon";

            SqlDataReader reader = command.ExecuteReader();

            List<Weapon> weapons = new();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int power = reader.GetInt32(2);
                string type = reader.GetString(3);
                int price = reader.GetInt32(4);
                bool availability = reader.GetBoolean(5);

                Weapon weapon = new(name, power, type, price, availability);
                weapons.Add(weapon);
            }

            return weapons;
        }


        public Weapon? GetEntity(int entityId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Weapons WHERE id = @id";
            command.Parameters.AddWithValue("@id", entityId);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int power = reader.GetInt32(2);
                string type = reader.GetString(3);
                int price = reader.GetInt32(4);
                bool availability = reader.GetBoolean(5);

                Weapon weapon = new Weapon(name, power, type, price, availability);
                return weapon;
            }
            else
            {
                return null; // Weapon not found
            }
        }
        
    }
}