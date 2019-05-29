using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMN6204_0319_Datenbanken
{

    public struct HighscoreEntry
    {

        public string Name;

        public int Score;

    }

    public class Highscore
    {

        private MySqlConnection m_connection;

        public Highscore(MySqlConnection _connection)
        {
            m_connection = _connection;
        }

        public HighscoreEntry[] GetHighscore(int _page = 1, int _perPage = 10)
        {
            MySqlCommand command = new MySqlCommand("", m_connection);
            command.CommandText = "" +
                "SELECT users.name, highscore.score FROM highscore" +
                " INNER JOIN users ON users.id = user_id" +
                " ORDER BY score DESC" +
                $" LIMIT {_perPage} OFFSET {(_page - 1) * _perPage}";

            HighscoreEntry[] highscoreEntries = new HighscoreEntry[_perPage];

            using (var reader = command.ExecuteReader())
            {
                for (int i = 0; i < _perPage && reader.Read(); ++i)
                {
                    highscoreEntries[i] = new HighscoreEntry
                    {
                        Name = (string)reader["name"],
                        Score = (int)reader["score"]
                    };
                }
            }

            return highscoreEntries;
        }

        public HighscoreEntry GetUserHighscore(User _user)
        {
            MySqlCommand command = new MySqlCommand("", m_connection);

            command.CommandText = $"SELECT highscore.score FROM highscore WHERE user_id = {_user.ID} ORDER BY score DESC LIMIT 1";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new HighscoreEntry { Name = _user.Name, Score = (int)reader["score"] };
                }
            }

            return default(HighscoreEntry);
        }

        public void Add(User _user, int _score)
        {
            MySqlCommand command = new MySqlCommand("", m_connection);
            
            // create the new highscore row
            command.CommandText = $"INSERT INTO highscore (score, user_id) VALUES ({_score}, {_user.ID})";
            command.ExecuteNonQuery();
        }
    }

}
