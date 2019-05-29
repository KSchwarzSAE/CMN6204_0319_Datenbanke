using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class PlayerResource : Model
    {

        public int ResourceID;

        public int PlayerID;

        public int Amount;

    }

    public class PlayerResourceTable : Table<PlayerResource>
    {

        public PlayerResourceTable(MySqlConnection _connection)
            : base(_connection, "player_resources")
        {
        }

        protected override PlayerResource BuildModel(MySqlDataReader reader)
        {
            return new PlayerResource
            {
                ID = (int)reader["id"],
                PlayerID = (int)reader["player_id"],
                ResourceID = (int)reader["resource_id"],
                Amount = (int)reader["amount"],
            };
        }

        public PlayerResource FindByPlayerIDAndResourceID(int playerID, int resourceID)
        {
            return ReadSingle($"SELECT * FROM player_resources WHERE player_id = {playerID} AND resource_id = {resourceID}");
        }

        protected override Dictionary<string, string> GetColumns(PlayerResource model)
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();

            columns["player_id"] = model.PlayerID.ToString();
            columns["resource_id"] = model.ResourceID.ToString();
            columns["amount"] = model.Amount.ToString();

            return columns;
        }
    }

}
