using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class Resource : Model
    {

        public string Name;

    }

    public class ResourceTable : Table<Resource>
    {

        public ResourceTable(MySqlConnection _connection) 
            : base(_connection, "resources")
        {
        }

        public Resource Create(string _name)
        {
            try
            {
                Execute($"INSERT INTO resources (name) VALUES ({_name})");
            }
            catch(Exception)
            {
                return null;
            }

            return FindByName(_name);
        }

        public Resource FindByName(string _name)
        {
            return ReadSingle($"SELECT * FROM resource WHERE name = '{_name}'");
        }

        protected override Resource BuildModel(MySqlDataReader reader)
        {
            return new Resource
            {
                ID = (int)reader["id"],
                Name = (string)reader["name"],
            };
        }

        protected override Dictionary<string, string> GetColumns(Resource model)
        {
            throw new NotImplementedException();
        }
    }

}
