using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace StrategieSpiel
{

    public class UnitTemplate : Model
    {
        
        public string Name;

        public int HP;

        public int Attack;

    }

    public class UnitTemplateTable : Table<UnitTemplate>
    {

        public UnitTemplateTable(MySqlConnection _connection)
            : base(_connection, "unit_templates")
        {
        }

        protected override UnitTemplate BuildModel(MySqlDataReader reader)
        {
            return new UnitTemplate
            {
                ID = (int)reader["id"],
                Name = (string)reader["name"],
                HP = (int)reader["hp"],
                Attack = (int)reader["attack"],
            };
        }

        protected override Dictionary<string, string> GetColumns(UnitTemplate model)
        {
            throw new NotImplementedException();
        }
    }

}
