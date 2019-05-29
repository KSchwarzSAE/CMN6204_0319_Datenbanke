using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMN6204_0319_Datenbanken
{

    public struct User
    {

        public int ID;

        public string Name;

        public string Email;

        public string PasswordHash;

    }

    public class UserTable
    {

        private MySqlConnection m_connection;

        public UserTable(MySqlConnection _connection)
        {
            m_connection = _connection;
        }

        private MySqlCommand CreateCommand()
        {
            return new MySqlCommand("", m_connection);
        }

        public User? FindByName(string _name)
        {
            MySqlCommand command = CreateCommand();
            
            command.CommandText = $"SELECT * FROM users WHERE name = '{_name}'";

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    return new User
                    {
                        Name = (string)reader["name"],
                        Email = (string)reader["email"],
                        PasswordHash = (string)reader["password_hash"],
                        ID = (int)reader["id"]
                    };
            }

            return null;
        }

    }

}
