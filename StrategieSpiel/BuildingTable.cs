using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class Building : Model
    {

        public int BuildingTemplateID;

        public int PlayerID;

        public int Level;

    }

    public class BuildingTable : Table<Building>
    {

        public BuildingTable(MySqlConnection _connection)
            : base(_connection, "buildings")
        {
        }

        protected override Building BuildModel(MySqlDataReader reader)
        {
            return new Building
            {
                ID = (int)reader["id"],
                PlayerID = (int)reader["player_id"],
                BuildingTemplateID = (int)reader["building_template_id"],
                Level = (int)reader["level"],
            };
        }

        protected override Dictionary<string, string> GetColumns(Building model)
        {
            throw new NotImplementedException();
        }
    }

}
