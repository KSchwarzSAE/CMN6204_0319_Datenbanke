using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class Player : Model
    {
        
        public string Name;

    }

    public class PlayerTable : Table<Player>
    {

        public PlayerTable(MySqlConnection _connection) 
            : base(_connection, "players")
        {
        }

        public Player Create(string _name)
        {
            try
            {
                Execute($"INSERT INTO players (name) VALUES ({_name})");
            }
            catch(Exception)
            {
                return null;
            }

            return FindByName(_name);
        }

        public Player FindByName(string _name)
        {
            return ReadSingle($"SELECT * FROM players WHERE name = '{_name}'");
        }

        protected override Player BuildModel(MySqlDataReader reader)
        {
            return new Player
            {
                ID = (int)reader["id"],
                Name = (string)reader["name"],
            };
        }

        protected override Dictionary<string, string> GetColumns(Player model)
        {
            throw new NotImplementedException();
        }
    }

}
