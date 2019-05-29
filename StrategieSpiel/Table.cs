using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategieSpiel
{

    public abstract class Model
    {

        public int ID;

    }

    public abstract class Table<ModelType> where ModelType : Model
    {

        private MySqlConnection m_connection;

        private string tableName;

        public Table(MySqlConnection _connection, string _tableName)
        {
            m_connection = _connection;
            tableName = _tableName;
        }

        public ModelType FindById(int _id)
        {
            return ReadSingle($"SELECT * FROM {tableName} WHERE id = {_id}");
        }

        protected ModelType ReadSingle(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, m_connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    return BuildModel(reader);
                else
                    return null;
            }
        }

        protected ModelType[] ReadMany(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, m_connection);

            using (var reader = command.ExecuteReader())
            {
                List<ModelType> models = new List<ModelType>();
                while (reader.Read())
                    models.Add(BuildModel(reader));
                return models.ToArray();
            }
        }

        public ModelType[] All()
        {
            return ReadMany($"SELECT * FROM {tableName}");
        }

        protected void Execute(string _sql)
        {
            (new MySqlCommand(_sql, m_connection)).ExecuteNonQuery();
        }
        
        public ModelType Create(ModelType model)
        {
            var columns = GetColumns(model);

            string names = "";
            string values = "";

            foreach(var pair in columns)
            {
                names += pair.Key + ",";
                values += "'" + pair.Value + "',";
            }

            names = names.TrimEnd(',');
            values = values.TrimEnd(',');

            MySqlCommand command 
                = new MySqlCommand($"INSERT INTO {tableName} ({names}) VALUES ({values})", m_connection);
            command.ExecuteNonQuery();

            return FindById((int)command.LastInsertedId);
        }

        public void Update(ModelType model)
        {
            var columns = GetColumns(model);

            string updates = "";

            foreach (var pair in columns)
            {
                updates = $"{pair.Key}='{pair.Value}',";
            }
            updates = updates.TrimEnd(',');

            Execute($"UPDATE {tableName} SET {updates} WHERE id={model.ID}");
        }

        protected abstract ModelType BuildModel(MySqlDataReader reader);

        protected abstract Dictionary<string, string> GetColumns(ModelType model);

    }

}
