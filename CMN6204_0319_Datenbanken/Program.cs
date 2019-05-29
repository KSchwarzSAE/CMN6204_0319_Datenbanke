using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace CMN6204_0319_Datenbanken
{

    class Program
    {

        static void Main(string[] args)
        {
            // using will automatically free the resources of the connection (i.e. issue mySqlConnection.Close())
            using (MySqlConnection mySqlConnection = new MySqlConnection())
            {
                // connect to the server
                mySqlConnection.ConnectionString = "Server=localhost;user=mobile_game_server;password=mvFOz1DS7vxwaemE;database=mobile_game";
                mySqlConnection.Open();

                // create the user table wrapper
                UserTable users = new UserTable(mySqlConnection);

                // create the highscore
                Highscore highscore = new Highscore(mySqlConnection);

                User? user = users.FindByName("User2");

                if (user.HasValue)
                    highscore.GetUserHighscore(user.Value);
                else
                    Console.WriteLine("The user was not found.");

                // add a new entry
                // Random random = new Random();
                // for (int i = 0; i < 10000; i++) highscore.Add("User1", random.Next(10000, 10000000));
                // for (int i = 0; i < 10000; i++) highscore.Add("User2", random.Next(10000, 10000000));
                // for (int i = 0; i < 10000; i++) highscore.Add("Kevin", random.Next(10000, 10000000));

                /*
                // print the first three pages
                for (int page = 1; page <= 3; page++)
                {
                    Console.WriteLine($"Page {page}:");
                    var entries = highscore.GetHighscore(page, 10);

                    foreach(var entry in entries)
                    {
                        Console.WriteLine($"  {entry.Name} => {entry.Score}");
                    }
                }

                var highScoreEntry = highscore.GetUserHighscore("User1");
                Console.WriteLine($"  {highScoreEntry.Name} => {highScoreEntry.Score}");*/
            }

            Console.ReadLine();
        }

    }
}
