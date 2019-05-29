using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class BuildingTemplate : Model
    {

        public string Name;

        public int ResourceNeededID;

        public int ResourceNeededAmount;

        public int ResourceProducedID;

        public int ResourceProducedAmount;

    }

    public class BuildingTemplateTable : Table<BuildingTemplate>
    {

        public BuildingTemplateTable(MySqlConnection _connection)
            : base(_connection, "building_templates")
        {
        }

        protected override BuildingTemplate BuildModel(MySqlDataReader reader)
        {
            return new BuildingTemplate
            {
                ID = (int)reader["id"],
                Name = (string)reader["name"],
                ResourceNeededID = (int)reader["resource_needed_id"],
                ResourceNeededAmount = (int)reader["resource_needed_amount"],
                ResourceProducedID = (int)reader["resource_produced_id"],
                ResourceProducedAmount = (int)reader["resource_produced_amount"],
            };
        }

        protected override Dictionary<string, string> GetColumns(BuildingTemplate model)
        {
            throw new NotImplementedException();
        }
    }

}
