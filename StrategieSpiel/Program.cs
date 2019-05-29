using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategieSpiel
{
    class Program
    {

        static void Main(string[] args)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = "server=localhost;database=strategy_game;user=root";
                connection.Open();

                BuildingTable buildingTable = new BuildingTable(connection);
                BuildingTemplateTable buildingTemplateTable = new BuildingTemplateTable(connection);
                PlayerResourceTable playerResourceTable = new PlayerResourceTable(connection);

                UpdateProduction(buildingTable, buildingTemplateTable, playerResourceTable);
            }
        }

        static void UpdateProduction(BuildingTable buildingTable, 
            BuildingTemplateTable buildingTemplateTable,
            PlayerResourceTable playerResourceTable)
        {
            foreach(var building in buildingTable.All())
            {
                var template = buildingTemplateTable.FindById(building.BuildingTemplateID);

                if(template.ResourceNeededID >= 0 && template.ResourceNeededAmount >= 0)
                {
                    PlayerResource needed = playerResourceTable
                        .FindByPlayerIDAndResourceID(building.PlayerID, template.ResourceNeededID);

                    if (needed == null)
                        continue;

                    if (needed.Amount < template.ResourceNeededAmount)
                        continue;

                    needed.Amount -= template.ResourceNeededAmount;
                    playerResourceTable.Update(needed);
                }

                PlayerResource produced = playerResourceTable
                        .FindByPlayerIDAndResourceID(building.PlayerID, template.ResourceProducedID);

                // if the resource does not exist for the player
                if(produced == null)
                {
                    // create it
                    produced = new PlayerResource
                    {
                        PlayerID = building.PlayerID,
                        ResourceID = template.ResourceProducedID,
                        Amount = 0
                    };

                    produced = playerResourceTable.Create(produced);
                }

                // increment the amount
                produced.Amount += template.ResourceProducedAmount;
                playerResourceTable.Update(produced);
            }
        }

    }
}
