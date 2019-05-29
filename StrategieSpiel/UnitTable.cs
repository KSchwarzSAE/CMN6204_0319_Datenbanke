using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class Unit : Model
    {
        
        public int UnitTemplateID;

        public int PlayerID;

        public int HP;

    }

    public class UnitTable : Table<Unit>
    {

        public UnitTable(MySqlConnection _connection)
            : base(_connection, "units")
        {
        }

        protected override Unit BuildModel(MySqlDataReader reader)
        {
            return new Unit
            {
                ID = (int)reader["id"],
                PlayerID = (int)reader["player_id"],
                UnitTemplateID = (int)reader["unit_template_id"],
                HP = (int)reader["hp"],
            };
        }

        protected override Dictionary<string, string> GetColumns(Unit model)
        {
            throw new NotImplementedException();
        }
    }

}
